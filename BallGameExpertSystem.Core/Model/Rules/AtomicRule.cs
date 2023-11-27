namespace BallGameExpertSystem.Core.Model.Rules
{
    public class AtomicRule : Rule
    {
        private CharacteristicValue _value;
        public CharacteristicValue CharacteristicValue => _value;

        public override List<Rule>? Predcessors => null;
        public override List<Rule>? Successors { get; } = new List<Rule>();
        public override int Depth => 0;
        protected override int CalculateDepth() => 0;

        public AtomicRule(CharacteristicValue characteristicValue)
        {
            _value = characteristicValue;
        }

        public override void Update(IEnumerable<CharacteristicValue>? characteristicValues)
        {
            if (characteristicValues == null) return;

            if (characteristicValues.Contains(_value))
                IsObserved = true;
            else
                IsObserved = false;
        }
    }
}
