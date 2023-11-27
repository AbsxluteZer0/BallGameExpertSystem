using BallGameExpertSystem.Core.InferenceEngine.Interfaces;

namespace BallGameExpertSystem.Core.Exceptions
{
    internal class InferenceEngineInvalidStateException : Exception
    {
        public InferenceEngineInvalidStateException(IBallGameInferenceEngine ie)
            : base($"Current state {ie.CurrentState} is invalid.") { }

        public InferenceEngineInvalidStateException(IBallGameInferenceEngine ie, IBallGameInferenceEngine.State expected)
            : base($"Current state {ie.CurrentState} is invalid. The state is expected to be {expected}") { }
    }
}
