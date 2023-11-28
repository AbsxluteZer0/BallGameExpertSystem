using BallGameExpertSystem.Core.KnowledgeBase.Interfaces;
using BallGameExpertSystem.Core.Model.Characteristics;
using BallGameExpertSystem.Startup.Utilities.Builders;

using static BallGameExpertSystem.Startup.Utilities.Constants.CharacteristicValuesConstants;

namespace BallGameExpertSystem.Startup.Utilities
{
    internal class KnowledgeBaseInitializer
    {
        public static void Initialize(IBallGameKnowledgeBase knowledgeBase)
        {
            #region Characteristics

            var ballShape = new BallBallGameCharacteristic(
                "Ball Shape", new[]
                {
                    Round,
                    Oval
                });

            var ballMaterial = new BallBallGameCharacteristic(
                "Ball Material", new[]
                {
                    Leather,
                    Rubber
                });

            var ballSize = new BallBallGameCharacteristic(
                "Ball Size", new[]
                {
                    Small,
                    Medium,
                    Large
                });

            var ballElasticity = new BallBallGameCharacteristic(
                "Ball Elasticity", new[]
                {
                    Low,
                    Medium,
                    High
                });

            var ballColor = new BallBallGameCharacteristic(
                "Ball Color", new[]
                {
                    White,
                    Orange,
                    Yellow
                });

            var groundType = new PlaceBallGameCharacteristic(
                "Ground Type", new[]
                {
                    Stadium,
                    Ground,
                    Court
                });

            var whereHeld = new PlaceBallGameCharacteristic(
                "Where Held", new[]
                {
                    Outdoors,
                    Indoors
                });

            var covering = new PlaceBallGameCharacteristic(
                "Covering", new[]
                {
                    Lawn,
                    Soil,
                    Rubber,
                    Laminate
                });

            var area = new PlaceBallGameCharacteristic(
                "Area", new[]
                {
                    Small,
                    Medium,
                    Large
                });

            var numberOfTeams = new TeamBallGameCharacteristic(
                "Number of Teams", new[]
                {
                    One,
                    Two
                });

            var playersInTeam = new TeamBallGameCharacteristic(
                "Players in a Team", new[]
                {
                    One,
                    Two,
                    Five,
                    Eleven
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

            var ruleGraphBuilder = new RuleGraphBuilder(knowledgeBase);

            ruleGraphBuilder
                .FirstCharacteristic(ballShape)
                    .HasValue(Round)
                .AndCharacteristic(ballMaterial)
                    .HasValue(Rubber)
                .AndCharacteristic(ballSize)
                    .HasValue(Large)
                .AndCharacteristic(ballElasticity)
                    .HasValue(High)
                .AndCharacteristic(ballColor)
                    .HasValue(Orange)
                .AndCharacteristic(groundType)
                    .HasValue(Ground)
                .AndCharacteristic(whereHeld)
                    .HasValue(Indoors)
                    .Or(Outdoors)
                .AndCharacteristic(covering)
                    .HasValue(Laminate)
                    .Or(Rubber)
                .AndCharacteristic(area)
                    .HasValue(Medium)
                .AndCharacteristic(numberOfTeams)
                    .HasValue(Two)
                .AndCharacteristic(playersInTeam)
                    .HasValue(Five)
                .Conclude("Баскетбол");

            #endregion
        }

        protected static void InitializeGames(IBallGameKnowledgeBase knowledgeBase)
        {
            #region Games Initialization
            knowledgeBase.Rules.Add(
                new RuleGraphBuilder("Європейський футбол")
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
                new RuleGraphBuilder("Баскетбол")
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
                new RuleGraphBuilder("Великий теніс")
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
