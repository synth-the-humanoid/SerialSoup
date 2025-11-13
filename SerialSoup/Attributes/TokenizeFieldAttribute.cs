using System;

namespace SerialSoup.Attributes
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class TokenizeFieldAttribute : Attribute
    {

    }
}