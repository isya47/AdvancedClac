using System;

namespace AdvancedClac
{
    public class Token
    {
        private  int _value;
        private readonly string _tokenType;
        public Token(int value, string tokenType)
        {
            _value = value;
            _tokenType = tokenType;
        }
        
        public int GetValue
        {
            get { return _value; }
        }

        public void SetValue(int newValue)
        {
            _value = newValue;
        }

        public String GetTokenType
        { 
            get { return _tokenType; }
        }
    }
}