using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using Romulox.Core.NoIntro.Entities;

namespace Romulox.Core.NoIntro.Helpers
{
    public class DatFilesHelper
    {
        private readonly List<NoIntroDatFile> datFiles;
        
        public DatFilesHelper(string datFilesPath)
        {
            datFiles = new List<NoIntroDatFile>();
            
            var files = Directory.GetFiles(datFilesPath);

            foreach (var file in files)
            {
                datFiles.Add(NoIntroDatFile.CreateFromFile(file));
            }
        }

        public NoIntroGame FindGame(string file)
        {
            var md5Hash = ComputeMd5Hash(file);
            
            foreach (var datFile in datFiles)
            {
                var game = datFile.FindGameByMd5Hash(md5Hash);

                if (game != null)
                {
                    return game;
                }
            }

            return null;
        }

        public NoIntroRom FindRom(string file)
        {
            var md5Hash = ComputeMd5Hash(file);

            foreach (var datFile in datFiles)
            {
                var rom = datFile.FindRomByMd5Hash(md5Hash);

                if (rom != null)
                {
                    return rom;
                }
            }

            return null;
        }

        public string FindGameName(string file)
        {
            var game = FindGame(file);

            return game?.Name;
        }
        
        public static string ComputeMd5Hash(string file)
        {
            MD5 md5Provider = new MD5CryptoServiceProvider();

            byte[] fileHash;
            using (FileStream fileStream = File.OpenRead(file))
            {
                fileHash = md5Provider.ComputeHash(fileStream);
            }
            
            return BitConverter.ToString(fileHash).Replace("-", "");
        }

        /*
         * Returns the game name without naming naming conventions if they are used
         * Returns the file name without extensions if conventions are not used
         */
        public static string CleanName(string file)
        {
            var fileName = Path.GetFileName(file);
            
            // Sequence of () come after game title
            var firstOpenParenthesis = fileName.IndexOf('(');

            if (firstOpenParenthesis == -1)
                return Path.GetFileNameWithoutExtension(file);

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