using SerialSoup.Exceptions;

namespace SerialSoup.Tokens
{
    public sealed class StringToken : ValueToken<string>
    {
        public StringToken(string data="\"\"")
        {
            Serialize(data);
        }

        protected override void Serialize(string data)
        {
            Value = Parser.Decapsulate(data, '"');
        }

        protected override string Deserialize()
        {
            return string.Format("\"{0}\"", Value);
        }
    }
}