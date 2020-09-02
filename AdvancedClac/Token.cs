using System;

namespace AdvancedClac
{
    public class Token
    {
        private  string _value;
        private readonly string _tokenType;
        public Token(string value, string tokenType)
        {
            _value = value;
            _tokenType = tokenType;
        }
        
        public string GetValue
        {
            get { return _value; }
        }

        public void SetValue(string newValue)
        {
            _value = newValue;
        }

        public string GetTokenType
        { 
            get { return _tokenType; }
        }
    }
}