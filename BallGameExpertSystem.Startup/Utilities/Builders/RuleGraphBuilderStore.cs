using BallGameExpertSystem.Core.KnowledgeBase.Interfaces;
using BallGameExpertSystem.Core.Model.Rules;

namespace BallGameExpertSystem.Startup.Utilities.Builders
{
    internal class RuleGraphBuilderStore
    {
        private Rule? _lastAdded;

        public IBallGameKnowledgeBase KnowledgeBase { get; set; }

        /// <summary>
        /// Rules to fall under the conclusion.
        /// </summary>
        public List<Rule> FinalConjunctionRules { get; set; } = new List<Rule>();

        /// <summary>
        /// Rules to fall under an OR rule.
        /// </summary>
        public List<Rule> CurrentDisjunctionRules { get; set; } = new List<Rule>();

        public RuleGraphBuilderStore(IBallGameKnowledgeBase knowledgeBase)
        {
            KnowledgeBase = knowledgeBase;
        }

        public void AddSingleRule(Rule rule)
        {
            FinalConjunctionRules.Add(_lastAdded = rule);
            CurrentDisjunctionRules = new List<Rule> { rule };
        }

        public void AddRuleToDisjunction(Rule rule)
        {
            CurrentDisjunctionRules.Add(rule);
        }

        public void CloseDisjunction()
        {
            if (CurrentDisjunctionRules.Count <= 1)
                throw new InvalidOperationException(
                    "Cannot perform a disjunction with less than 2 elements");

            if (_lastAdded != null)
                FinalConjunctionRules.Remove(_lastAdded);

            ORRule disjunction = new ORRule(CurrentDisjunctionRules);
            FinalConjunctionRules.Add(disjunction);

            CurrentDisjunctionRules.Clear();
        }

        public bool TryCloseDisjunction()
        {
            try
            {
                CloseDisjunction();
                return true;
            }
            catch 
            {
                return false;
            }
        }
    }
}
