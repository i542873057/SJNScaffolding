using System;

namespace SJNScaffolding.Utilities
{
    public class NamingUtil
    {
        public static String CamelCase(string phrase)
        {
            string firstChar = phrase.Substring(0, 1).ToLower();
            return firstChar + phrase.Substring(1);
        }
    }
}
