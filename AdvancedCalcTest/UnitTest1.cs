using System;
using AdvancedClac;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace TestProject1
{
    public class Tests
    {
        Tokenizer TL = new Tokenizer();
        
        [Test]
        public void TestEmptyString()
        {
            try
            {
                var obj = TL.Scan(" ");
            }
            catch (Exception ae)
            {
                Assert.AreEqual( "Empty String", ae.Message );
            }
        }
        [Test]
        public void TestInvalidSymbol()
        {
            try
            {
                var obj = TL.Scan("#");
            }
            catch (Exception ae)
            {
                Assert.AreEqual( "Unknown symbol while tokenizing", ae.Message );
            }
        }
    }
}