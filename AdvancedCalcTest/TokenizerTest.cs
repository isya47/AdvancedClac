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
        Tokenizer T = new Tokenizer();

        [Test]
        public void TestEmptyString()
        {
            try
            {
                var obj = T.Scan(" ");
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
                var obj = T.Scan("#");
            }
            catch (Exception ae)
            {
                Assert.AreEqual("Unknown symbol while tokenizing", ae.Message);
            }
        }

        [Test]
        public void TestCharacter()
        {
            var actual = T.Scan("a");

            List<Token> expected = new List<Token>()
            {
                new Token("a", TokenTypeEnum.Variables)
            };

            CollectionAssert.AreEqual(expected, actual);
        }
        
        //РАБОТАЕТ ЕСЛИ ПРОАПГРЕЙДИТЬ КЛАСС ТОКЕНОВ
        [Test]
        public void TestSingleDigit()
        {
            var actual = T.Scan("3");

            List<Token> expected = new List<Token>()
            {
                new Token("3", TokenTypeEnum.Numbers)
            };

            CollectionAssert.AreEqual(expected, actual);
        }
        
        [Test]
        public void TestDecimal()
        {
            var actual = T.Scan("0.52");

            List<Token> expected = new List<Token>()
            {
                new Token("0.52", TokenTypeEnum.Numbers)
            };

            CollectionAssert.AreEqual(expected, actual);
        }
        
        [Test]
        public void TestDigits()
        {
          
            var actual = T.Scan("13");

            List<Token> expected = new List<Token>()
            {
                new Token("13", TokenTypeEnum.Numbers)
            };
        
            CollectionAssert.AreEqual(expected, actual);
        }
        
        [Test]
        public void TestCharacterAndNumber()
        {
            var actual = T.Scan("2a");

            List<Token> expected = new List<Token>()
            {
                new Token("2", TokenTypeEnum.Numbers),
                new Token("a", TokenTypeEnum.Variables)
            };
        
            CollectionAssert.AreEqual(expected, actual);
        }
        
        [Test]
        public void TestAdditionNumbers()
        {
            var actual = T.Scan("23 + 551");

            List<Token> expected = new List<Token>()
            {
                new Token("23", TokenTypeEnum.Numbers),
                new Token("+", TokenTypeEnum.Operators),
                new Token("551", TokenTypeEnum.Numbers)
            };
        
            CollectionAssert.AreEqual(expected, actual);
        }
        
        [Test]
        public void TestAdditionVariables()
        {
          
            var actual = T.Scan("a + b");

            List<Token> expected = new List<Token>()
            {
                new Token("a", TokenTypeEnum.Variables),
                new Token("+", TokenTypeEnum.Operators),
                new Token("b", TokenTypeEnum.Variables)
            };
        
            CollectionAssert.AreEqual(expected, actual);
        }
        
        [Test]
        public void TestMultNumbers()
        {
          
            var actual = T.Scan("55 * 21");

            List<Token> expected = new List<Token>()
            {
                new Token("55", TokenTypeEnum.Numbers),
                new Token("*", TokenTypeEnum.Operators),
                new Token("21", TokenTypeEnum.Numbers)
            };
        
            CollectionAssert.AreEqual(expected, actual);
        }
        
        [Test]
        public void TestMultVariables()
        {
          
            var actual = T.Scan("a * b");

            List<Token> expected = new List<Token>()
            {
                new Token("a", TokenTypeEnum.Variables),
                new Token("*", TokenTypeEnum.Operators),
                new Token("b", TokenTypeEnum.Variables)
            };
        
            CollectionAssert.AreEqual(expected, actual);
        }
        
        [Test]
        public void TestDivNumbers()
        {
          
            var actual = T.Scan("55 / 21");

            List<Token> expected = new List<Token>()
            {
                new Token("55", TokenTypeEnum.Numbers),
                new Token("/", TokenTypeEnum.Operators),
                new Token("21", TokenTypeEnum.Numbers)
            };
        
            CollectionAssert.AreEqual(expected, actual);
        }
        [Test]
        public void TestDivVariables()
        {
          
            var actual = T.Scan("a / b");

            List<Token> expected = new List<Token>()
            {
                new Token("a", TokenTypeEnum.Variables),
                new Token("/", TokenTypeEnum.Operators),
                new Token("b", TokenTypeEnum.Variables)
            };
        
            CollectionAssert.AreEqual(expected, actual);
        }
        
        [Test]
        public void TestSubNumbers()
        {
          
            var actual = T.Scan("55 - 21");

            List<Token> expected = new List<Token>()
            {
                new Token("55", TokenTypeEnum.Numbers),
                new Token("-", TokenTypeEnum.Operators),
                new Token("21", TokenTypeEnum.Numbers)
            };
        
            CollectionAssert.AreEqual(expected, actual);
        }
        [Test]
        public void TestSubVariables()
        {
          
            var actual = T.Scan("a - b");

            List<Token> expected = new List<Token>()
            {
                new Token("a", TokenTypeEnum.Variables),
                new Token("-", TokenTypeEnum.Operators),
                new Token("b", TokenTypeEnum.Variables)
            };
        
            CollectionAssert.AreEqual(expected, actual);
        }
        [Test]
        public void TestLogicalOr()
        {
          
            var actual = T.Scan("a | b");

            List<Token> expected = new List<Token>()
            {
                new Token("a", TokenTypeEnum.Variables),
                new Token("|", TokenTypeEnum.Operators),
                new Token("b", TokenTypeEnum.Variables)
            };
        
            CollectionAssert.AreEqual(expected, actual);
        }
        [Test]
        public void TestLogicalAnd()
        {
          
            var actual = T.Scan("a & b");

            List<Token> expected = new List<Token>()
            {
                new Token("a", TokenTypeEnum.Variables),
                new Token("&", TokenTypeEnum.Operators),
                new Token("b", TokenTypeEnum.Variables)
            };
        
            CollectionAssert.AreEqual(expected, actual);
        }
        
        [Test]
        public void TestLogicalXor()
        {
          
            var actual = T.Scan("a ^ b");

            List<Token> expected = new List<Token>()
            {
                new Token("a", TokenTypeEnum.Variables),
                new Token("^", TokenTypeEnum.Operators),
                new Token("b", TokenTypeEnum.Variables)
            };
        
            CollectionAssert.AreEqual(expected, actual);
        }
        
        [Test]
        public void TestBitwiseCompl()
        {
          
            var actual = T.Scan("~b");

            List<Token> expected = new List<Token>()
            {
                new Token("~", TokenTypeEnum.Operators),
                new Token("b", TokenTypeEnum.Variables)
            };
        
            CollectionAssert.AreEqual(expected, actual);
        }
        
        [Test]
        public void TestBrackets()
        {
          
            var actual = T.Scan("a(2+c)");

            List<Token> expected = new List<Token>()
            {
                new Token("a", TokenTypeEnum.Variables),
                new Token("(", TokenTypeEnum.Operators),
                new Token("2", TokenTypeEnum.Numbers),
                new Token("+", TokenTypeEnum.Operators),
                new Token("c", TokenTypeEnum.Variables),
                new Token(")", TokenTypeEnum.Operators)
            };
            CollectionAssert.AreEqual(expected, actual);
        }
        
        [Test]
        public void TestSin()
        {
          
            var actual = T.Scan("sin(45)");

            List<Token> expected = new List<Token>()
            {
                new Token("sin", TokenTypeEnum.Variables),
                new Token("(", TokenTypeEnum.Operators),
                new Token("45", TokenTypeEnum.Numbers),
                new Token(")", TokenTypeEnum.Operators)
            };
        
            CollectionAssert.AreEqual(expected, actual);
        }
        
        [Test]
        public void TestCos()
        {
          
            var actual = T.Scan("cos(b)");

            List<Token> expected = new List<Token>()
            {
                new Token("cos", TokenTypeEnum.Variables),
                new Token("(", TokenTypeEnum.Operators),
                new Token("b", TokenTypeEnum.Variables),
                new Token(")", TokenTypeEnum.Operators)
            };
        
            CollectionAssert.AreEqual(expected, actual);
        }
        
        [Test]
        public void TestTan()
        {
          
            var actual = T.Scan("tan(45a)");

            List<Token> expected = new List<Token>()
            {
                new Token("tan", TokenTypeEnum.Variables),
                new Token("(", TokenTypeEnum.Operators),
                new Token("45", TokenTypeEnum.Numbers),
                new Token("a", TokenTypeEnum.Variables),
                new Token(")", TokenTypeEnum.Operators)
            };
            CollectionAssert.AreEqual(expected, actual);
        }
        
        [Test]
        public void TestPow()
        {
          
            var actual = T.Scan("pow(a,2)");

            List<Token> expected = new List<Token>()
            {
                new Token("pow", TokenTypeEnum.Variables),
                new Token("(", TokenTypeEnum.Operators),
                new Token("a", TokenTypeEnum.Variables),
                new Token(",", TokenTypeEnum.Operators),
                new Token("2", TokenTypeEnum.Numbers),
                new Token(")", TokenTypeEnum.Operators)
            };
            CollectionAssert.AreEqual(expected, actual);
        }
    }
}