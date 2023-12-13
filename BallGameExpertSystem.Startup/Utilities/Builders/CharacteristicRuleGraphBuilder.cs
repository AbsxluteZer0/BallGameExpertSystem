using BallGameExpertSystem.Core.Model.Characteristics;

namespace BallGameExpertSystem.Startup.Utilities.Builders
{
    internal class CharacteristicRuleGraphBuilder
    {
        private readonly RuleGraphBuilderStore _ruleGraphBuilderStore;

        internal enum Relationship
        {
            AND,
            OR
        }

        internal BallGameCharacteristic CurrentCharacteristic { get; set; } = null!;
        internal Relationship PreviousCharacteristicRelationship { get; set; } = Relationship.AND;

        public CharacteristicRuleGraphBuilder(RuleGraphBuilderStore ruleGraphBuilderStore)
        {
            _ruleGraphBuilderStore = ruleGraphBuilderStore;
        }

        public ValueRuleGraphBuilder Characteristic(BallGameCharacteristic characteristic)
        {
            if (!_ruleGraphBuilderStore.KnowledgeBase.ContainsCharacteristic(characteristic))
                throw new ArgumentException("There's no such characteristic in the knowledge base.", nameof(characteristic));

            CurrentCharacteristic = characteristic;

            return new ValueRuleGraphBuilder(_ruleGraphBuilderStore, this);
        }
    }
}