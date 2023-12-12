using BallGameExpertSystem.Core.Model;
using BallGameExpertSystem.Core.Model.Characteristics;
using BallGameExpertSystem.Core.Model.Rules;

using static BallGameExpertSystem.Startup.Utilities.Builders.CharacteristicRuleGraphBuilder;

namespace BallGameExpertSystem.Startup.Utilities.Builders
{
    internal class ValueRuleGraphBuilder
    {
        private readonly RuleGraphBuilderStore _ruleGraphBuilderStore;
        private readonly CharacteristicRuleGraphBuilder _characteristicRuleGraphBuilde;

        internal ValueRuleGraphBuilder(RuleGraphBuilderStore ruleGraphBuilderStore,
            CharacteristicRuleGraphBuilder characteristicRuleGraphBuilder)
        {
            _ruleGraphBuilderStore = ruleGraphBuilderStore;
            _characteristicRuleGraphBuilde = characteristicRuleGraphBuilder;
        }

        public ChainRuleGraphBuilder HasValue(string value)
        {
            BallGameCharacteristic characteristic
                = _characteristicRuleGraphBuilde.CurrentCharacteristic;

            if (!characteristic.Takes(value))
                throw new ArgumentException(
                    "The characteristic cannot take the specified value.",
                    nameof(value));

            var atomicRule = new AtomicRule(
                new CharacteristicValue(characteristic, value));

            Relationship characteristicRelationship
                = _characteristicRuleGraphBuilde.PreviousCharacteristicRelationship;

            if (characteristicRelationship == Relationship.AND)
            {
                _ruleGraphBuilderStore.AddSingleRule(atomicRule);
            }
            else if (characteristicRelationship == Relationship.OR)
            {
                _ruleGraphBuilderStore.AddRuleToDisjunction(atomicRule);
            }

            return new ChainRuleGraphBuilder(_ruleGraphBuilderStore, _characteristicRuleGraphBuilde, this);
        }
    }
}