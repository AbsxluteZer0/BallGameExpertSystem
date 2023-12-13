using BallGameExpertSystem.Core.Model.Characteristics;

namespace BallGameExpertSystem.Core.Model
{
    public class CharacteristicValue : IEquatable<CharacteristicValue>
    {
        public BallGameCharacteristic Characteristic { get; set; }

        public int Value { get; set; }

        public CharacteristicValue(BallGameCharacteristic characteristic, int value = -1)
        {
            Characteristic = characteristic;
            Value = value;
        }

        public CharacteristicValue(BallGameCharacteristic characteristic, string value)
        {
            Characteristic = characteristic;

            System.Collections.Immutable.ImmutableDictionary<int, string> possibleValues
                = characteristic.PossibleValues;

            if (possibleValues.ContainsValue(value))
                Value = possibleValues.First(x => x.Value == value)
                                      .Key;
            else
                throw new ArgumentOutOfRangeException(nameof(value));
        }

        public bool Equals(CharacteristicValue? other)
        {
            if (other == null) return false;

            return Characteristic.Equals(other.Characteristic)
                && Value == other.Value;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Characteristic, Value);
        }
    }
}
