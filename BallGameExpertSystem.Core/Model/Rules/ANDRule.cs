using BallGameExpertSystem.Core.Extensions;

namespace BallGameExpertSystem.Core.Model.Rules
{
    public class ANDRule : Rule, IEquatable<ANDRule>
    {
        public override List<Rule>? Predcessors { get; }
        public override List<Rule>? Successors { get; } = new List<Rule>();

        public ANDRule(IEnumerable<Rule> predcessors)
        {
            Predcessors = predcessors.ToList();
            predcessors.ForEach(p => p.Successors?.Add(this));
        }

        public override void Update(IEnumerable<CharacteristicValue>? characteristicValues = null)
        {
            if (Predcessors == null) return;

            if (Predcessors.All(r => r.IsObserved))
                IsObserved = true;
            else
                IsObserved = false;
        }

        public bool Equals(ANDRule? other)
        {
            if (other == null)
                return false;

            if (other is FinalConclusion)
                return false;

            if (Predcessors == null
                && other.Predcessors == null)
                return true;

            if (Predcessors == null
                || other.Predcessors == null)
                return false;

            return true;
        }
    }
}
