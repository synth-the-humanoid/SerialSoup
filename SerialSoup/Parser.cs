using System;
using SerialSoup.Exceptions;

namespace SerialSoup
{
    internal static class Parser
    {   
        public static bool StartsAndEndsWith(string text, char starter, char ender='\0')
        {
            Func<char, string> ctos = (char c) => 
            {
                return "" + c;
            };

            if(ender == '\0')
            {
                ender = starter;
            }

            return text.StartsWith(ctos(starter)) && text.EndsWith(ctos(ender));
        }

        public static string Shave(string text, int startShave, int endShave=0)
        {
            startShave = Math.Max(startShave, 0);
            endShave = Math.Max(endShave, 0);

            int shaveLength;
            if((shaveLength = startShave + endShave) >= text.Length)
            {
                return "";
            }
            return text.Substring(startShave, text.Length - shaveLength);
        }

        public static string Decapsulate(string text, char starter, char ender='\0')
        {
            if(ender == '\0')
            {
                ender = starter;
            }

            if(StartsAndEndsWith(text, starter, ender))
            {
                return Shave(text, 1, 1);
            }
            throw new MalformedDataException(string.Format("Unable to decapsulate string.\nString: {0}\nStarter: {1}\nCloser: {2}", text, starter, ender));
        }

        public static string Substring(string text, int startIndex, int endIndex=-1)
        {
            if(endIndex == -1)
            {
                endIndex = text.Length - 1;
            }
            string result = "";
            startIndex = Math.Min(Math.Max(startIndex, 0), text.Length - 1);
            endIndex = Math.Min(Math.Max(endIndex, startIndex), text.Length - 1);
            for(int index = startIndex; index <= endIndex; index++)
            {
                result += text[index];
            }
            return result;
        }

        public static string GetOuterBlock(string text, int startIndex=0)
        {
            string result = "";
            int depth = 0;
            
            for(int index = startIndex; index < text.Length; index++)
            {
                if (text[index] == '{')
                {
                    depth++;
                }
                if (text[index] == '}')
                {
                    if (--depth < 0)
                    {
                        throw new MalformedDataException("Early closing bracket.");
                    }
                    if (depth == 0)
                    {
                        result += text[index];
                        return result;
                    }
                }
                if (depth != 0)
                {
                    result += text[index];
                }
            }
            throw new MalformedDataException("Missing closing bracket");
        }

        public static int FindNext(string text, char target, int startIndex=0, bool ignoreInStrings=true)
        {
            bool inString = false;
            for(int index = startIndex; index < text.Length; index++)
            {
                if (!inString && text[index] == target)
                {
                    return index;
                }
                if (text[index] == '"')
                {
                    inString = !inString;
                }
            }
            return -1;
        }

        public static bool Contains(string text, char target)
        {
            foreach(char eachChar in text)
            {
                if(eachChar == target)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool OnlyContains(string text, string allowed)
        {
            foreach(char eachChar in text)
            {
                if(!Contains(allowed, eachChar))
                {
                    return false;
                }
            }
            return true;
        }
    }
}