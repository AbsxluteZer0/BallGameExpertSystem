namespace BallGameExpertSystem.Core.Model.Characteristics
{
    public class TeamBallGameCharacteristic : BallGameCharacteristic
    {
        // Static id generation infrastructure
        private static int s_lastGeneratedId;
        private static int NextUniqueId => Interlocked.Increment(ref s_lastGeneratedId);

        private readonly int _id;
        public override string Id => 'T' + _id.ToString();

        public TeamBallGameCharacteristic(string name, IEnumerable<string> possibleValues)
            : base(name, possibleValues)
        {
            _id = NextUniqueId;
        }
    }
}
