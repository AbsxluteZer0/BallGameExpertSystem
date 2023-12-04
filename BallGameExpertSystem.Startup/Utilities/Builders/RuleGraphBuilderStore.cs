using BallGameExpertSystem.Core.Model.Rules;

namespace BallGameExpertSystem.Startup.Utilities.Builders
{
    internal class RuleGraphBuilderStore
    {
        /// <summary>
        /// Rules to fall under the conclusion.
        /// </summary>
        public List<Rule> FinalConjunctionRules { get; set; } = new List<Rule>();

        /// <summary>
        /// Rules to fall under an OR rule.
        /// </summary>
        public List<Rule> CurrentDisjunctionRules { get; set; } = new List<Rule>();

        public void AddSingleRule(Rule rule)
        {
            FinalConjunctionRules.Add(rule);
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
