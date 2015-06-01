using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheSavannah.Fuzzy
{
    class FuzzyOR : FuzzyTerm
    {
        private FuzzyTerm operand1;
        private FuzzyTerm operand2;

        public FuzzyOR(FuzzyTerm op1, FuzzyTerm op2)
        {
            operand1 = op1;
            operand2 = op2;
        }

        public override double GetDOM()
        {
            return Math.Max(operand1.GetDOM(), operand2.GetDOM());
        }

        public override void ORWithDOM(double d)
        {
            //double max = Math.Max(operand1.GetDOM(), operand2.GetDOM());
            //return Math.Max(max, d);
        }
    }

    class FuzzyAND : FuzzyTerm
    {
        private FuzzyTerm operand1;
        private FuzzyTerm operand2;

        public FuzzyAND(FuzzyTerm op1, FuzzyTerm op2)
        {
            operand1 = op1;
            operand2 = op2;
        }
        public override double GetDOM()
        {
            return Math.Min(operand1.GetDOM(), operand2.GetDOM());
        }

        public override void ORWithDOM(double d)
        {
            //double max = Math.Min(operand1.GetDOM(), operand2.GetDOM());
            //return Math.Max(max, d);
        }
    }
}
