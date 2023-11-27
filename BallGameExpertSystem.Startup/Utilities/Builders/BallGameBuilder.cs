using BallGameExpertSystem.Core.Model.Rules;

namespace BallGameExpertSystem.Utilities.Builders
{
    public class BallGameBuilder : Rule
    {
        public BallGameBuilder() { }
        public BallGameBuilder(string name) => Name = name;

        public BallGameBallBuilder Ball() => new BallGameBallBuilder();
        public BallGameGroundBuilder Ground() => new BallGameGroundBuilder();
        public BallGameTeamBuilder Team() => new BallGameTeamBuilder();

        public Rule Build() => this;
    }
}
