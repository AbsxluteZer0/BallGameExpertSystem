namespace BallGameExpertSystem.Core.Model.Rules
{
    public abstract class Rule
    {
        private bool isObserved;

        public virtual List<Rule>? Predcessors { get; }
        public virtual List<Rule>? Successors { get; }
        public virtual bool IsObserved
        {
            get => isObserved;
            protected set 
            { 
                if (!IsLocked) 
                    isObserved = value; 
            }
        }
        public virtual bool IsLocked { get; private set; }
        public virtual int Depth => CalculateDepth();

        protected virtual int CalculateDepth()
        {
            if (Predcessors == null ||
                Predcessors.Count == 0)
                return 0;
            else
                return Predcessors.First()
                    .CalculateDepth() + 1;
        }

        public abstract void Update(IEnumerable<CharacteristicValue>? characteristicValues = null);

        public void LockObserved()
        {
            IsObserved = true;
            IsLocked = true;
        }

        public void Unlock()
        {
            IsLocked = false;
        }

        public virtual bool IsPredcessorOf(Rule other)
        {
            if (Successors == null)
                return false;
            else if (Successors.Contains(other)
                    || Successors.Any(s => s.IsPredcessorOf(other)))
                return true;

            return false;
        }
    }
}
