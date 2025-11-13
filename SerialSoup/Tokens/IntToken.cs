using SerialSoup.Exceptions;

namespace SerialSoup.Tokens
{
    public sealed class IntToken : ValueToken<int>
    {
        public IntToken(int value=0)
        {
            Value = value;
        }

        public IntToken(string value)
        {
            Serialize(value);
        }

        protected override void Serialize(string data)
        {
            try
            {
                Value = int.Parse(data);
            }
            catch
            {
                throw new MalformedDataException(string.Format("Invalid integer: {0}", data));
            }
        }
    }
}