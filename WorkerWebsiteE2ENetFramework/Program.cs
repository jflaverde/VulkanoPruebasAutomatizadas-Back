using DataFramework.DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ControllerVulkano;
using System.Configuration;
using System.Diagnostics;
using System.Text;
using System.Threading;
using GeneralWorkerNetFramework;

namespace WorkerWebsiteBDT
{
    class Program
    {
        static void Main()
        {
            DebugLog.EscribirLog("Inicio de la generacion del worker E2E");
            GeneralWorkerNetFramework.RabbitMQ rabbitMQ = new GeneralWorkerNetFramework.RabbitMQ();
            EstrategiaDTO strategy = rabbitMQ.GetMessages("QUEUE_E2E");

            if(strategy!=null)
            {
                for (int i = 0; i < strategy.TipoPruebas.First().CantidadEjecuciones; i++)
                {
                    StartProcess(strategy);
                }
            }
            DebugLog.EscribirLog("Fin de la generacion del worker E2E");
            Environment.Exit(0);
        }

        /// <summary>
        /// Start with the process of environment preparation and test execution
        /// </summary>
        /// <param name="strategy"><see cref="EstrategiaDTO"/></param>
        static void StartProcess(EstrategiaDTO strategy)
        {
            DebugLog.EscribirLog("Procesando la estrategia " + strategy.Estrategia_ID);
            TipoPruebaController tipoPruebaController = new TipoPruebaController();
            int idExecution = 0;
            try
            {
                WorkerStatus worker = new WorkerStatus();
                worker.WorkerID = Convert.ToInt32(ConfigurationManager.AppSettings["WorkerID"]);
                worker.Estado.ID = 6;
                worker.TipoPrueba = "QUEUE_E2E";
                WorkerController workerController = new WorkerController();
                workerController.UpdateWorkerStatus(worker);

                foreach (TipoPruebaDTO tipoPrueba in strategy.TipoPruebas)
                {
                    DebugLog.EscribirLog("Procesando la prueba " + tipoPrueba.ID);
                    
                    ScriptFile scriptFile = new ScriptFile();
                    //guarda en el historico de ejecucion de pruebas
                    idExecution = tipoPruebaController.InsertEjecucionTipoPrueba(strategy.Estrategia_ID, strategy.TipoPruebas[0].ID, 0, "", EstadoEnum.EnEjecucion);
                    
                    //ruta donde se encuentra el archivo del script
                    string testProject = Path.Combine(ConfigurationManager.AppSettings["RutaScript"]);
                    //ruta destino temporal
                    string destinationFolder = @"C:\Temp";
                    DebugLog.EscribirLog("id ejecucion " + idExecution);
                    string destinationPath = string.Concat(destinationFolder, @"\", tipoPrueba.Script.Script);
                    DebugLog.EscribirLog(destinationPath);
                    //ruta de origen del script
                    string scriptPath = string.Concat(testProject, tipoPrueba.Script.Script, tipoPrueba.Script.Nombre, tipoPrueba.Script.Extension);

                    //si la ruta destino no existe se crea
                    if (!File.Exists(destinationPath))
                    {
                        Directory.CreateDirectory(destinationPath);
                    }
                    DebugLog.EscribirLog("creo ruta de temp");
                    // Extract the zip file
                    GeneralWorkerNetFramework.ActionsFile actionsFile = new GeneralWorkerNetFramework.ActionsFile();
                    actionsFile.UnzipFile(scriptPath, destinationPath);
                    // Create json structure file
                    //string projectPath = //@"F:\Universidad de los Andes\MISO\Pruebas Automaticas\T Grupal\-201920_MISO4208\PrestashopTest";
                    string dataDestinationFolder = Path.Combine(destinationPath, @"Prestashop.Core\fixtures\data.json");
                    DebugLog.EscribirLog("ruta del fixtures");
                    //parametros de mockaroo
                    #region Mockaroo
                    ParametersRequest parameters = new ParametersRequest
                    {
                        ApiController = tipoPrueba.ApiController,
                        Key = tipoPrueba.ApiKey
                    };

                    JsonFile jsonFile = new JsonFile();
                    int numberDataGenerated = jsonFile.GenerateData(parameters, dataDestinationFolder);
                    #endregion
                    // Reemplaza tokens del script
                    DebugLog.EscribirLog(destinationPath);
                    #region Reemplazar tokens
                    IEnumerable<string> featureFiles = scriptFile.GetFeatureFiles(destinationPath);
                    // Replace tokens with random position object of json file
                    DebugLog.EscribirLog("inicio del reemplazo tokens");
                    foreach (string featureFile in featureFiles)
                    {
                        scriptFile.ReplaceTokens(featureFile, numberDataGenerated);
                    }
                    DebugLog.EscribirLog("reemplazo tokens");
                    #endregion
                    // Executes Cypress script
                    if (strategy != null)
                    {
                        DebugLog.EscribirLog("ruta del package");

                        string package = destinationPath + "\\" + "E2E.TestProject";
                        DebugLog.EscribirLog("fin ruta del fixtures");
                        // Install the node modules for each of the test project

                        var psiNpmRunDist = new ProcessStartInfo
                        {
                            FileName = "cmd.exe",
                            RedirectStandardInput = true,
                            UseShellExecute = false,
                            WorkingDirectory = package


                        };
                        DebugLog.EscribirLog("Abiendo CMD " + package);
                        using (var pNpmRunDist = Process.Start(psiNpmRunDist))
                        {
                            StringBuilder output = new StringBuilder();
                            pNpmRunDist.StandardInput.WriteLine("npm install cypress --save-dev");
                            Thread.Sleep(1000);

                            pNpmRunDist.StandardInput.WriteLine("npx cypress run ." + tipoPrueba.Parametros);
                            pNpmRunDist.WaitForExit(400000);
                            pNpmRunDist.Close();
                        }
                        DebugLog.EscribirLog("marca como finalizado");
                        //marca como finalizado
                    }
                }

            }
            catch (Exception ex)
            {
                DebugLog.EscribirLog("error " + ex.Message);
                Console.WriteLine("error " + ex);
                //en caso de error envia nuevamente a la cola
                Dispatcher rabbit = new Dispatcher();
                rabbit.EnviaMensajes(strategy);
            }
            finally
            {
                DebugLog.EscribirLog("Liberando el worker");
                WorkerStatus worker = new WorkerStatus();
                worker.WorkerID = Convert.ToInt32(ConfigurationManager.AppSettings["WorkerID"]);
                worker.Estado.ID = 5;
                worker.TipoPrueba = "QUEUE_E2E";
                WorkerController workerController = new WorkerController();
                workerController.UpdateWorkerStatus(worker);
                DebugLog.EscribirLog("Fin del proceso de la estrategia " + strategy.Estrategia_ID);
                tipoPruebaController.InsertEjecucionTipoPrueba(strategy.Estrategia_ID, strategy.TipoPruebas[0].ID, idExecution, "", EstadoEnum.WorkerLibre);
            }
        }
    }
}