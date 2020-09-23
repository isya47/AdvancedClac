using System;
using System.Collections.Generic;
using System.Linq;
using AdvancedClac;
using FluentAssertions;
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
                Assert.AreEqual("Empty String", ae.Message);
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
                Assert.AreEqual("Unknown symbol while tokenizing", ae.Message);
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

        //РАБОТАЕТ ЕСЛИ ПРОАПГРЕЙДИТЬ КЛАСС ТОКЕНОВ
        [Test]
        public void TestSingleDigit()
        {
            var actual = TL.Scan("3");

            List<Token> expected = new List<Token>()
            {
                new Token("3", TokenTypeEnum.Numbers)
            };

            CollectionAssert.AreEqual(expected, actual);
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

        [Test]
        public void TestDigits()
        {
            var T = new Tokenizer();
            var actual = T.Scan("13");

            List<Token> expected = new List<Token>()
            {
                new Token("13", TokenTypeEnum.Numbers)
            };
        
            CollectionAssert.AreEqual(expected, actual);
        }
    }
}