using BallGameExpertSystem.Core.Model.Characteristics;
using BallGameExpertSystem.Core.Model.Rules;

namespace BallGameExpertSystem.Core.KnowledgeBase.Interfaces
{
    public interface IBallGameKnowledgeBase
    {
        /// <summary>
        /// Area of definition of the system.<br/>
        /// Characteristic with the lowest priority
        /// is considered initial.
        /// </summary>
        public List<BallGameCharacteristic> Characteristics { get; }

        /// <summary>
        /// The control graph of the system.<br/>
        /// </summary>
        public List<Rule> Rules { get; }

        public BallGameCharacteristic Initial => Characteristics.MinBy(c => c.Priority)
            ?? throw new InvalidOperationException("The characteristics list is empty.");

        public void AddCharacteristic(BallGameCharacteristic characteristic, int priority)
        {
            characteristic.Priority = priority;
            Characteristics.Add(characteristic);
        }

        public void AddCharacteristics(IEnumerable<BallGameCharacteristic> characteristics)
        {
            Characteristics.AddRange(characteristics);
        }
    }
}