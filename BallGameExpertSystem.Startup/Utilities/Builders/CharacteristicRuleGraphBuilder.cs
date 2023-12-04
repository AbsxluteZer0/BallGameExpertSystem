using BallGameExpertSystem.Core.KnowledgeBase.Interfaces;
using BallGameExpertSystem.Core.Model.Characteristics;

namespace BallGameExpertSystem.Startup.Utilities.Builders
{
    internal class CharacteristicRuleGraphBuilder
    {
        private readonly IBallGameKnowledgeBase _knowledgeBase;
        private readonly RuleGraphBuilderStore _ruleGraphBuilderStore;

        internal enum Relationship
        {
            AND,
            OR
        }

        internal BallGameCharacteristic CurrentCharacteristic { get; set; } = null!;
        internal Relationship PreviousCharacteristicRelationship { get; set; } = Relationship.AND;

        public CharacteristicRuleGraphBuilder(IBallGameKnowledgeBase knowledgeBase,
            RuleGraphBuilderStore ruleGraphBuilderStore)
        {
            _knowledgeBase = knowledgeBase;
            _ruleGraphBuilderStore = ruleGraphBuilderStore;
        }

        public ValueRuleGraphBuilder Characteristic(BallGameCharacteristic characteristic)
        {
            if (!_knowledgeBase.ContainsCharacteristic(characteristic))
                throw new ArgumentException("There's no such characteristic in the knowledge base.", nameof(characteristic));

            CurrentCharacteristic = characteristic;

            return new ValueRuleGraphBuilder(_knowledgeBase, _ruleGraphBuilderStore, this);
        }
    }
}