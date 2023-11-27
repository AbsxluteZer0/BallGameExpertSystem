using System.Linq;

namespace BallGameExpertSystem.Utilities.Builders
{
    public class BallGameBallBuilder : BallGameBuilder
    {
        public BallGameBallBuilder OfShape(params BallShape[] possibleShapes)
        {
            ballShapeMatches =
                s => possibleShapes.Contains(s);
            return this;
        }

        public BallGameBallBuilder OfMaterial(params BallMaterial[] possibleMaterials)
        {
            ballMaterialMatches =
                m => possibleMaterials.Contains(m);
            return this;
        }

        public BallGameBallBuilder OfSize(params BallSize[] possibleSizes)
        {
            ballSizeMatches =
                s => possibleSizes.Contains(s);
            return this;
        }

        public BallGameBallBuilder OfElasticity(params BallElasticity[] possibleElasticities)
        {
            ballElasticityMatches =
                e => possibleElasticities.Contains(e);
            return this;
        }

        public BallGameBallBuilder OfColor(params BallColor[] possibleColors)
        {
            ballColorMatches =
                c => possibleColors.Contains(c);
            return this;
        }
    }
}
