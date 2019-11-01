using System;
using System.IO;
using System.IO.Compression;

namespace GeneralWorker
{
    public class ActionsFile
    {
        /// <summary>
        /// Extract to a directory the content of the file zip specified
        /// </summary>
        /// <param name="scriptPath">Full path of the file to extract. The extension must be .zip</param>
        /// <param name="destinationPath">Path in which content will be extracted</param>
        public void UnzipFile(string scriptPath, string destinationPath)
        {
            try
            {
                ZipFile.ExtractToDirectory(scriptPath, destinationPath);
            }
            catch (Exception ex)
            {
                if (ex is ArgumentException || ex is ArgumentNullException)
                {
                    Console.WriteLine("Argument is null or empty.");
                }
                if (ex is DirectoryNotFoundException)
                {
                    Console.WriteLine("Directory was not found.");
                }
                if (ex is IOException)
                {
                    Console.WriteLine("Directory already exists, is empty or contains at least one invalid character.");
                }
            }
        }
    }
}
