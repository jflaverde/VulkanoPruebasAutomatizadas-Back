using DataFramework.DTO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using ControllerVulkano;

namespace WorkerWebsiteBDT
{
    class Program
    {
        static void Main()
        {
            GeneralWorkerNetFramework.RabbitMQ rabbitMQ = new GeneralWorkerNetFramework.RabbitMQ();
            var strategy = rabbitMQ.GetMessages("QUEUE_BDT");

            if (strategy != null)
            {
                TestCypress(strategy);
            }
        }

        static void TestCypress(EstrategiaDTO strategy)
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
                GeneralWorkerNetFramework.ActionsFile actionsFile = new GeneralWorkerNetFramework.ActionsFile();
                actionsFile.UnzipFile(scriptPath, destinationFolder);

                // Install the node modules for each of the test projects
                var psiNpmRunDist = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    RedirectStandardInput = true,
                };
                var pNpmRunDist = Process.Start(psiNpmRunDist);

                Directory.GetDirectories(destinationPath).ToList().ForEach(p => {
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
        static string GetScriptProjectPath()
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