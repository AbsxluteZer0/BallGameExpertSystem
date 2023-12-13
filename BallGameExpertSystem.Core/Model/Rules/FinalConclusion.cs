namespace BallGameExpertSystem.Core.Model.Rules
{
    public class FinalConclusion : ANDRule, IEquatable<FinalConclusion>
    {
        public string Text { get; }
        public override List<Rule>? Successors => null;
        public FinalConclusion(IEnumerable<Rule> predecessors, string conclusionText) : base(predecessors)
        {
            Text = conclusionText;
        }

        public bool Equals(FinalConclusion? other)
        {
            bool? baseEquals = BaseEquals(other);
            if (baseEquals != null)
                return (bool)baseEquals;

            return Text == other!.Text
                && Predecessors.SequenceEqual(other.Predecessors);
        }

        public override bool Equals(object? obj) => Equals(obj as FinalConclusion);

        public override int GetHashCode() => Text.GetHashCode();
    }
}
