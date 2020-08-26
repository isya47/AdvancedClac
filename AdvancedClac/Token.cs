using System;

namespace AdvancedClac
{
    public class Token
    {
        private  int _value;
        private readonly string _operation;
        public Token(int value, string operation)
        {
            _value = value;
            _operation = operation;
        }
        
        public int GetValue
        {
            get { return _value; }
        }

        public void SetValue(int newValue)
        {
            _value = newValue;
        }

        public String GetOperation
        { 
            get { return _operation; }
        }
    }
}