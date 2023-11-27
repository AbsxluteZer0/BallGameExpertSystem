namespace BallGameExpertSystem.Core.Model.Characteristics
{
    public class BallBallGameCharacteristic : BallGameCharacteristic
    {
        // Static id generation infrastructure
        private static int s_lastGeneratedId = 0;
        private static int NextUniqueId => Interlocked.Increment(ref s_lastGeneratedId);


        private readonly int _id;
        public override string Id => 'B' + _id.ToString();

        public BallBallGameCharacteristic(string name, IEnumerable<string> possibleValues)
            : base(name, possibleValues)
        {
            _id = NextUniqueId;
        }
    }
}
