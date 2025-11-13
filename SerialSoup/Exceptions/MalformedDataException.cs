namespace SerialSoup.Exceptions
{
    public sealed class MalformedDataException : SerialSoupException
    {
        public MalformedDataException(string errorMessage)
        {
            ErrorContent = string.Format("Malformed Data Exception!\n{0}", errorMessage);
        }
    }
}