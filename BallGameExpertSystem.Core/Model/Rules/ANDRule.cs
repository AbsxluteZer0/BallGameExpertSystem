using BallGameExpertSystem.Core.Extensions;

namespace BallGameExpertSystem.Core.Model.Rules
{
    public class ANDRule : Rule
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
    }
}
