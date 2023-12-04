namespace BallGameExpertSystem.Core.Model.Rules
{
    public class FinalConclusion : ANDRule, IEquatable<FinalConclusion>
    {
        public string Text { get; }
        public override List<Rule>? Successors => null;
        public FinalConclusion(IEnumerable<Rule> predcessors, string conclusionText) : base(predcessors)
        {
            Text = conclusionText;
        }

        public bool Equals(FinalConclusion? other)
        {
            if (other == null) 
                return false;

            if (Text != other.Text) 
                return false;

            if (Predcessors == null 
                && other.Predcessors == null)
                return true;

            if (Predcessors == null
                || other.Predcessors == null)
                return false;

            return Predcessors.SequenceEqual(other.Predcessors);
        }
    }
}
