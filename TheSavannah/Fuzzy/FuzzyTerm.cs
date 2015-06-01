using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheSavannah.Fuzzy
{
    public abstract class FuzzyTerm
    {
        public abstract double GetDOM();
        public abstract void ORWithDOM(double d);
    }
}
