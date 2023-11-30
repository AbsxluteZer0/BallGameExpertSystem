using BallGameExpertSystem.Core.Model.Rules;

namespace BallGameExpertSystem.Startup.Utilities.Builders
{
    internal class RuleGraphBuilderStore
    {
        public List<Rule> CurrentSessionRules { get; set; } = new List<Rule>(); // stack?

        public void AddCurrentSessionRule(Rule rule)
        {
            CurrentSessionRules.Add(rule);
        }
    }
}
