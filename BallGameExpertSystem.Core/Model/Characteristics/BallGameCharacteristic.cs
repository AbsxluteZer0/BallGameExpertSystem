using System.Collections.Immutable;
using BallGameExpertSystem.Core.Extensions;

namespace BallGameExpertSystem.Core.Model.Characteristics
{
    public abstract class BallGameCharacteristic : IComparable<BallGameCharacteristic>, IEquatable<BallGameCharacteristic>
    {
        public abstract string Id { get; }
        public virtual string Name { get; }
        public string FullIdentifier => $"{Id} \"{Name}\"";
        public virtual ImmutableDictionary<int, string> PossibleValues { get; }
        public virtual int Priority { get; set; }

        public BallGameCharacteristic(string name, IEnumerable<string> possibleValues)
        {
            Name = name;
            PossibleValues = possibleValues.ToDictionary()
                                           .ToImmutableDictionary();           
        }

        public virtual int CompareTo(BallGameCharacteristic? other)
        {
            if (other == null) 
                return -1;

            return other.Priority switch
            {
                var less when less < Priority => 1,
                var greater when greater > Priority => -1,
                _ => 0 // equal
            };
        }

        public bool Equals(BallGameCharacteristic? other)
        {
            if (other == null) 
                return false;

            return Id == other.Id;
        }
    }
}
