using BallGameExpertSystem.Core.Extensions;

namespace BallGameExpertSystem.Core.Model.Rules
{
    public class ORRule : Rule
    {
        public override List<Rule>? Predcessors { get; }
        public override List<Rule>? Successors { get; } = new List<Rule>();

        public ORRule(IEnumerable<Rule> predcessors)
        {
            Predcessors = predcessors.ToList();
            predcessors.ForEach(p => p.Successors?.Add(this));
        }

        public override void Update(IEnumerable<CharacteristicValue>? characteristicValues = null)
        {
            if (Predcessors == null) return;

            if (Predcessors.Where(r => r.IsObserved).Any())
                IsObserved = true;
            else
                IsObserved = false;
        }
    }
}
