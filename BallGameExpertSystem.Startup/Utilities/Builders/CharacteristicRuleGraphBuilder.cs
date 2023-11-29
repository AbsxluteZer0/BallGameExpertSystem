using BallGameExpertSystem.Core.KnowledgeBase.Interfaces;
using BallGameExpertSystem.Core.Model.Characteristics;

namespace BallGameExpertSystem.Startup.Utilities.Builders
{
    internal class CharacteristicRuleGraphBuilder
    {
        protected readonly IBallGameKnowledgeBase _knowledgeBase;

        internal enum RelationshipType
        {
            AND,
            OR
        }

        internal BallGameCharacteristic CurrentCharacteristic { get; set; } = null!;
        internal RelationshipType PreviousCharacteristicRelationship { get; set; }

        public CharacteristicRuleGraphBuilder(IBallGameKnowledgeBase knowledgeBase)
        {
            _knowledgeBase = knowledgeBase;
        }

        public ValueRuleGraphBuilder Characteristic(BallGameCharacteristic characteristic)
        {
            if (!_knowledgeBase.ContainsCharacteristic(characteristic))
                throw new ArgumentException("There's no such characteristic in the knowledge base.", nameof(characteristic));

            CurrentCharacteristic = characteristic;

            return new ValueRuleGraphBuilder(_knowledgeBase, this);
        }
    }
}