using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheSavannah.Fuzzy
{
    class FuzzyRule
    {
        private FuzzyTerm antecedent;
        private FuzzyTerm consequent;

        public FuzzyRule(FuzzyTerm ant, FuzzyTerm cons)
        {
            antecedent = ant;
            consequent = cons;
        }
        public void Calculate()
        {
            consequent.ORWithDOM(antecedent.GetDOM());
        }
    }
}
