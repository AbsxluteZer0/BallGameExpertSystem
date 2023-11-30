using BallGameExpertSystem.Core.KnowledgeBase.Interfaces;
using BallGameExpertSystem.Core.Model;
using BallGameExpertSystem.Core.Model.Characteristics;
using BallGameExpertSystem.Core.Model.Rules;

namespace BallGameExpertSystem.Startup.Utilities.Builders
{
    internal class ValueRuleGraphBuilder
    {
        private readonly IBallGameKnowledgeBase _knowledgeBase;
        private readonly RuleGraphBuilderStore _ruleGraphBuilderStore;
        private readonly CharacteristicRuleGraphBuilder _characteristicRuleGraphBuilde;

        internal ValueRuleGraphBuilder(IBallGameKnowledgeBase knowledgeBase,
            RuleGraphBuilderStore ruleGraphBuilderStore,
            CharacteristicRuleGraphBuilder characteristicRuleGraphBuilder)
        {
            _knowledgeBase = knowledgeBase;
            _ruleGraphBuilderStore = ruleGraphBuilderStore;
            _characteristicRuleGraphBuilde = characteristicRuleGraphBuilder;
        }

        public ChainRuleGraphBuilder HasValue(string value)
        {
            BallGameCharacteristic characteristic = _characteristicRuleGraphBuilde.CurrentCharacteristic;

            if (!characteristic.Takes(value))
                throw new ArgumentException(
                    "The characteristic cannot take the specified value.", 
                    nameof(value));

            // First characteristic

            var atomicRule = new AtomicRule(
                new CharacteristicValue(characteristic, value));

            if (!_knowledgeBase.ContainsRule(atomicRule))
                _knowledgeBase.AddRule(atomicRule);

            _ruleGraphBuilderStore.AddCurrentSessionRule(atomicRule);



            // And characteristic



            // Or characteristic

            return new ChainRuleGraphBuilder(_ruleGraphBuilderStore, _characteristicRuleGraphBuilde, this);
        }
    }
}