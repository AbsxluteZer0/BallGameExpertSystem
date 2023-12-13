using BallGameExpertSystem.Core.Exceptions;
using BallGameExpertSystem.Core.Extensions;
using BallGameExpertSystem.Core.InferenceEngine.Interfaces;
using BallGameExpertSystem.Core.KnowledgeBase.Interfaces;
using BallGameExpertSystem.Core.Model;
using BallGameExpertSystem.Core.Model.Characteristics;
using BallGameExpertSystem.Core.Model.Rules;
using System.Collections.Immutable;
using System.Collections.ObjectModel;

namespace BallGameExpertSystem.Core.InferenceEngine
{
    public class BallGameInferenceEngine : IBallGameInferenceEngine
    {
        private readonly ObservableCollection<CharacteristicValue> _knownCharacteristicValues;
        private ImmutableList<BallGameCharacteristic>? _characteristics;
        private ImmutableList<Rule>? _rules;

        private List<BallGameCharacteristic>? _availableCharacteristics;

        // For SingleSuggestion state only
        private Stack<CharacteristicValue>? _pendingCharValueSuggestions;
        private int excessiveSuggestions;

        public IBallGameInferenceEngine.State CurrentState { get; private set; } = IBallGameInferenceEngine.State.NoContext;

        private FinalConclusion? _conclusion;
        public string Conclusion
        {
            get
            {
                switch (CurrentState)
                {
                    case IBallGameInferenceEngine.State.NoContext:
                        throw new InferenceEngineNoContextException(this);

                    case IBallGameInferenceEngine.State.Ready:
                    case IBallGameInferenceEngine.State.SingleSuggestion:
                    case IBallGameInferenceEngine.State.MultipleSuggestions:
                        throw new InferenceEngineNotEnoughDataToConclude(this);

                    case IBallGameInferenceEngine.State.Conclusion:
                        return _conclusion?.Text ?? "Couldn't find a game that meets the specified requirements.";

                    default:
                        return "Error";
                }
            }
        }

        public BallGameInferenceEngine()
        {
            _knownCharacteristicValues = new ObservableCollection<CharacteristicValue>();
            _knownCharacteristicValues.CollectionChanged += KnownCharacteristicValuesChanged;
        }

        public CharacteristicValue? Infer(CharacteristicValue input)
        {
            switch (CurrentState)
            {
                case IBallGameInferenceEngine.State.NoContext:
                    throw new InferenceEngineNoContextException(this);

                case IBallGameInferenceEngine.State.Ready:
                case IBallGameInferenceEngine.State.MultipleSuggestions:
                    return MultipleSuggestionsInfer(input);

                case IBallGameInferenceEngine.State.SingleSuggestion:
                    return SingleSuggestionInfer(input);

                default: // Conclusion
                    return null;
            }
        }

        private CharacteristicValue? MultipleSuggestionsInfer(CharacteristicValue input)
        {
            if (_rules == null)
                throw new InferenceEngineNoContextException(this);

            ProcessInput(input);

            IEnumerable<FinalConclusion> possibleConclusions
                = GetPossibleConclusions(_rules.Where(r => r.IsObserved));

            CharacteristicValue? result = null;

            switch (possibleConclusions.Count())
            {
                case 0:
                    CurrentState = IBallGameInferenceEngine.State.Conclusion;
                    break;

                case 1:
                    CurrentState = IBallGameInferenceEngine.State.SingleSuggestion;

                    ProcessSingleConclusion(possibleConclusions.Single());

                    if (_pendingCharValueSuggestions != null)
                        result = _pendingCharValueSuggestions.Peek();                    
                    break;

                default: // > 1
                    CurrentState = IBallGameInferenceEngine.State.MultipleSuggestions;

                    BallGameCharacteristic? characteristicSuggestion
                        = GetTheBestCharacteristicSuggestion(possibleConclusions);

                    if (characteristicSuggestion != null)
                        result = new CharacteristicValue(characteristicSuggestion);
                    break;                   
            }

            return result;
        }

        private CharacteristicValue? SingleSuggestionInfer(CharacteristicValue input)
        {
            if (_pendingCharValueSuggestions == null)
                throw new InferenceEngineInvalidStateException(this);
            if (!_pendingCharValueSuggestions.Any())
                throw new UnexpectedException(
                    $"The collection {nameof(_pendingCharValueSuggestions)} isn't supposed to be empty.");

            CharacteristicValue suggestion;

            ProcessInput(input);

            // if there are several OR-related values for single characteristic
            // additional values for the characteristic are asked only if the answer is negative
            int currentSuggestionsCount = _pendingCharValueSuggestions
                .Where(cv => cv.Characteristic.Equals(input.Characteristic))
                .Count();

            excessiveSuggestions = currentSuggestionsCount - 1;

            if (_pendingCharValueSuggestions.Peek()
                                            .Equals(input))
            {
                _pendingCharValueSuggestions.Pop();

                while (excessiveSuggestions > 0)
                    _pendingCharValueSuggestions.Pop();

                if (_pendingCharValueSuggestions.TryPeek(out suggestion!))
                    return suggestion;
                else
                    CurrentState = IBallGameInferenceEngine.State.Conclusion;
            }
            else
            {
                if (excessiveSuggestions > 0)
                    _pendingCharValueSuggestions.Pop();
                
                if (_pendingCharValueSuggestions.TryPeek(out suggestion!))
                    return suggestion;

                _conclusion = null;
                CurrentState = IBallGameInferenceEngine.State.Conclusion;
            }
            
            return null;
        }

        private void ProcessInput(CharacteristicValue input)
        {
            if (_availableCharacteristics == null
                || _rules == null)
                throw new InferenceEngineNoContextException(this);

            _knownCharacteristicValues.Add(input);
            _availableCharacteristics.Remove(input.Characteristic);
        }

        private IEnumerable<Rule> ObtainUniqueSuccessors(IEnumerable<Rule> rules)
        {
            var uniqueSuccessors = new HashSet<Rule>();

            foreach (Rule rule in rules)
            {
                if (rule.Successors != null)
                    uniqueSuccessors.UnionWith(rule.Successors);
            }

            return uniqueSuccessors;
        }

        private IEnumerable<Rule> ObtainUniquePredecessors(IEnumerable<Rule> rules)
        {
            var uniquePredecessors = new HashSet<Rule>();

            foreach (Rule rule in rules)
            {
                if (rule.Predecessors != null)
                    uniquePredecessors.UnionWith(rule.Predecessors);
            }

            return uniquePredecessors;
        }

        private IEnumerable<AtomicRule> GetAllRelatedAtomicRules(IEnumerable<Rule> rules)
        {
            IEnumerable<Rule> uniquePredcessors = ObtainUniquePredecessors(rules);

            var result = new HashSet<AtomicRule>();

            do
            {
                IEnumerable<AtomicRule?> foundAtoms
                    = uniquePredcessors.Where(r => r is AtomicRule)
                            .Select(fc => fc as AtomicRule);

                if (foundAtoms.Any())
                    result.UnionWith(foundAtoms as IEnumerable<AtomicRule>);

                uniquePredcessors = ObtainUniquePredecessors(uniquePredcessors);

            } while (uniquePredcessors.Any());

            return result;
        }

        private IEnumerable<FinalConclusion> GetPossibleConclusions(IEnumerable<Rule> observedRules)
        {
            IEnumerable<Rule> uniqueSuccessors = ObtainUniqueSuccessors(observedRules);

            var relatedFinalConclusions = new HashSet<FinalConclusion>();

            do
            {
                IEnumerable<FinalConclusion?> foundConclusions
                    = uniqueSuccessors.Where(r => r is FinalConclusion)
                                      .Select(fc => fc as FinalConclusion);

                if (foundConclusions != null)
                    relatedFinalConclusions.UnionWith(foundConclusions as IEnumerable<FinalConclusion>);

                uniqueSuccessors = ObtainUniqueSuccessors(uniqueSuccessors);

            } while (uniqueSuccessors.Any());


            var commonRelatedFinalConclusions
                = relatedFinalConclusions.Where(fc => 
                    observedRules.All(obs => obs.IsPredecessorOf(fc)));

            return commonRelatedFinalConclusions;
        }

        private void ProcessSingleConclusion(FinalConclusion conclusion)
        {
            if (_availableCharacteristics == null)
                throw new InferenceEngineNoContextException(this);

            _conclusion = conclusion;

            List<AtomicRule> reducedRelatedAtomicRules = TryReduceAtomicRules(conclusion);

            var unobservedRelatedAtomicRules = reducedRelatedAtomicRules.Where(r => !r.IsObserved);

            if (!unobservedRelatedAtomicRules.Any()) 
            {
                if (conclusion.IsObserved)
                {
                    CurrentState = IBallGameInferenceEngine.State.Conclusion;
                    return;
                }
                else
                    throw new ExecutionFlowException(
                        $"{nameof(unobservedRelatedAtomicRules)} collection is empty " +
                        $"but the conclusion is not observed.");
            }

            _pendingCharValueSuggestions = new Stack<CharacteristicValue>(
                unobservedRelatedAtomicRules.Where(ar => _availableCharacteristics
                                                .Contains(ar.CharacteristicValue.Characteristic))
                                            .Select(ar => ar.CharacteristicValue)
                                            .OrderBy(cv => cv.Characteristic));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="possibleConclusions"></param>
        /// <returns>
        /// 1) related atomic rules w/o those unnecessary to ask;<br/>
        /// 2) all related atomic rules if there is nothing to reduce.
        /// </returns>
        private List<AtomicRule> TryReduceAtomicRules(FinalConclusion possibleConclusions)
        {
            List<AtomicRule> relatedAtomicRules
               = GetAllRelatedAtomicRules(new List<FinalConclusion>() { possibleConclusions }).ToList();

            var multipleValueCharacteristicGroups
                = relatedAtomicRules.GroupBy(ar => ar.CharacteristicValue.Characteristic)
                                    .Where(group => group.Count() > 1);

            List<AtomicRule> redundant;

            if (multipleValueCharacteristicGroups.Any()
                && TryFindRedundantCharacteristics(multipleValueCharacteristicGroups, out redundant))
            {
                foreach (var atomicRule in redundant)
                {
                    atomicRule.Successors?
                              .Where(s => s.IsPredecessorOf(possibleConclusions))
                              .ForEach(s => s.LockObserved());

                    _availableCharacteristics?.Remove(atomicRule.CharacteristicValue.Characteristic);
                    relatedAtomicRules.Remove(atomicRule);
                }
            }

            return relatedAtomicRules;
        }

        private bool TryFindRedundantCharacteristics(
            IEnumerable<IGrouping<BallGameCharacteristic, AtomicRule>> multipleValueCharacteristicGroups,
            out List<AtomicRule> redundant)
        {
            redundant = new List<AtomicRule>();

            foreach (var group in multipleValueCharacteristicGroups)
            {
                if (group.Key.PossibleValues.Keys.SequenceEqual(
                        group.Select(ar => ar.CharacteristicValue.Value)))
                    redundant.AddRange(group);
            }

            return redundant.Any();
        }

        private BallGameCharacteristic? GetTheBestCharacteristicSuggestion(IEnumerable<FinalConclusion> possibleConclusions)
        {
            IEnumerable<AtomicRule> relatedAtomicRules
               = GetAllRelatedAtomicRules(possibleConclusions);

            IEnumerable<AtomicRule> availableAtomicRules
                = FilterAtomicRulesByAvailableCharacteristics(relatedAtomicRules);

            Dictionary<AtomicRule, int> atomicRulesAndNumbersOfMatchingSucceedingConclusions =
                new Dictionary<AtomicRule, int>();

            foreach (var atomicRule in availableAtomicRules)
            {
                int c = 0;

                foreach (var conclusion in possibleConclusions)
                    if (atomicRule.IsPredecessorOf(conclusion))
                        c++;

                atomicRulesAndNumbersOfMatchingSucceedingConclusions[atomicRule] = c;
            }

            var maxMatchingCount
                = atomicRulesAndNumbersOfMatchingSucceedingConclusions
                  .Values
                  .Max();

            var maxMatchingLeastPriorityRule = atomicRulesAndNumbersOfMatchingSucceedingConclusions
                .Where(rn => rn.Value == maxMatchingCount)
                .Select(rn => rn.Key)
                .OrderBy(rule => rule.CharacteristicValue.Characteristic)
                .FirstOrDefault();

            return maxMatchingLeastPriorityRule?.CharacteristicValue.Characteristic;
        }

        private IEnumerable<AtomicRule> FilterAtomicRulesByAvailableCharacteristics(IEnumerable<AtomicRule> relatedAtomicRules)
        {
            if (_availableCharacteristics == null)
                throw new InferenceEngineNoContextException(this);

            return relatedAtomicRules.Where(r => _availableCharacteristics
                                     .Contains(r.CharacteristicValue.Characteristic));
        }

        public void SetContext(IBallGameKnowledgeBase knowledgeBase)
        {
            _characteristics = knowledgeBase.Characteristics.ToImmutableList();
            _rules = knowledgeBase.Rules.ToImmutableList();

            _availableCharacteristics = _characteristics.ToList();

            CurrentState = IBallGameInferenceEngine.State.Ready;
        }

        public void Reset()
        {
            if (_characteristics == null)
                throw new InferenceEngineNoContextException(this);

            _knownCharacteristicValues.Clear();
            _availableCharacteristics = _characteristics.ToList();

            _pendingCharValueSuggestions = null;
            _conclusion = null;

            CurrentState = IBallGameInferenceEngine.State.Ready;
        }

        private void KnownCharacteristicValuesChanged(object? sender,
            System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (_rules == null)
                throw new InferenceEngineNoContextException(this);

            _rules.Where(r => r.Depth == 0)
                 .ForEach(atomicRule => atomicRule.Update(_knownCharacteristicValues));

            for (int i = 1; i <= _rules.Select(r => r.Depth).Max(); i++)
            {
                _rules.Where(r => r.Depth == i)
                 .ForEach(compositeRule => compositeRule.Update());
            }
        }
    }
}
