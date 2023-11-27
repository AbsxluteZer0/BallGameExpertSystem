namespace BallGameExpertSystem.Core.Model.Rules
{
    public class FinalConclusion : ANDRule
    {
        public string Text { get; }
        public override List<Rule>? Successors => null;
        public FinalConclusion(IEnumerable<Rule> predcessors, string conclusionText) : base(predcessors)
        {
            Text = conclusionText;
        }
    }
}
