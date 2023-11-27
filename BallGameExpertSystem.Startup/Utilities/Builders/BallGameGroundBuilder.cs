using System.Linq;

namespace BallGameExpertSystem.Utilities.Builders
{
    public class BallGameGroundBuilder : BallGameBuilder
    {
        public BallGameGroundBuilder OfType(params GroundType[] possibleGroundTypes)
        {
            groundTypeMatches =
                g => possibleGroundTypes.Contains(g);
            return this;
        }

        public BallGameGroundBuilder Is(params WhereHeld[] possibleLocations)
        {
            whereHeldMatches = 
                w => possibleLocations.Contains(w); 
            return this;
        }

        public BallGameGroundBuilder CoveredWith(params Covering[] possibleCoverings)
        {
            coveringMatches =
                c => possibleCoverings.Contains(c);
            return this;
        }

        public BallGameGroundBuilder OfArea(params Area[] possibleAreas) 
        {
            areaMatches =
                a => possibleAreas.Contains(a);
            return this;
        }
    }
}
