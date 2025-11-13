namespace SerialSoup.Tokens
{
    public abstract class TokenizableObject : ITokenizable 
    {
        public DictToken Tokenize()
        {
            return (this as ITokenizable).ITokenize();
        }

        public void Detokenize(DictToken token)
        {
            (this as ITokenizable).IDetokenize(token);
        }

        public sealed override string ToString()
        {
            return Tokenize().ToString();
        }
    }
}
