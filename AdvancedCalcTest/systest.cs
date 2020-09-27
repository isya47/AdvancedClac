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
        public void TestSingle()
        {
            Assert.AreEqual("1", MathFunc.Eval(MathFunc.Parsing(TL.Scan("1"))));
            
        }
        [Test]
        public void TestAddition()
        {
            Assert.AreEqual( "3", MathFunc.Eval(MathFunc.Parsing(TL.Scan("1+2"))) );
        }
        [Test]
        public void TestMultiplication()
        {
            Assert.AreEqual( "6", MathFunc.Eval(MathFunc.Parsing(TL.Scan("3*2"))) );
        }
        
        [Test]
        public void TestSubtraction()
        {
            Assert.AreEqual( "1", MathFunc.Eval(MathFunc.Parsing(TL.Scan("3-2"))) );
        }
        [Test]
        public void TestDivision()
        {
            Assert.AreEqual( "5", MathFunc.Eval(MathFunc.Parsing(TL.Scan("10/2"))) );
        }
        [Test]
        public void TestTrigonometry()
        {
            Assert.AreEqual( "-0.7568024953079282",  MathFunc.Eval(MathFunc.Parsing(TL.Scan("sin(3+1)"))) );
        }
        [Test]
        public void TestAddition1()
        {
            Assert.AreEqual( "3.6", MathFunc.Eval(MathFunc.Parsing(TL.Scan("1.1+2.5"))));
        }
        [Test]
        public void TestAddition2()
        {
 
                Assert.AreEqual( "15.5", MathFunc.Eval(MathFunc.Parsing(TL.Scan("4+11.5"))) );
        }
        [Test]
        public void TestSubtraction1()
        {
            Assert.AreEqual( "1.1", MathFunc.Eval(MathFunc.Parsing(TL.Scan("3.2-2.1"))));
        }
        [Test]
        public void TestSubtraction2()
        {
            Assert.AreEqual( "0.2", MathFunc.Eval(MathFunc.Parsing(TL.Scan("3.2-3"))));
        }
        [Test]
        public void TestMultiplication1()
        {
            try
            {
                var obj = MathFunc.Eval(MathFunc.Parsing(TL.Scan("3.2*3.4")));
            }
            catch (Exception ae)
            {
                Assert.AreEqual( "Unknown symbol while tokenizing", ae.Message );
            }
        }
        [Test]
        public void TestMultiplication2()
        {
            try
            {
                var obj = MathFunc.Eval(MathFunc.Parsing(TL.Scan("3.2*3")));
            }
            catch (Exception ae)
            {
                Assert.AreEqual( "Unknown symbol while tokenizing", ae.Message );
            }
        }
        [Test]
        public void TestDivision1()
        {
            try
            {
                var obj = MathFunc.Eval(MathFunc.Parsing(TL.Scan("4.4/3.2")));
            }
            catch (Exception ae)
            {
                Assert.AreEqual( "Unknown symbol while tokenizing", ae.Message );
            }
        }
        [Test]
        public void TestDivision2()
        {
            try
            {
                var obj = MathFunc.Eval(MathFunc.Parsing(TL.Scan("3/3.5")));
            }
            catch (Exception ae)
            {
                Assert.AreEqual( "Unknown symbol while tokenizing", ae.Message );
            }
        }
        [Test]
        public void TestCombined1()
        {
            try
            {
                var obj = MathFunc.Eval(MathFunc.Parsing(TL.Scan("-3*pow(2+2,-4/2)")));
            }
            catch (Exception ae)
            {
                Assert.AreEqual( "Unknown symbol while tokenizing", ae.Message );
            }
        }
        [Test]
        public void TestSin()
        {
            var actual = MathFunc.Eval(MathFunc.Parsing(TL.Scan("sin(30)")));
            Assert.AreEqual( "-0.9880316240928618", actual);
        }
        
    }
}