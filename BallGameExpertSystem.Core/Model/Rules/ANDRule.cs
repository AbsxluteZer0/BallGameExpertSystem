using BallGameExpertSystem.Core.Extensions;

namespace BallGameExpertSystem.Core.Model.Rules
{
    public class ANDRule : Rule, IEquatable<ANDRule>
    {
        public override List<Rule> Predecessors { get; }
        public override List<Rule>? Successors { get; } = new List<Rule>();

        public ANDRule(IEnumerable<Rule> predecessors)
        {
            Predecessors = predecessors.ToList();
            predecessors.ForEach(p => p.Successors?.Add(this));
        }

        public override void Update(IEnumerable<CharacteristicValue>? characteristicValues = null)
        {
            if (Predecessors == null) return;

            if (Predecessors.All(r => r.IsObserved))
                IsObserved = true;
            else
                IsObserved = false;
        }

        public bool Equals(ANDRule? other)
        {
            bool? baseEquals = BaseEquals(other);
            if (baseEquals != null)
                return (bool)baseEquals;

            if (Predecessors == null
                && other!.Predecessors == null)
                return true;

            if (Predecessors == null
                || other!.Predecessors == null)
                return false;

            return Predecessors.ScrambledEquals(other.Predecessors);
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as ANDRule);
        }

        public override int GetHashCode()
        {
            int hash = nameof(ANDRule).GetHashCode();

            foreach (var rule in Predecessors)
                hash ^= rule.GetHashCode();

            return hash;
        }
    }
}
