using BallGameExpertSystem.Core.KnowledgeBase.Interfaces;
using BallGameExpertSystem.Core.Model;

namespace BallGameExpertSystem.Core.InferenceEngine.Interfaces
{
    public interface IBallGameInferenceEngine
    {
        public enum State
        {
            NoContext = -1,
            Ready = 0,
            SingleSuggestion = 1,
            MultipleSuggestions = 2,
            Conclusion = 4
        }

        State CurrentState { get; }
        string Conclusion { get; }

        /// <summary>
        /// Suggests the best characteristic to ask.
        /// Check CurrentState for additional details.
        /// </summary>
        /// <param name="input">
        /// A key-value pair of the asked characteristic and its user-assigned value.
        /// </param>
        /// <param name="suggestion">
        /// Value suggestion for more specific question.
        /// Only initialized in SingleSuggestion state.
        /// </param>
        /// <returns>
        /// A CharacteristicValue pair object initialized with:<br/>
        /// 1) suggested characteristic and -1 as an uninitialized value;<br/>
        /// 2) suggested characteristic and suggested value;<br/>
        /// 3) null if there is nothing to suggest.
        /// </returns>
        CharacteristicValue? Infer(CharacteristicValue input);

        /// <summary>
        /// You must set data context before using the engine.
        /// </summary>
        /// <param name="knowledgeBase">
        /// An initialized knowledge base.
        /// </param>
        void SetContext(IBallGameKnowledgeBase knowledgeBase);
        void Reset();
    }
}