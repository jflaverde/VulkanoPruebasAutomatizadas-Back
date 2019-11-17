using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace WorkerWebsiteBDT
{
    class ScriptFile
    {
        /// <summary>
        /// Get an enumarator with the feature files path
        /// </summary>
        /// <param name="projectPath">The path of test project</param>
        /// <returns>Enumerator with the feature files path</returns>
        public IEnumerable<string> GetFeatureFiles(string projectPath)
        {
            IEnumerable<string> scriptsFiles = Directory.GetFiles(projectPath, "*.*", SearchOption.AllDirectories)
                .Where(s => Path.GetExtension(s).Contains(".feature"));
            return scriptsFiles;
        }

        /// <summary>
        /// REplace tokens on data test file
        /// </summary>
        /// <param name="featureFilePath">Path of the feature script file</param>
        /// <param name="numberDataGenerated">Number of data to generate on the test</param>
        public void ReplaceTokens(string featureFilePath, int numberDataGenerated)
        {
            string tokenToSearch = "{0}";

            // Read the content file
            StringBuilder stringBuilder = new StringBuilder();
            using StringReader reader = new StringReader(featureFilePath);
            foreach (string line in File.ReadAllLines(featureFilePath))
            {
                if (line.Contains(tokenToSearch))
                {
                    Random rnd = new Random();
                    int randomObject = rnd.Next(0, numberDataGenerated);
                    string lineReplaced = line.Replace(tokenToSearch, randomObject.ToString());
                    stringBuilder.AppendLine(lineReplaced);
                    continue;
                }

                stringBuilder.AppendLine(line);
            }

            using var writer = new StreamWriter(featureFilePath);
            writer.Write(stringBuilder.ToString());
        }
    }
}
