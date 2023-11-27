using BallGameExpertSystem.Core.Exceptions;
using BallGameExpertSystem.Core.InferenceEngine.Interfaces;
using BallGameExpertSystem.Core.KnowledgeBase.Interfaces;
using BallGameExpertSystem.Core.Model;
using BallGameExpertSystem.Core.Model.Characteristics;
using BallGameExpertSystem.Core.UserInterface.Interfaces;

namespace BallGameExpertSystem.Core
{
    public class BallGameExpertSystem
    {
        private readonly IBallGameKnowledgeBase _knowledgeBase;
        private readonly IBallGameInferenceEngine _inferenceEngine;
        private readonly IUserInterface _userInterface;

        private CharacteristicValue? _currentSuggestion;
        private CharacteristicValue? CurrentSuggestion 
        { 
            get => _currentSuggestion ?? 
                throw new ExecutionFlowException("The field must be initialized before usage."); 
            set => _currentSuggestion = value; 
        }

        public BallGameExpertSystem(IBallGameKnowledgeBase knowledgeBase,
            IBallGameInferenceEngine inferenceEngine, IUserInterface userInterface)
        {
            _knowledgeBase = knowledgeBase;
            _userInterface = userInterface;
            _inferenceEngine = inferenceEngine;
        }

        public int Conclude()
        {
            while (true)
            {
                CharacteristicValue input;

                switch (_inferenceEngine.CurrentState)
                {
                    case IBallGameInferenceEngine.State.NoContext:
                        _inferenceEngine.SetContext(_knowledgeBase);
                        break;

                    case IBallGameInferenceEngine.State.Ready:
                        BallGameCharacteristic initial = _knowledgeBase.Initial;
                        input = AskForValue(initial);
                        CurrentSuggestion = _inferenceEngine.Infer(input);
                        break;

                    case IBallGameInferenceEngine.State.MultipleSuggestions:
                        input = AskForValue(CurrentSuggestion!.Characteristic);
                        CurrentSuggestion = _inferenceEngine.Infer(input);
                        break;

                    case IBallGameInferenceEngine.State.SingleSuggestion:
                        input = AskForValue(CurrentSuggestion!);
                        CurrentSuggestion = _inferenceEngine.Infer(input);
                        break;

                    case IBallGameInferenceEngine.State.Conclusion:
                        _userInterface.ShowMessage(_inferenceEngine.Conclusion);
                        return 0;

                    default:
                        return 1;
                }
            }
        }

        private CharacteristicValue AskForValue(BallGameCharacteristic characteristic)
        {
            IDictionary<int, string> options = characteristic.PossibleValues;

            int chosenOption = _userInterface.AskWithOptions(
                $"From the options below, select the one that best describes " +
                $"{characteristic.Name.ToLower()}:", options);

            if (!options.ContainsKey(chosenOption))
                throw new System.Exception(
                    "Invalid option. The options dictionary couldn't find the provided key. " +
                    "Probably the current interface implementation didn't verify user input.");
            else
                return new CharacteristicValue(characteristic, chosenOption);
        }

        private CharacteristicValue AskForValue(CharacteristicValue characteristicValue)
        {
            BallGameCharacteristic characteristic = characteristicValue.Characteristic;
            int value = characteristicValue.Value;

            IDictionary<int, string> options = characteristic.PossibleValues;

            bool answerIsPositive = _userInterface.AskClosedQuestion(
                $"Is {characteristic.Name.ToLower()} equal to {options[value].ToLower()}?");

            if (answerIsPositive)
                return characteristicValue;
            else
                return new CharacteristicValue(characteristic, -1);
        }
    }
}
