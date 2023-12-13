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
            if (other == null) 
                return false;

            if (Text != other.Text) 
                return false;

            if (Predecessors == null 
                && other.Predecessors == null)
                return true;

            if (Predecessors == null
                || other.Predecessors == null)
                return false;

            return Predecessors.SequenceEqual(other.Predecessors);
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as FinalConclusion);
        }

        public override int GetHashCode()
        {
            return Text.GetHashCode();
        }
    }
}
