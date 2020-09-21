using System;
using System.Collections.Generic;
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

        //[Test]
        public void TestCharacter()
        {
            var expectedOutput = TL.Scan("a");
            foreach (var inst in expectedOutput)
            {
                Assert.AreEqual(inst.Value, "a");
                Assert.AreEqual(inst.TokenType, TokenTypeEnum.Variables);
            }
        }
        
        [Test]
        public void TestSingleDigit()
        {
            var expected = TL.Scan("3");

            List<Token> actual = new List<Token>()
            {
                new Token("3", TokenTypeEnum.Numbers)
            };

            //CollectionAssert.AreEqual(expected, actual);
            foreach (var inst in expected)
            {
                Assert.AreEqual(inst.Value, actual[0].Value);
                Assert.AreEqual(inst.TokenType, actual[0].TokenType);
            }

        }
        //[Test]
        public void TestSingleDigits()
        {
            var expectedOutput = TL.Scan("3");
            foreach (var inst in expectedOutput)
            {
                Assert.AreEqual(inst.Value, "3");
                Assert.AreEqual(inst.TokenType, TokenTypeEnum.Numbers);
            }
        }
    }
}