namespace BallGameExpertSystem.Core.UserInterface.Interfaces
{
    public interface IUserInterface
    {
        void ShowMessage(string message);
        string RequestInput(string message);
        void ConfirmationRequest(string message);
        bool AskClosedQuestion(string question);
        int AskWithOptions(string question, IDictionary<int, string> options);
        
    }
}
