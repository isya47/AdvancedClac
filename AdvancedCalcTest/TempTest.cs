using System;
using System.Collections.Generic;
using AdvancedClac;
using FluentAssertions;
using NUnit.Framework;


namespace TestProject1
{
    [TestFixture]
    public class Test
    {
        private Tokenizer _t;
        private List<Token> _expected;
        
        [SetUp]
        public void SetUp()
        {
            _t = new Tokenizer();
            _expected = new List<Token>();
        }
        
        [TearDown]
        public void CleanUp() 
        { 
            GC.Collect();
            _t = null;
            _expected = null;
        }
        
        
        [Test]
        public void TestCharacter()
        {
            var actual = _t.Scan("a");
            _expected.Add(new Token("a", TokenTypeEnum.Variables));
            CollectionAssert.AreEqual(_expected, actual);
            
        }
        
        [Test]
        public void TestSingleDigit()
        {
            var actual = _t.Scan("3");

            _expected.Add(new Token("3", TokenTypeEnum.Numbers));
            CollectionAssert.AreEqual(_expected, actual);
        }
        
        [Test]
        public void TestDecimal()
        {
            var actual = _t.Scan("0.52");
            _expected.Add(new Token("0.52", TokenTypeEnum.Numbers));
            CollectionAssert.AreEqual(_expected, actual);
        }
       
    }
}