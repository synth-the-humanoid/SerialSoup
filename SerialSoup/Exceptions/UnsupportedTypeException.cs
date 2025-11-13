namespace SerialSoup.Exceptions
{
    public class UnsupportedTypeException : SerialSoupException
    {
        public UnsupportedTypeException(Type unsupported) 
        {
            ErrorContent = string.Format("Unsupported Type Exception!\nType {0} is not supported.", unsupported.Name);
        }
    }
}