using BallGameExpertSystem.Core.KnowledgeBase.Interfaces;
using BallGameExpertSystem.Core.Model.Characteristics;
using BallGameExpertSystem.Core.Model.Rules;

namespace BallGameExpertSystem.Core.KnowledgeBase
{
    public class BallGameKnowledgeBase : IBallGameKnowledgeBase
    {
        /// <summary>
        /// Area of definition of the system.<br/>
        /// One and only one characteristic must have 
        /// priority equal to 0 — it is the initial 
        /// characteristic for a knowledge base.
        /// </summary>
        public List<BallGameCharacteristic> Characteristics { get; } 
            = new List<BallGameCharacteristic>();

        public List<Rule> Rules { get; } = new List<Rule>();
        
        public BallGameCharacteristic Initial => Characteristics.Single(c => c.Priority == 0);

        /// <summary>
        /// Adds a single characteristic with the specified priority.
        /// </summary>
        /// <param name="characteristic">
        /// Characteristics within the knowledge base must be unique.
        /// </param>
        /// <param name="priority">
        /// There must be only one characteristic with priority = 0.
        /// </param>
        /// <exception cref="ArgumentException">
        /// Is thrown if a parameter doesn't meet the requirements.
        /// </exception>
        public void AddCharacteristic(BallGameCharacteristic characteristic, int priority)
        {
            if (priority == 0
                && Characteristics
                    .Where(c => c.Priority == 0)
                    .Any())
            {
                throw new ArgumentException(
                    "There already is a characteristic with priority equal to 0", 
                    nameof(priority));
            }

            if (Characteristics.Contains(characteristic))
            {
                throw new ArgumentException(
                    "The characteristic is already in the knowledge base",
                    nameof(characteristic));
            }

            characteristic.Priority = priority;
            Characteristics.Add(characteristic);
            PopulateWithBasicRulesFor(characteristic);
        }

        private void PopulateWithBasicRulesFor(BallGameCharacteristic characteristic)
        {
            foreach (var value in characteristic.PossibleValues)
            {
                Rules.Add(new AtomicRule(
                        new Model.CharacteristicValue(characteristic, value.Key)));
            }
        }

        /// <summary>
        /// Adds a batch of characteristics. 
        /// The priority must be specified beforehand.
        /// </summary>
        /// <param name="characteristic">
        /// Characteristics within the knowledge base must be unique.
        /// </param>
        /// <param name="priority">
        /// There must be only one characteristic with priority = 0.
        /// </param>
        /// <exception cref="ArgumentException">
        /// Is thrown if a parameter doesn't meet the requirements.
        /// </exception>
        public void AddCharacteristics(IEnumerable<BallGameCharacteristic> characteristics)
        {
            var list = characteristics.ToList();

            foreach (var characteristic in list)
            {
                if (Characteristics.Contains(characteristic))
                    list.Remove(characteristic);
            }

            int zeroPrioriryCharacteristicsCount
                = characteristics.Where(c => c.Priority == 0)
                                 .Count();

            if (zeroPrioriryCharacteristicsCount > 0)
            {
                if (Characteristics.Where(c => c.Priority == 0)
                                   .Any())
                    throw new ArgumentException("There already is a characteristic with priority = 0.");

                if (zeroPrioriryCharacteristicsCount > 1)
                    throw new ArgumentException("The are more than 1 characteristic with priority = 0.");
            }

            Characteristics.AddRange(list);
            list.ForEach(ch => PopulateWithBasicRulesFor(ch));
        }

        public void AddRule(Rule rule)
        {
            if (rule.Predecessors != null)
            {
                var predecessors = new List<Rule>(rule.Predecessors);

                foreach (var predecessor in predecessors)
                {
                    AddRule(predecessor);
                    Rule actualPredecessor = Actualize(predecessor); // get the actual predecessor from the knowledge base
                    rule.DisjointPredecessor(predecessor);           // remove the obsolete predecessor
                    rule.AddPredecessor(actualPredecessor);          // link the actual predecessor to the rule
                }
            }

            if (!Rules.Contains(rule))                               // add if the rule isn't in the knowledge base
                Rules.Add(rule);
        }

        /// <summary>
        /// Searches the knowledge base for the rule,
        /// adds it if it isn't there and returns the result.
        /// </summary>
        /// <param name="rule">The rule to actualize with the knowledge bas e.</param>
        /// <returns>The actual rule object in the knowledge base.</returns>
        private Rule Actualize(Rule rule)
        {
            Rule? found = Rules.FirstOrDefault(r => r.Equals(rule));

            if (found != null)
                return found;

            Rules.Add(rule);
            return rule;
        }
    }
}
