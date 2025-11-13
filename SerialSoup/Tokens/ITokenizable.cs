using SerialSoup.Attributes;
using System.Reflection;

namespace SerialSoup.Tokens
{
    public interface ITokenizable
    {
        private void RunForEachTokenField(Action<FieldInfo> func)
        {
            foreach(FieldInfo eachField in GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
            {
                if(eachField.GetCustomAttribute<TokenizeFieldAttribute>() != null)
                {
                    func(eachField);
                }
            }
        }

        public DictToken ITokenize()
        {
            DictToken token = new DictToken();
            RunForEachTokenField((FieldInfo eachField) => 
            {
                token.Value[eachField.Name] = Token.FromObject(eachField.GetValue(this));
            });
            return token;
        }

        public void IDetokenize(DictToken token)
        {
            RunForEachTokenField((FieldInfo eachField) => 
            {
                if(token.Value.ContainsKey(eachField.Name))
                {
                    Token valueToken = token.Value[eachField.Name];
                    if(valueToken is DictToken && typeof(ITokenizable).IsAssignableFrom(eachField.FieldType))
                    {
                        (eachField.GetValue(this) as ITokenizable).IDetokenize(valueToken as DictToken);
                    }
                    else
                    {
                        eachField.SetValue(this, valueToken.TokenValue);
                    }
                }
            });
        }
    }
}