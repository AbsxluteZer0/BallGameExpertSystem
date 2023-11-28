using BallGameExpertSystem.ConsoleUI.UserInterface;
using BallGameExpertSystem.Core.InferenceEngine;
using BallGameExpertSystem.Core.InferenceEngine.Interfaces;
using BallGameExpertSystem.Core.KnowledgeBase;
using BallGameExpertSystem.Core.KnowledgeBase.Interfaces;
using BallGameExpertSystem.Core.UserInterface.Interfaces;
using BallGameExpertSystem.Startup.Utilities;

namespace BallGameExpertSystem.Startup
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            IUserInterface userInterface = new ConsoleUserInterface();
            IBallGameKnowledgeBase knowledgeBase = new BallGameKnowledgeBase();
            IBallGameInferenceEngine inferenceEngine = new BallGameInferenceEngine();

            KnowledgeBaseInitializer.Initialize(knowledgeBase);
            
            var expertSystem = new Core.BallGameExpertSystem(knowledgeBase, inferenceEngine, userInterface);

            expertSystem.Conclude(); // starting point

            userInterface.RequestInput("Press any key to continue...");
        }
    }
}
