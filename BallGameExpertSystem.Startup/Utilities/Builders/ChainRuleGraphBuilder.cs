using BallGameExpertSystem.Core.Model.Characteristics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BallGameExpertSystem.Startup.Utilities.Builders.CharacteristicRuleGraphBuilder;

namespace BallGameExpertSystem.Startup.Utilities.Builders
{
    internal class ChainRuleGraphBuilder
    {
        private readonly CharacteristicRuleGraphBuilder _characteristicRuleGraphBuilder;
        private readonly ValueRuleGraphBuilder _valueRuleGraphBuilder;

        public ChainRuleGraphBuilder(CharacteristicRuleGraphBuilder characteristicRuleGraphBuilder, 
            ValueRuleGraphBuilder valueRuleGraphBuilder)
        {
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
            if (!_knowledgeBase.ContainsCharacteristic(characteristic))
                throw new ArgumentException("There's no such characteristic in the knowledge base.", nameof(characteristic));

            return new ValueRuleGraphBuilder(_knowledgeBase, characteristic, RelationshipType.AND);
        }

        public ValueRuleGraphBuilder OrCharacteristic(BallBallGameCharacteristic characteristic)
        {
            if (!_knowledgeBase.ContainsCharacteristic(characteristic))
                throw new ArgumentException("There's no such characteristic in the knowledge base.", nameof(characteristic));

            return new ValueRuleGraphBuilder(_knowledgeBase, characteristic, RelationshipType.OR);
        }

        public void Conclude(string conclusion)
        {
            throw new NotImplementedException();
        }
    }
}
