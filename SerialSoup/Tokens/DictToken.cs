using System.Collections.Generic;

namespace SerialSoup.Tokens
{
    public sealed class DictToken : ValueToken<Dictionary<string, Token>>
    {
        public DictToken(string value ="{}")
        {
            Serialize(value);
        }

        protected override void Serialize(string value)
        {
            Value = new Dictionary<string, Token>();
            string data = Parser.Decapsulate(Parser.GetOuterBlock(value), '{', '}');
            int index = 0;
            while(index < data.Length)
            {
                char currentChar;
                string currentField = "";
                while(index < data.Length && (currentChar = data[index++]) != ':')
                {
                    currentField += currentChar;
                }
                int nextComma;
                string currentValue;
                if((nextComma = Parser.FindNext(data, ',', index)) != -1)
                {
                    currentValue = Parser.Substring(data, index, nextComma - 1);
                    index = nextComma + 1;
                }
                else
                {
                    currentValue = Parser.Substring(data, index);
                    index = data.Length;
                }
                Value[currentField] = Token.Tokenize(currentValue);
            }
        }

        public override string ToString()
        {
            string data = "";
            foreach(string eachField in Value.Keys)
            {
                string currentItem = string.Format("{0}:{1}", eachField, Value[eachField]);
                if(data.Length == 0)
                {
                    data = currentItem;
                }
                else
                {
                    data = string.Format("{0},{1}", data, currentItem);
                }
            }
            return '{' + data + '}';
        }
    }
}