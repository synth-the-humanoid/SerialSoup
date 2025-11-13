using System;

namespace SerialSoup.Exceptions
{
    public abstract class SerialSoupException : Exception
    {
        private string ssError;

        public string ErrorContent
        {
            get
            {
                return string.Format("SerialSoup Exception!\n{0}\n", ssError);
            }
            protected set
            {
                ssError = value;
            }
        }
    }
}