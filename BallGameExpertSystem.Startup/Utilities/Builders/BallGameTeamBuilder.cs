using System.Linq;

namespace BallGameExpertSystem.Utilities.Builders
{
    public class BallGameTeamBuilder : BallGameBuilder
    {
        public BallGameTeamBuilder Count(params int[] possibleNumbersOfTeams)
        {
            numberOfTeamsMatches =
                n => possibleNumbersOfTeams.Contains(n);
            return this;
        }

        public BallGameTeamBuilder Count(int from, int to)
        {
            numberOfTeamsMatches =
                n => n >= from && n <= to;
            return this;
        }

        public BallGameTeamBuilder WithNumberOfPlayers(params int[] possibleNumbersOfPlayers)
        {
            playersInTeamMatches =
                p => possibleNumbersOfPlayers.Contains(p);
            return this;
        }

        public BallGameTeamBuilder WithNumberOfPlayers(int from, int to)
        {
            playersInTeamMatches =
                p => p >= from && p <= to;
            return this;
        }
    }
}
