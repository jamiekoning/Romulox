using System;
using System.Linq;

namespace Romulox.Core.Helpers
{
    public static class StringExtensions
    {
        public static bool ContainsTokens(this string str, string[] tokens)
        {
            if (str == null || tokens == null)
                return false;
                //throw new ArgumentNullException("Parameter cannot be null", nameof(tokens));

            bool containsTokens = true;
            foreach (var token in tokens)
            {
                if (str.IndexOf(token, StringComparison.CurrentCultureIgnoreCase) == -1)
                    containsTokens = false;
            }

            return containsTokens;
        }

        public static string RemovePunctuation(this string str)
        {
            if (str == null)
                return null;
            
            return new string(str.Where(c => !char.IsPunctuation(c)).ToArray());
        }
    }
}