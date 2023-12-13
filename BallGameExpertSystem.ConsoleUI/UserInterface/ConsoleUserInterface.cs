using BallGameExpertSystem.Core.UserInterface.Interfaces;

namespace BallGameExpertSystem.ConsoleUI.UserInterface
{
    public class ConsoleUserInterface : IUserInterface
    {
        public void ShowMessage(string message)
        {
            Console.WriteLine(message);
        }

        protected virtual void ShowMenu(IDictionary<int, string> menu)
        {
            foreach (KeyValuePair<int, string> m in menu)
                Console.WriteLine($"{m.Key}) {m.Value}");
        }

        public string RequestInput(string message)
        {
            ShowMessage(message);

            string? inputString = Console.ReadLine();

            if (string.IsNullOrEmpty(inputString))
            {
                Console.Clear();
                Console.WriteLine("You must provide input. Try again.");
                return RequestInput(message);
            }

            return inputString;
        }

        public void ConfirmationRequest(string message)
        {
            ShowMessage(message);
            Console.Read();
        }

        public bool AskClosedQuestion(string question)
        {
            var options = new Dictionary<int, string>(2)
            {
                { 1, "Yes" },
                { 2, "No" }
            };

            int answerId = AskWithOptions(question, options);

            return answerId == 1 ? true : false;
        }

        public int AskWithOptions(string question, IDictionary<int, string> options)
        {
            if (options.Count <= 0)
                throw new ArgumentException("There are no options to ask", nameof(options));

            ShowMessage(question);
            ShowMenu(options);

            Console.Write("Your answer: ");
            string? str = Console.ReadLine();
            int answerId = -1;

            if (int.TryParse(str, out answerId))
            {
                if (!options.ContainsKey(answerId))
                {
                    Console.Clear();
                    Console.WriteLine("There is no such option. Try again.");
                    return AskWithOptions(question, options);
                }
                return answerId;
            }
            else
            {
                Console.Clear();
                Console.WriteLine("The input is not a number. Try again.");
                return AskWithOptions(question, options);
            }
        }
    }
}
