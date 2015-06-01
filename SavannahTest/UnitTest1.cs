using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TheSavannah;
using TheSavannah.Fuzzy;

namespace SavannahTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestShoulder()
        {
            
            TheSavannah.Fuzzy.FuzzySet s = new FuzzySet_Triangle(50, 25);
            List<double> numbers = new List<double>();
            for (int i = 0; i <= 100; i++)
            {
                s.CalculateDOM(i);
                numbers.Add(s.GetDOM());
            }
            int n = 0;
        }
    }
}
