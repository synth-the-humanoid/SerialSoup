using SerialSoup.Exceptions;

namespace SerialSoup.Tokens
{
    public sealed class FloatToken : ValueToken<float>
    {
        public FloatToken(float value = 0f)
        {
            Value = value;
        }

        public FloatToken(string value)
        {
            Serialize(value);
        }

        protected override void Serialize(string data)
        {
            try
            {
                Value = float.Parse(data);
            }
            catch
            {
                throw new MalformedDataException(string.Format("Invalid float: {0}", data));
            }
        }
    }
}