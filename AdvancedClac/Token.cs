using System;

namespace AdvancedClac
{
    public class Token : IEquatable<Token>
    {
        private string _value;
        private TokenTypeEnum _tokenType;
        
        public Token(string value, TokenTypeEnum tokenType)
        {
            
            try
            {
                _value = value;
                _tokenType = tokenType;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public TokenTypeEnum TokenType => _tokenType;

        public string Value
        {
            get => _value;

            set => _value = value;
        }
        
        public override int GetHashCode()
        {
            int hash = 23;
            hash = hash * 31 + Value == null ? 0 : Value.GetHashCode();
            hash = hash * 31 + TokenType.GetHashCode();
            return hash;
        }

        public override bool Equals(object other)
        {
            return Equals(other as Token);
        }

        public bool Equals(Token other)
        {
            return other != null &&
                   this.Value == other.Value &&
                   this.TokenType == other.TokenType;
        } 
    }
}