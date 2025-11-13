namespace SerialSoup.Tokens
{
    public abstract class ValueToken<T> : Token
    {
        private T currentValue;

        public T Value
        {
            get
            {
                return currentValue;
            }
            protected set
            {
                currentValue = value;
            }
        }

        protected override string Deserialize()
        {
            return Value.ToString();
        }

        protected override object GetObject()
        {
            return Value;
        }
    }
}