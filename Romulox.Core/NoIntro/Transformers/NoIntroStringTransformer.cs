using System;
using System.IO;
using Romulox.Core.Interfaces;

namespace Romulox.Core.NoIntro.Transformers
{
    public class NoIntroStringTransformer : IGameNameTransformer
    {
        /*
         * Transforms a No-Intro named string into a standard format
         * 
         * No-Intro uses the naming convention
         * <game title> (<region>) (<version>) (<date>) (Rev <revision>)
         * As far as I am aware no game titles include parenthesis
         * therefore we can take the substring from the string beginning to the first '('
         * in order to extract the title
         *
         * Games with "The" in the title are named with a trailing ", The"
         */
        public string Transform(string file)
        {
            var fileName = Path.GetFileName(file);
            
            // Sequence of () come after game title
            var firstOpenParenthesis = fileName.IndexOf('(');

            if (firstOpenParenthesis == -1)
                throw new ArgumentException("Parameter does not use No-Intro naming conventions", nameof(fileName));
                    
            string gameName = fileName.Substring(0, firstOpenParenthesis).TrimEnd();
            
            //check if the game name includes trailing ", The"
            int trailingTheIndex = gameName.IndexOf(", The");

            if (trailingTheIndex != -1)
            {
                gameName = gameName.Remove(trailingTheIndex, 5);
                gameName = "The " + gameName;
            }

            return gameName;
        }
    }
}