using SerialSoup.Tokens;
using SerialSoup.Exceptions;

namespace SerialSoup
{
    public abstract class Token
    {
        public static Token Tokenize(string text)
        {
            if(Parser.StartsAndEndsWith(text, '{', '}'))
            {
                return new DictToken(text);
            }
            if(Parser.StartsAndEndsWith(text, '"'))
            {
                return new StringToken(text);
            }
            if(Parser.OnlyContains(text, "0123456789."))
            {
                if(Parser.Contains(text, '.'))
                {
                    return new FloatToken(text);
                }
                return new IntToken(text);
            }
            throw new MalformedDataException("Invalid token.");
        }

        public static Token FromObject(object value)
        {
            if(value != null)
            {
                if(value is ITokenizable)
                {
                    return (value as ITokenizable).ITokenize();
                }
                if (value is string)
                {
                    return new StringToken(string.Format("\"{0}\"", value as string));
                }
                if (value is int)
                {
                    return new IntToken(value.ToString());
                }
                if(value is float)
                {
                    return new FloatToken(value.ToString());
                }
                if(value is Dictionary<object, object>)
                {
                    Dictionary<object, object> dict = value as Dictionary<object, object>;
                    DictToken token = new DictToken();
                    foreach (object eachKey in dict.Keys)
                    {
                        token.Value[eachKey.ToString()] = FromObject(dict[eachKey]);
                    }
                    return token;
                }
                if(value is Array)
                {
                    Dictionary<int, object> dict = new Dictionary<int, object>();
                    Array array = value as Array;
                    for(int index = 0; index < array.Length; index++)
                    {
                        dict[index] = array.GetValue(index);
                    }
                    return FromObject(dict);
                }
                throw new UnsupportedTypeException(value.GetType());
            }
            return new DictToken();
        }

        protected abstract void Serialize(string data);
        protected abstract string Deserialize();

        public override string ToString()
        {
            return Deserialize();
        }

        public object TokenValue
        {
            get
            {
                return GetObject();
            }
        }

        protected virtual object GetObject()
        {
            return null;
        }
    }
}