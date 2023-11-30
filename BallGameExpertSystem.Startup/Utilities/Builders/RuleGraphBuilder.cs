using BallGameExpertSystem.Core.KnowledgeBase.Interfaces;

namespace BallGameExpertSystem.Startup.Utilities.Builders
{
    internal class RuleGraphBuilder
    {
        private IBallGameKnowledgeBase _knowledgeBase;

        public RuleGraphBuilder(IBallGameKnowledgeBase knowledgeBase)
        {
            _knowledgeBase = knowledgeBase;
        }

        public CharacteristicRuleGraphBuilder Start()
        {
            RuleGraphBuilderStore store = new RuleGraphBuilderStore();
            return new CharacteristicRuleGraphBuilder(_knowledgeBase, store);
        }
    }
}
