namespace BallGameExpertSystem.Core.Model.Rules
{
    public abstract class Rule
    {
        private bool isObserved;

        public virtual List<Rule>? Predecessors { get; }
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
            if (Predecessors == null ||
                Predecessors.Count == 0)
                return 0;
            else
                return Predecessors.Max(x => x.Depth) + 1;
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

        public virtual bool IsPredecessorOf(Rule other)
        {
            if (Successors == null)
                return false;
            else if (Successors.Contains(other)
                    || Successors.Any(s => s.IsPredecessorOf(other)))
                return true;

            return false;
        }

        public virtual void AddPredecessor(Rule predecessor)
        {
            Predecessors?.Add(predecessor);
            predecessor.Successors?.Add(this);
        }

        public virtual void DisjointPredecessor(Rule predecessor)
        {
            predecessor.Successors?.Remove(this);
            Predecessors?.Remove(predecessor);
        }
    }
}
