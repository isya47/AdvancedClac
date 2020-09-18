using System;
using System.Collections.Generic;
using AdvancedClac;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace TestProject1
{
    public class Calctests
    {
        Tokenizer TL = new Tokenizer();
        
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
            try
            {
                var obj = MathFunc.Eval(MathFunc.Parsing(TL.Scan("1")));
            }
            catch (Exception ae)
            {
                Assert.AreEqual( "Unknown symbol while tokenizing", ae.Message );
            }
        }
        [Test]
        public void TestAddition()
        {
            try
            {
                var obj = MathFunc.Eval(MathFunc.Parsing(TL.Scan("1+2")));
            }
            catch (Exception ae)
            {
                Assert.AreEqual( "Unknown symbol while tokenizing", ae.Message );
            }
        }
        [Test]
        public void TestMultiplication()
        {
            try
            {
                var obj = MathFunc.Eval(MathFunc.Parsing(TL.Scan("3*2")));
            }
            catch (Exception ae)
            {
                Assert.AreEqual( "Unknown symbol while tokenizing", ae.Message );
            }
        }
        
        [Test]
        public void TestSubtraction()
        {
            try
            {
                var obj = MathFunc.Eval(MathFunc.Parsing(TL.Scan("3-2")));
            }
            catch (Exception ae)
            {
                Assert.AreEqual( "Unknown symbol while tokenizing", ae.Message );
            }
        }
        [Test]
        public void Testdivision()
        {
            try
            {
                var obj = MathFunc.Eval(MathFunc.Parsing(TL.Scan("10/2")));
            }
            catch (Exception ae)
            {
                Assert.AreEqual( "Unknown symbol while tokenizing", ae.Message );
            }
        }
        [Test]
        public void TestTrigonometry()
        {
            try
            {
                var obj = MathFunc.Eval(MathFunc.Parsing(TL.Scan("sin(3+1)")));
            }
            catch (Exception ae)
            {
                Assert.AreEqual( "Unknown symbol while tokenizing", ae.Message );
            }
        }
        [Test]
        public void TestAddition1()
        {
            try
            {
                var obj = MathFunc.Eval(MathFunc.Parsing(TL.Scan("1.1+2.5")));
            }
            catch (Exception ae)
            {
                Assert.AreEqual( "Unknown symbol while tokenizing", ae.Message );
            }
        }
        [Test]
        public void TestAddition2()
        {
            try
            {
                var obj = MathFunc.Eval(MathFunc.Parsing(TL.Scan("4+11.5")));
            }
            catch (Exception ae)
            {
                Assert.AreEqual( "Unknown symbol while tokenizing", ae.Message );
            }
        }
        [Test]
        public void TestSubtraction1()
        {
            try
            {
                var obj = MathFunc.Eval(MathFunc.Parsing(TL.Scan("3.2-")));
            }
            catch (Exception ae)
            {
                Assert.AreEqual( "Unknown symbol while tokenizing", ae.Message );
            }
        }
    }
}