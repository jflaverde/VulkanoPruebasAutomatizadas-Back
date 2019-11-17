using Controller;
using Data.DTO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        /// <summary>
        /// Run cypress test project
        /// </summary>
        /// <param name="strategy"><see cref="EstrategiaDTO"/></param>
        public void RunCypressTest(EstrategiaDTO strategy)
        {
            foreach (TipoPruebaDTO tipoPrueba in strategy.TipoPruebas)
            {
                TipoPruebaController tipoPruebaController = new TipoPruebaController();
                int idExecution = tipoPruebaController.InsertEjecucionTipoPrueba(strategy.Estrategia_ID, strategy.TipoPruebas[0].ID, 0, "");

                string testProject = Path.Combine(GetScriptProjectPath(), @"wwwroot\uploads");
                string destinationFolder = @"C:\Temp";
                string scriptPath = string.Concat(testProject, strategy.TipoPruebas.First().Script.Script);
                string destinationPath = string.Concat(destinationFolder, @"\",
                    Path.GetFileNameWithoutExtension(scriptPath));

                if (!File.Exists(scriptPath))
                {
                    String msg = String.Format("{0} do not exist. Please verify the information provided.", scriptPath);
                    Console.WriteLine(msg);
                    return;
                }

                // Extract the zip file
                GeneralWorker.ActionsFile actionsFile = new GeneralWorker.ActionsFile();
                actionsFile.UnzipFile(scriptPath, destinationFolder);

                // Install the node modules for each of the test projects
                var psiNpmRunDist = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    RedirectStandardInput = true,
                };
                var pNpmRunDist = Process.Start(psiNpmRunDist);

                Directory.GetDirectories(destinationPath).ToList().ForEach(p =>
                {
                    string[] packageJsonFile = Directory.GetFiles(p, "package.json");
                    if (packageJsonFile.Length == 1)
                    {
                        pNpmRunDist.StandardInput.WriteLine(Path.GetPathRoot(p).Replace(@"\", ""));
                        pNpmRunDist.StandardInput.WriteLine(string.Format("cd {0}", p));
                        pNpmRunDist.StandardInput.WriteLine("npm i");
                    }
                });

                pNpmRunDist.StandardInput.WriteLine("npx cypress run test");
                pNpmRunDist.WaitForExit();

                tipoPruebaController.InsertEjecucionTipoPrueba(strategy.Estrategia_ID, strategy.TipoPruebas[0].ID, idExecution, "");
            }
        }

        /// <summary>
        /// Get path of script project
        /// </summary>
        /// <returns>Path of script project</returns>
        public string GetScriptProjectPath()
        {
            DirectoryInfo directory = Directory.GetParent(Directory.GetCurrentDirectory());
            while (!directory.Name.Equals("VulkanoPruebasAutomatizadas-back"))
            {
                directory = directory.Parent;
            }

            directory = directory.Parent;
            string testProject = Path.Combine(directory.FullName, "VulkanoPruebasAutomatizadas-Front");
            return testProject;
        }
    }
}
