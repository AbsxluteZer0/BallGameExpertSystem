using BallGameExpertSystem.Core.InferenceEngine.Interfaces;

namespace BallGameExpertSystem.Core.Exceptions
{
    internal class InferenceEngineNotEnoughDataToConclude : Exception
    {
        public InferenceEngineNotEnoughDataToConclude(IBallGameInferenceEngine ie) :
            base($"Not enought data to conclude! Continue using {nameof(ie.Infer)}") { }
    }
}
