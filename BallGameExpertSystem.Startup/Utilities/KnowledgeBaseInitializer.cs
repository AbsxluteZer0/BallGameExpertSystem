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
                "Held", new[]
                {
                    Outdoors,
                    Indoors
                });

            var covering = new PlaceBallGameCharacteristic(
                "Floor Covering", new[]
                {
                    Lawn,
                    Soil,
                    Rubber,
                    Laminate
                });

            var area = new PlaceBallGameCharacteristic(
                "Ground Area", new[]
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
                "Number of Players in a Team", new[]
                {
                    One,
                    Two,
                    Five,
                    Eleven
                });

            numberOfTeams.Priority = 0;
            playersInTeam.Priority = 1;
            ballMaterial.Priority = 2;
            ballColor.Priority = 3;
            ballShape.Priority = 4;
            ballSize.Priority = 5;
            ballElasticity.Priority = 5;
            groundType.Priority = 6;
            whereHeld.Priority = 7;
            covering.Priority = 8;
            area.Priority = 9;

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

            ruleGraphBuilder.Start()
                .Characteristic(ballShape)
                    .HasValue(Round)
                .AndCharacteristic(ballMaterial)
                    .HasValue(Leather)
                .AndCharacteristic(ballSize)
                    .HasValue(Medium)
                .AndCharacteristic(ballElasticity)
                    .HasValue(Low)
                .AndCharacteristic(ballColor)
                    .HasValue(White)
                .AndCharacteristic(groundType)
                    .HasValue(Stadium)
                .AndCharacteristic(whereHeld)
                    .HasValue(Outdoors)
                .AndCharacteristic(covering)
                    .HasValue(Lawn)
                .AndCharacteristic(area)
                    .HasValue(Large)
                .AndCharacteristic(numberOfTeams)
                    .HasValue(Two)
                .AndCharacteristic(playersInTeam)
                    .HasValue(Eleven)
                .Conclude("Європейський футбол");

            ruleGraphBuilder.Start()
                .Characteristic(ballShape)
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

            ruleGraphBuilder.Start()
                .Characteristic(ballShape)
                    .HasValue(Round)
                .AndCharacteristic(ballMaterial)
                    .HasValue(Rubber)
                .AndCharacteristic(ballSize)
                    .HasValue(Small)
                .AndCharacteristic(ballElasticity)
                    .HasValue(Medium)
                .AndCharacteristic(ballColor)
                    .HasValue(Yellow)
                .AndCharacteristic(groundType)
                    .HasValue(Court)
                .AndCharacteristic(whereHeld)
                    .HasValue(Outdoors)
                    .Or(Indoors)
                .AndCharacteristic(covering)
                    .HasValue(Lawn)
                    .Or(Soil)
                    .Or(Rubber)
                .AndCharacteristic(area)
                    .HasValue(Medium)
                .AndCharacteristic(numberOfTeams)
                    .HasValue(Two)
                .AndCharacteristic(playersInTeam)
                    .HasValue(One)
                    .Or(Two)
                .Conclude("Великий теніс");

            #endregion
        }
    }
}
