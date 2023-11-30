using BallGameExpertSystem.Core.Model.Characteristics;
using static BallGameExpertSystem.Startup.Utilities.Builders.CharacteristicRuleGraphBuilder;

namespace BallGameExpertSystem.Startup.Utilities.Builders
{
    internal class ChainRuleGraphBuilder
    {
        private readonly RuleGraphBuilderStore _ruleGraphBuilderStore;
        private readonly CharacteristicRuleGraphBuilder _characteristicRuleGraphBuilder;
        private readonly ValueRuleGraphBuilder _valueRuleGraphBuilder;

        public ChainRuleGraphBuilder(RuleGraphBuilderStore ruleGraphBuilderStore,
            CharacteristicRuleGraphBuilder characteristicRuleGraphBuilder,
            ValueRuleGraphBuilder valueRuleGraphBuilder)
        {
            _ruleGraphBuilderStore = ruleGraphBuilderStore;
            _characteristicRuleGraphBuilder = characteristicRuleGraphBuilder;
            _valueRuleGraphBuilder = valueRuleGraphBuilder;
        }

        public ValueRuleGraphBuilder OrValue(string value)
        {
            throw new NotImplementedException();

            return new ChainRuleGraphBuilder();
        }

        public ValueRuleGraphBuilder AndCharacteristic(BallGameCharacteristic characteristic)
        {
            _characteristicRuleGraphBuilder.PreviousCharacteristicRelationship = RelationshipType.AND;

            return _characteristicRuleGraphBuilder.Characteristic(characteristic);
        }

        public ValueRuleGraphBuilder OrCharacteristic(BallBallGameCharacteristic characteristic)
        {
            _characteristicRuleGraphBuilder.PreviousCharacteristicRelationship = RelationshipType.OR;

            return _characteristicRuleGraphBuilder.Characteristic(characteristic);
        }

        public void Conclude(string conclusion)
        {
            // Go through all of the:
            //_ruleGraphBuilder.CurrentSessionRules;
            // Ensure theyre connected
            // and create a FinalConclusion(conclusion) for them

            throw new NotImplementedException();
        }
    }
}
