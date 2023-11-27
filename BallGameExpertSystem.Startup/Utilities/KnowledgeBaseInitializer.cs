using BallGameExpertSystem.Core.KnowledgeBase;
using BallGameExpertSystem.Core.KnowledgeBase.Interfaces;
using BallGameExpertSystem.Core.Model.Characteristics;
using BallGameExpertSystem.Utilities.Builders;

namespace BallGameExpertSystem
{
    internal class KnowledgeBaseInitializer
    {
        public static void Initialize(IBallGameKnowledgeBase knowledgeBase)
        {
            #region Characteristics

            var ballShape = new BallBallGameCharacteristic(
                "Ball Shape", new[] 
                { 
                    "Round", 
                    "Oval" 
                });

            var ballMaterial = new BallBallGameCharacteristic(
                "Ball Material", new[] 
                { 
                    "Leather", 
                    "Rubber" 
                });

            var ballSize = new BallBallGameCharacteristic(
                "Ball Size", new[] 
                { 
                    "Small", 
                    "Medium", 
                    "Large" 
                });

            var ballElasticity = new BallBallGameCharacteristic(
                "Ball Elasticity", new[] 
                { 
                    "Low",                                           
                    "Medium",                                           
                    "High" 
                });

            var ballColor = new BallBallGameCharacteristic(
                "Ball Color", new[] 
                { 
                    "White", 
                    "Orange", 
                    "Yellow" 
                });

            var groundType = new PlaceBallGameCharacteristic(
                "Ground Type", new[] 
                { 
                    "Stadium", 
                    "Ground", 
                    "Court" 
                });

            var whereHeld = new PlaceBallGameCharacteristic(
                "Where Held", new[] 
                { 
                    "Outdoors",                                      
                    "Indoors" 
                });

            var covering = new PlaceBallGameCharacteristic(
                "Covering", new[] 
                { 
                    "Lawn", 
                    "Soil", 
                    "Rubber", 
                    "Laminate" 
                });

            var area = new PlaceBallGameCharacteristic(
                "Area", new[] 
                { 
                    "Small",                                
                    "Medium",
                    "Large" 
                });

            var numberOfTeams = new TeamBallGameCharacteristic(
                "Number of Teams", new[] 
                { 
                    "1",
                    "2"
                });

            var playersInTeam = new TeamBallGameCharacteristic(
                "Players in a Team", new[] 
                { 
                    "1",
                    "2",                
                    "5",
                    "11"
                });

            knowledgeBase.AddCharacteristics(new BallGameCharacteristic[]
            {
                ballShape,
                ballMaterial,
                ballSize,
                ballElasticity, 
                ballColor,
                groundType,
                whereHeld,
                covering,
                area,
                numberOfTeams,
                playersInTeam
            });

            #endregion

            #region Rules

            InitializeGames(knowledgeBase);

            #endregion
        }

        protected static void InitializeGames(IBallGameKnowledgeBase knowledgeBase) 
        {
            #region Games Initialization
            knowledgeBase.Rules.Add(
                new BallGameBuilder("Європейський футбол")
                .Ball().OfShape(BallShape.Round)
                       .OfMaterial(BallMaterial.Leather)
                       .OfSize(BallSize.Medium)
                       .OfElasticity(BallElasticity.Low)
                       .OfColor(BallColor.White)
                .Ground().OfType(GroundType.Stadium)
                         .Is(WhereHeld.Outdoors)
                         .CoveredWith(Covering.Lawn)
                         .OfArea(Area.Large)
                .Team().Count(2)
                       .WithNumberOfPlayers(11)
                .Build());

            knowledgeBase.Rules.Add(
                new BallGameBuilder("Баскетбол")
                    .Ball().OfShape(BallShape.Round)
                           .OfMaterial(BallMaterial.Rubber)
                           .OfSize(BallSize.Large)
                           .OfElasticity(BallElasticity.High)
                           .OfColor(BallColor.Orange)
                    .Ground().OfType(GroundType.Ground)
                             .Is(WhereHeld.Outdoors, WhereHeld.Indoors)
                             .CoveredWith(Covering.Laminate, Covering.Rubber)
                             .OfArea(Area.Medium)
                    .Team().Count(2)
                           .WithNumberOfPlayers(5)
                    .Build());

            knowledgeBase.Rules.Add(
                new BallGameBuilder("Великий теніс")
                    .Ball().OfShape(BallShape.Round)
                           .OfMaterial(BallMaterial.Rubber)
                           .OfSize(BallSize.Small)
                           .OfElasticity(BallElasticity.Medium)
                           .OfColor(BallColor.Yellow)
                    .Ground().OfType(GroundType.Court)
                             .Is(WhereHeld.Outdoors, WhereHeld.Indoors)
                             .CoveredWith(Covering.Lawn, Covering.Soil, Covering.Rubber)
                             .OfArea(Area.Medium)
                    .Team().Count(2)
                           .WithNumberOfPlayers(new int[] { 1, 2 })
                    .Build());
            #endregion
        }
    }
}
