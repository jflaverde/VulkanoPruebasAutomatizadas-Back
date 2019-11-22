using DataFramework.DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ControllerVulkano;


namespace WorkerWebsiteBDT
{
    class Program
    {

        static void Main()
        {
            GeneralWorkerNetFramework.RabbitMQ rabbitMQ = new GeneralWorkerNetFramework.RabbitMQ();
            EstrategiaDTO strategy = rabbitMQ.GetMessages("QUEUE_BDT");

            //TODO: Reemplazar la cantidad de iteraciones por el valor del mensaje
            for (int i = 0; i < 10; i++)
            {
                StartProcess(strategy);
            }
        }

        /// <summary>
        /// Start with the process of environment preparation and test execution
        /// </summary>
        /// <param name="strategy"><see cref="EstrategiaDTO"/></param>
        static void StartProcess(EstrategiaDTO strategy)
        {
            // TODO: Reemplazar las siguientes variables por el valor del mensaje: projectPath, parameters
            foreach (TipoPruebaDTO tipoPrueba in strategy.TipoPruebas)
            {
                TipoPruebaController tipoPruebaController = new TipoPruebaController();
                ScriptFile scriptFile = new ScriptFile();

                int idExecution = tipoPruebaController.InsertEjecucionTipoPrueba(strategy.Estrategia_ID, strategy.TipoPruebas[0].ID, 0, "");

                string testProject = Path.Combine(scriptFile.GetScriptProjectPath(), @"wwwroot\uploads");
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

                // Create json file structure file
                string projectPath = @"F:\Universidad de los Andes\MISO\Pruebas Automaticas\T Grupal\-201920_MISO4208\PrestashopTest";
                string dataDestinationFolder = Path.Combine(projectPath, @"Prestashop.Core\fixtures\data.json");
                ParametersRequest parameters = new ParametersRequest
                {
                    ApiController = "dataprestashop.json",
                    Key = "c1893e20"
                };

                JsonFile jsonFile = new JsonFile();
                int numberDataGenerated = jsonFile.GenerateData(parameters, dataDestinationFolder);

                // Read test scripts
                IEnumerable<string> featureFiles = scriptFile.GetFeatureFiles(projectPath);

                // Replace tokens with random position object of json file
                foreach (string featureFile in featureFiles)
                {
                    scriptFile.ReplaceTokens(featureFile, numberDataGenerated);
                }

                // Executes Cypress script
                if (strategy != null)
                {
                    scriptFile.RunCypressTest(strategy);
                }
            }
        }
    }
}