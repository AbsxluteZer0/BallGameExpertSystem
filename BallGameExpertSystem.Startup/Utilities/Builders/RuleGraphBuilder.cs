using BallGameExpertSystem.Core.KnowledgeBase.Interfaces;
using BallGameExpertSystem.Core.Model;
using BallGameExpertSystem.Core.Model.Characteristics;
using BallGameExpertSystem.Core.Model.Rules;

namespace BallGameExpertSystem.Startup.Utilities.Builders
{
    internal class RuleGraphBuilder
    {
        private IBallGameKnowledgeBase _knowledgeBase;

        public RuleGraphBuilder(IBallGameKnowledgeBase knowledgeBase)
        {
            _knowledgeBase = knowledgeBase;
        }

        public CharacteristicRuleGraphBuilder Initiate() => new CharacteristicRuleGraphBuilder(_knowledgeBase);
    }
}
