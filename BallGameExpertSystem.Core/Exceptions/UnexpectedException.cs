namespace BallGameExpertSystem.Core.Exceptions
{
    internal class UnexpectedException : Exception
    {
        public UnexpectedException(string details)
            : base($"Logic flow error. Probably you've broken something. Details: '{details}'") { }
    }
}
