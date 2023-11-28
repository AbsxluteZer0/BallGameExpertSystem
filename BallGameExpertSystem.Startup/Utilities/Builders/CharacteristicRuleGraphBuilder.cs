using BallGameExpertSystem.Core.KnowledgeBase.Interfaces;
using BallGameExpertSystem.Core.Model.Characteristics;

namespace BallGameExpertSystem.Startup.Utilities.Builders
{
    internal class CharacteristicRuleGraphBuilder
    {
        internal enum Operation
        {
            AND,
            OR
        }

        protected readonly IBallGameKnowledgeBase _knowledgeBase;

        public CharacteristicRuleGraphBuilder(IBallGameKnowledgeBase knowledgeBase)
        {
            _knowledgeBase = knowledgeBase;
        }

        public ValueRuleGraphBuilder AndCharacteristic(BallGameCharacteristic characteristic)
        {
            if (!_knowledgeBase.ContainsCharacteristic(characteristic))
                throw new ArgumentException("There's no such characteristic in the knowledge base.", nameof(characteristic));

            return new ValueRuleGraphBuilder(_knowledgeBase, characteristic, Operation.AND);
        }

        public ValueRuleGraphBuilder OrCharacteristic(BallBallGameCharacteristic characteristic)
        {
            if (!_knowledgeBase.ContainsCharacteristic(characteristic))
                throw new ArgumentException("There's no such characteristic in the knowledge base.", nameof(characteristic));

            return new ValueRuleGraphBuilder(_knowledgeBase, characteristic, Operation.OR);
        }
    }
}