namespace BallGameExpertSystem.Core.Model.Rules;

    public class AtomicRule : Rule, IEquatable<AtomicRule>
    {
        private CharacteristicValue _value;
        public CharacteristicValue CharacteristicValue => _value;

        public override List<Rule>? Predecessors => null;
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

        public override void AddPredecessor(Rule predecessor)
        {
            throw new MissingMethodException(
                $"Predecessor-related methods are disabled for {nameof(AtomicRule)} class.");
        }

        public override void DisjointPredecessor(Rule predecessor)
        {
            throw new MissingMethodException(
                $"Predecessor-related methods are disabled for {nameof(AtomicRule)} class.");
        }

        public bool Equals(AtomicRule? other)
        {
            return other != null 
                && _value.Equals(other._value);
        }

        public override bool Equals(object? obj) => Equals(obj as AtomicRule);

        public override int GetHashCode() => _value.GetHashCode();
    }

