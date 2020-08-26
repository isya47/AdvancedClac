using System;

namespace AdvancedClac
{
    public class Token
    {
        private readonly int _value;
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
        
        public String GetOperation
        { 
            get { return _operation; }
        }
    }
}