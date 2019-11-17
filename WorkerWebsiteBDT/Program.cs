using Data.DTO;
using System.Collections.Generic;
using System.IO;

namespace WorkerWebsiteBDT
{
    class Program
    {

        static void Main()
        {
            GeneralWorker.RabbitMQ rabbitMQ = new GeneralWorker.RabbitMQ();
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
            ScriptFile scriptFile = new ScriptFile();
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