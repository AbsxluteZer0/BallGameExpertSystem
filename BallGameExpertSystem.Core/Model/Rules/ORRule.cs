using BallGameExpertSystem.Core.Extensions;

namespace BallGameExpertSystem.Core.Model.Rules
{
    public class ORRule : Rule, IEquatable<ORRule>
    {
        public override List<Rule> Predecessors { get; }
        public override List<Rule> Successors { get; } = new List<Rule>();

        public ORRule(IEnumerable<Rule> predecessors)
        {
            Predecessors = predecessors.ToList();
            predecessors.ForEach(p => p.Successors?.Add(this));
        }

        public override void Update(IEnumerable<CharacteristicValue>? characteristicValues = null)
        {
            if (Predecessors == null) return;

            if (Predecessors.Where(r => r.IsObserved).Any())
                IsObserved = true;
            else
                IsObserved = false;
        }

        public bool Equals(ORRule? other)
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
            return Equals(obj as ORRule);
        }

        public override int GetHashCode()
        {
            int hash = nameof(ORRule).GetHashCode();

            foreach (var rule in Predecessors)
                hash ^= rule.GetHashCode();

            return hash;
        }
    }
}
