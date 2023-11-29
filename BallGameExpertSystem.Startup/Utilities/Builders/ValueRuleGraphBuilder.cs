using BallGameExpertSystem.Core.KnowledgeBase.Interfaces;
using BallGameExpertSystem.Core.Model;
using BallGameExpertSystem.Core.Model.Characteristics;
using BallGameExpertSystem.Core.Model.Rules;

namespace BallGameExpertSystem.Startup.Utilities.Builders
{
    internal class ValueRuleGraphBuilder : CharacteristicRuleGraphBuilder
    {
        private readonly CharacteristicRuleGraphBuilder _characteristicRuleGraphBuilde;

        internal ValueRuleGraphBuilder(IBallGameKnowledgeBase knowledgeBase, 
            CharacteristicRuleGraphBuilder characteristicRuleGraphBuilder) : base(knowledgeBase)
        {
            _characteristicRuleGraphBuilde = characteristicRuleGraphBuilder;
        }

        public ChainRuleGraphBuilder HasValue(string value)
        {
            BallGameCharacteristic characteristic = _characteristicRuleGraphBuilde.CurrentCharacteristic;

            if (!characteristic.Takes(value))
                throw new ArgumentException(
                    "The characteristic cannot take the specified value.", 
                    nameof(value));

            var atomicRule = new AtomicRule(
                new CharacteristicValue(characteristic, value));

            atomicRule = _knowledgeBase.GetRuleOrDefault(atomicRule) as AtomicRule 
                         ?? atomicRule;



            return new ChainRuleGraphBuilder(_characteristicRuleGraphBuilde, this);
        }
    }
}