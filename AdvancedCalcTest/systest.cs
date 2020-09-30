using System;
using System.Collections.Generic;
using AdvancedClac;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace TestProject1
{
    [TestFixture]
    public class Calctests
    {
        Tokenizer TL;
        
        [SetUp]
        public void SetUp()
        {
            TL = new Tokenizer();
        }
        
        [TearDown]
        public void CleanUp() 
        {
            GC.Collect(GC.MaxGeneration);
            TL = null;
        }
        
        [Test]
        public void TestEmptyString()
        {
            try
            {
                var obj = MathFunc.Eval(MathFunc.Parsing(TL.Scan(" ")));
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
                var obj = MathFunc.Eval(MathFunc.Parsing(TL.Scan("#")));
            }
            catch (Exception ae)
            {
                Assert.AreEqual( "Unknown symbol while tokenizing", ae.Message );
            }
        }
        
        [Test]
        public void TestWholeNumber()
        {
            Assert.AreEqual("1", MathFunc.Eval(MathFunc.Parsing(TL.Scan("1"))));
        }

        [Test]
        public void TestWholeAddition()
        {
            var output = MathFunc.Eval(MathFunc.Parsing(TL.Scan("25+32")));
            Assert.AreEqual("57",output);
        }
        
        [Test]
        public void TestWholeSubstraction()
        {
            var output = MathFunc.Eval(MathFunc.Parsing(TL.Scan("1000-999")));
            Assert.AreEqual("1",output);
        }
        
        [Test]
        public void TestWholeMult()
        {
            var output = MathFunc.Eval(MathFunc.Parsing(TL.Scan("122*14")));
            Assert.AreEqual("1708",output);
        }
        
        [Test]
        public void TestWholeDivision()
        {
            var output = MathFunc.Eval(MathFunc.Parsing(TL.Scan("5684/4")));
            Assert.AreEqual("1421",output);
        }
        
        [Test]
        public void TestWholeBrackets()
        {
            var output = MathFunc.Eval(MathFunc.Parsing(TL.Scan("2*(4-3)")));
            Assert.AreEqual("2",output);
        }
        
        [Test]
        public void TestWholeSin()
        {
            var output = MathFunc.Eval(MathFunc.Parsing(TL.Scan("sin(30)")));
            Assert.AreEqual("-0.9880316240928618",output);
        }
        
        [Test]
        public void TestWholeCos()
        {
            var output = MathFunc.Eval(MathFunc.Parsing(TL.Scan("cos(30)")));
            Assert.AreEqual("0.15425144988758405",output);
        }
        
        [Test]
        public void TestWholeTan()
        {
            var output = MathFunc.Eval(MathFunc.Parsing(TL.Scan("tan(50)")));
            Assert.AreEqual("-0.2719006119976307",output);
        }
        
        [Test]
        public void TestWholePow()
        {
            var output = MathFunc.Eval(MathFunc.Parsing(TL.Scan("pow(2,3)")));
            Assert.AreEqual("8",output);
        }
        
        [Test]
        public void TestWholeNegative()
        {
            var output = MathFunc.Eval(MathFunc.Parsing(TL.Scan("-4+2")));
            Assert.AreEqual("-2",output);
        }
        
        [Test]
        public void TestWholeZero()
        {
            var output = MathFunc.Eval(MathFunc.Parsing(TL.Scan("0*5")));
            Assert.AreEqual("0",output);
        }
        
        [Test]
        public void TestWholeOr()
        {
            var output = MathFunc.Eval(MathFunc.Parsing(TL.Scan("60|13")));
            Assert.AreEqual("61",output);
        }
        
        [Test]
        public void TestWholeAnd()
        {
            var output = MathFunc.Eval(MathFunc.Parsing(TL.Scan("60&13")));
            Assert.AreEqual("12",output);
        }
        
        [Test]
        public void TestWholeXor()
        {
            var output = MathFunc.Eval(MathFunc.Parsing(TL.Scan("60^13")));
            Assert.AreEqual("49",output);
        }
        
        [Test]
        public void TestWholeOnesCompl()
        {
            var output = MathFunc.Eval(MathFunc.Parsing(TL.Scan("~60")));
            Assert.AreEqual("-61",output);
        }
        
        
        /// 
        /// ТЕСТЫ ДЕСЯТИЧНЫХ ЧИСЕЛ
        ///
        
        
        [Test]
        public void TestDecimalNumber()
        {
            var output = MathFunc.Eval(MathFunc.Parsing(TL.Scan("0.5")));
            Assert.AreEqual("0.5", output);
        }

        [Test]
        public void TestDecimalAddition()
        {
            var output = MathFunc.Eval(MathFunc.Parsing(TL.Scan("0.25+0.5")));
            Assert.AreEqual("0.75",output);
        }
        
        [Test]
        public void TestDeciamlSubstraction()
        {
            var output = MathFunc.Eval(MathFunc.Parsing(TL.Scan("0.6-0.4")));
            Assert.AreEqual("0.2",output);
        }
        
        [Test]
        public void TestDecimalMult()
        {
            var output = MathFunc.Eval(MathFunc.Parsing(TL.Scan("0.4*0.5")));
            Assert.AreEqual("0.2",output);
        }
        
        [Test]
        public void TestDecimalDivision()
        {
            var output = MathFunc.Eval(MathFunc.Parsing(TL.Scan("1.6/0.5")));
            Assert.AreEqual("3.2",output);
        }
        
        [Test]
        public void TestDecimalBrackets()
        {
            var output = MathFunc.Eval(MathFunc.Parsing(TL.Scan("0.5*(1.5-0.3)")));
            Assert.AreEqual("0.6",output);
        }
        
        [Test]
        public void TestDecimalSin()
        {
            var output = MathFunc.Eval(MathFunc.Parsing(TL.Scan("sin(0.5)")));
            Assert.AreEqual("0.479425538604203",output);
        }
        
        [Test]
        public void TestDecimalCos()
        {
            var output = MathFunc.Eval(MathFunc.Parsing(TL.Scan("cos(0.5)")));
            Assert.AreEqual("0.8775825618903728",output);
        }
        
        [Test]
        public void TestDecimalTan()
        {
            var output = MathFunc.Eval(MathFunc.Parsing(TL.Scan("tan(0.5)")));
            Assert.AreEqual("0.5463024898437905",output);
        }
        
        [Test]
        public void TestDecimalPow()
        {
            var output = MathFunc.Eval(MathFunc.Parsing(TL.Scan("pow(0.5,0.3)")));
            Assert.AreEqual("0.8122523963562356",output);
        }
        
        [Test]
        public void TestDecimalNegative()
        {
            var output = MathFunc.Eval(MathFunc.Parsing(TL.Scan("-0.5+1")));
            Assert.AreEqual("0.5",output);
        }
        
        [Test]
        public void TestDecimalZero()
        {
            var output = MathFunc.Eval(MathFunc.Parsing(TL.Scan("0*0.55")));
            Assert.AreEqual("0",output);
        }
        
        
        [Test]
        public void TestDecimalOr()
        {
            try
            {
                var obj = MathFunc.Eval(MathFunc.Parsing(TL.Scan("0.5|0.3")));
            }
            catch (Exception ae)
            {
                Assert.AreEqual( "Invalid Antiunitary Operation", ae.Message );
            }
        }
        
        [Test]
        public void TestDecimalAnd()
        {
            try
            {
                var obj = MathFunc.Eval(MathFunc.Parsing(TL.Scan("0.5&0.3")));
            }
            catch (Exception ae)
            {
                Assert.AreEqual( "Invalid Antiunitary Operation", ae.Message );
            }
        }
        
        [Test]
        public void TestDecimalXor()
        {
            try
            {
                var obj = MathFunc.Eval(MathFunc.Parsing(TL.Scan("0.5^0.3")));
            }
            catch (Exception ae)
            {
                Assert.AreEqual( "Invalid Antiunitary Operation", ae.Message );
            }
        }
        
        [Test]
        public void TestDecimalOnesCompl()
        {
            try
            {
                var obj = MathFunc.Eval(MathFunc.Parsing(TL.Scan("~0.5")));
            }
            catch (Exception ae)
            {
                Assert.AreEqual( "Invalid Unitary Operation", ae.Message );
            }
        }
        
        
    }
}