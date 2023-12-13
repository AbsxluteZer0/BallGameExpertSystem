using BallGameExpertSystem.Core.KnowledgeBase.Interfaces;
using BallGameExpertSystem.Core.Model;
using BallGameExpertSystem.Core.Model.Characteristics;
using BallGameExpertSystem.Core.Model.Rules;
using static BallGameExpertSystem.Startup.Utilities.Builders.CharacteristicRuleGraphBuilder;

namespace BallGameExpertSystem.Startup.Utilities.Builders
{
    internal class ChainRuleGraphBuilder
    {
        private readonly RuleGraphBuilderStore _ruleGraphBuilderStore;
        private readonly CharacteristicRuleGraphBuilder _characteristicRuleGraphBuilder;

        public ChainRuleGraphBuilder(RuleGraphBuilderStore ruleGraphBuilderStore,
            CharacteristicRuleGraphBuilder characteristicRuleGraphBuilder)
        {
            _ruleGraphBuilderStore = ruleGraphBuilderStore;
            _characteristicRuleGraphBuilder = characteristicRuleGraphBuilder;
        }

        /*
         * Class methods logic
         * 
         * All methods except OrValue are trying to close the disjunction first
         * to make sure all rules added in that way are saved.
         * 
         * There is probably more sophisticated way of doing that.
        */

        public ChainRuleGraphBuilder Or(string value)
        {
            BallGameCharacteristic characteristic 
                = _characteristicRuleGraphBuilder.CurrentCharacteristic;

            _ruleGraphBuilderStore.AddRuleToDisjunction(
                new AtomicRule(
                    new CharacteristicValue(characteristic, value)));

            return this;
        }

        public ValueRuleGraphBuilder AndCharacteristic(BallGameCharacteristic characteristic)
        {
            _ruleGraphBuilderStore.TryCloseDisjunction();

            _characteristicRuleGraphBuilder.PreviousCharacteristicRelationship = Relationship.AND;

            return _characteristicRuleGraphBuilder.Characteristic(characteristic);
        }

        public ValueRuleGraphBuilder OrCharacteristic(BallBallGameCharacteristic characteristic)
        {
            _ruleGraphBuilderStore.TryCloseDisjunction();

            _characteristicRuleGraphBuilder.PreviousCharacteristicRelationship = Relationship.OR;

            return _characteristicRuleGraphBuilder.Characteristic(characteristic);
        }

        public void Conclude(string conclusion)
        {
            _ruleGraphBuilderStore.TryCloseDisjunction();

            FinalConclusion finalConclusion = new FinalConclusion(
                _ruleGraphBuilderStore.FinalConjunctionRules,
                conclusion);

            _ruleGraphBuilderStore.KnowledgeBase.AddRule(finalConclusion);
        }
    }
}
