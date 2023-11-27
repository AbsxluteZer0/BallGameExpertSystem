using BallGameExpertSystem.Core.InferenceEngine.Interfaces;

namespace BallGameExpertSystem.Core.Exceptions
{
    internal class InferenceEngineNoContextException : Exception
    {
        public InferenceEngineNoContextException(string message) : base(message) { }
        public InferenceEngineNoContextException(string message, Exception innerException) : base(message, innerException) { }
        public InferenceEngineNoContextException(IBallGameInferenceEngine ie) :
            base($"You must set a context using {nameof(ie.SetContext)}() method first!") { }
        public InferenceEngineNoContextException(IBallGameInferenceEngine ie, Exception innerException) :
            base($"You must set a context using {nameof(ie.SetContext)}() method first!", innerException) { }
    }
}
