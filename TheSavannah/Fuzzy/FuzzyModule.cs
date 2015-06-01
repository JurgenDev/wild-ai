using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using TheSavannah.Fuzzy;

namespace TheSavannah
{
    class FuzzyModule
    {
        private Dictionary<string, FuzzyVariable> variables = new Dictionary<string, FuzzyVariable>();
        private List<FuzzyRule> rules = new List<FuzzyRule>();

        public void RunRules()
        {
            foreach (FuzzyRule rule in rules)
            {
                rule.Calculate();
            }
        }

        public void AddVariable(string name, FuzzyVariable fvar)
        {
            variables.Add(name, fvar);
        }

        public void AddRule(FuzzyRule frul)
        {
            rules.Add(frul);
        }

        public void Fuzzify(string name, double value)
        {
            FuzzyVariable f;
            variables.TryGetValue(name,out f);
            if(f != null)
                f.Fuzzify(value);
        }

        public double DeFuzzify(string name)
        {
            FuzzyVariable f;
            variables.TryGetValue(name, out f);
            if (f != null)
            {
                double output = f.DefuzzifyMaxAv();
                Console.WriteLine("Module defuzzify: " + output);
                f.ClearDOMs();
                return output;
            }
            Console.WriteLine("Could not find Defuzzify target");
            return 0;
        }
    }
}
