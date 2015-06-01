using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheSavannah.Fuzzy
{
    class FuzzyVariable
    {
        
        private Dictionary<string, FuzzySet> members = new Dictionary<string, FuzzySet>();
        private int minRange;
        private int maxRange;

        public FuzzyVariable(int minrange, int maxrange)
        {
            minRange = minrange;
            maxRange = maxrange;
        }

        public void AddSet(string s, FuzzySet f)
        {
            members.Add(s, f);            
        }

        public void Fuzzify(double value)
        {
            foreach (KeyValuePair<string, FuzzySet> set in members)
            {
                set.Value.CalculateDOM(value);
            }
        }

        public void ClearDOMs()
        {
            foreach (KeyValuePair<string, FuzzySet> set in members)
            {
                set.Value.ClearDOM();
            }
        }
        public double DefuzzifyMaxAv()
        {
            double total = 0;
            double mul = 0;
            foreach (KeyValuePair<string, FuzzySet> s in members)
            {
                FuzzySet set = s.Value;
                double max = set.GetMax(minRange, maxRange);
                total += (max*set.DOM);
                mul += set.DOM;
                Console.WriteLine("Pair: {0}, total: {1} mul:{2}", s.Key, max, set.DOM);
            }
            Console.WriteLine("Variable Defuzzify: total = " + total + ", mul = " + mul + ", results in: " + total/mul);
            double d = total/mul;
            if (Double.IsNaN(d))
                return 0;
            return d;
        }
    }
}
