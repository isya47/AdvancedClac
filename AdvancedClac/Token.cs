using System;

namespace AdvancedClac
{
    public class Token
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
        
        public string GetValue
        {
            get { return _value; }
        }

        public void SetValue(string newValue)
        {
            try
            {
                _value = newValue;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                
            }
            
        }

        public Enum GetTokenType
        { 
            get { return _tokenType; }
        }
    }
}