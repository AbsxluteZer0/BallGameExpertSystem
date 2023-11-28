using BallGameExpertSystem.Core.KnowledgeBase.Interfaces;
using BallGameExpertSystem.Core.Model;
using BallGameExpertSystem.Core.Model.Characteristics;
using BallGameExpertSystem.Core.Model.Rules;

namespace BallGameExpertSystem.Startup.Utilities.Builders
{
    internal class ValueRuleGraphBuilder : CharacteristicRuleGraphBuilder
    {
        private readonly BallGameCharacteristic _characteristic;
        private readonly Operation _operation;

        internal ValueRuleGraphBuilder(IBallGameKnowledgeBase knowledgeBase, 
            BallGameCharacteristic characteristic,
            Operation operation) : base(knowledgeBase)
        {
            _characteristic = characteristic;
            _operation = operation;
        }

        public ValueRuleGraphBuilder HasValue(string value)
        {
            if (!_characteristic.Takes(value))
                throw new ArgumentException(
                    "The characteristic cannot take the specified value.", 
                    nameof(value));

            var atomicRule = new AtomicRule(
                new CharacteristicValue(_characteristic, value));

            atomicRule = _knowledgeBase.GetRuleOrDefault(atomicRule) as AtomicRule 
                         ?? atomicRule;



            return this;
        }

        public ValueRuleGraphBuilder Or(string value) 
        {
            throw new NotImplementedException();
        }

        public void Conclude(string conclusion) => this;
    }
}