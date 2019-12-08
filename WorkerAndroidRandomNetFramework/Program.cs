using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Threading;
using System.IO;
using System.IO.Compression;
using System.Text;
using DataFramework.DTO;
using System.Linq;
using System.Configuration;
using ControllerVulkano;

namespace WorkerAndroidRandom
{
    class Program
    {
        static void Main(string[] args)
        {
            GeneralWorkerNetFramework.RabbitMQ rabbitMQ = new GeneralWorkerNetFramework.RabbitMQ();
            EstrategiaDTO strategy = rabbitMQ.GetMessages("QUEUE_ANDROIDRANDOM");

            
            StartProcess(strategy);
            
        }

        static void StartProcess(EstrategiaDTO estrategia)
        {
            TipoPruebaController tipoPruebaController = new TipoPruebaController();
            int idExecution = tipoPruebaController.InsertEjecucionTipoPrueba(estrategia.Estrategia_ID, estrategia.TipoPruebas[0].ID, 0, "", EstadoEnum.EnEjecucion);


            int times = estrategia.TipoPruebas.First().CantidadEjecuciones;
            string delay = "0.2";
            string seed1;
            string seed2;
            string seed3;
            string seed4;
            string completeSeed = estrategia.TipoPruebas.First().Semilla;

            if (string.IsNullOrEmpty(completeSeed))
            {
                seed1 = new Random().Next(0, 999999999).ToString();
                seed2 = new Random().Next(0, 999999999).ToString();
                seed3 = new Random().Next(0, 999999999).ToString();
                seed4 = new Random().Next(0, 999999999).ToString();
                completeSeed = seed1 + seed2 + seed3 + seed4;
            }

            string ruta = estrategia.TipoPruebas.First().Script.Script;
            string fileName = ConfigurationManager.AppSettings["RutaScript"] + ruta + "\\features\\monkey.feature";

            string zipFile = "features.zip";
            string featuresPath = ConfigurationManager.AppSettings["RutaScript"] + ruta;
            string zipPath = ConfigurationManager.AppSettings["RutaScript"] + ruta + "\\" + zipFile;

            try
            {
                // Check if file already exists. If yes, delete it.     
                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }

                // Create a new file     
                using (FileStream fs = File.Create(fileName))
                {
                    // Add some text to file    
                    Byte[] title = new UTF8Encoding(true).GetBytes("Feature: Monkey Testing");
                    fs.Write(title, 0, title.Length);

                    Byte[] newLine = new UTF8Encoding(true).GetBytes("\n");
                    fs.Write(newLine, 0, newLine.Length);

                    Byte[] scenarioTitle = new UTF8Encoding(true).GetBytes("Scenario: Random touch");
                    fs.Write(newLine, 0, newLine.Length);

                    Byte[] author = new UTF8Encoding(true).GetBytes("    Given I make " + times + " events with a waiting time of " + delay + " with seed " + completeSeed);
                    fs.Write(author, 0, author.Length);
                }

                // Open the stream and read it back.    
                using (StreamReader sr = File.OpenText(fileName))
                {
                    string s = "";
                    while ((s = sr.ReadLine()) != null)
                    {
                        Console.WriteLine(s);
                    }
                }

                if (File.Exists(zipPath))
                {
                    File.Delete(zipPath);
                }

                ZipFile.CreateFromDirectory(featuresPath, zipPath);
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.ToString());
            }

            //Create upload for apk
            String cmd = @"/C C:\PROGRA~1\Amazon\AWSCLI\bin\aws devicefarm create-upload --project-arn arn:aws:devicefarm:us-west-2:813226252700:project:45bfef00-da01-48d6-bc69-2b2c7f6fb425 --name " + estrategia.TipoPruebas.First().Parametros + "--type ANDROID_APP";

            var psiNpmRunDist = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                UseShellExecute = false,
                Arguments = cmd
            };
            var pNpmRunDist = Process.Start(psiNpmRunDist);

            string output = pNpmRunDist.StandardOutput.ReadToEnd();

            Console.WriteLine(output);

            dynamic response = JsonConvert.DeserializeObject(output);
            string urlForApkUpload = response.upload.url.ToString();

            Console.WriteLine("url: " + urlForApkUpload);

            //Upload apk
            string rutaAbsoluta = estrategia.Aplicacion.Ruta_Aplicacion;

            String paramsForApkUpload = @"/C curl -T " + rutaAbsoluta + " \"" + urlForApkUpload + "\"";

            psiNpmRunDist = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                UseShellExecute = false,
                Arguments = paramsForApkUpload
            };
            pNpmRunDist = Process.Start(psiNpmRunDist);

            output = pNpmRunDist.StandardOutput.ReadToEnd();

            Console.WriteLine(output);

            pNpmRunDist.WaitForExit();

            Console.WriteLine("finished");

            //Create Upload for script
            cmd = @"/C C:\PROGRA~1\Amazon\AWSCLI\bin\aws devicefarm create-upload --project-arn arn:aws:devicefarm:us-west-2:813226252700:project:45bfef00-da01-48d6-bc69-2b2c7f6fb425 --name " + estrategia.TipoPruebas.First().Script.Nombre + estrategia.TipoPruebas.First().Script.Extension + " --type CALABASH_TEST_PACKAGE";

            psiNpmRunDist = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                UseShellExecute = false,
                Arguments = cmd
            };
            pNpmRunDist = Process.Start(psiNpmRunDist);

            output = pNpmRunDist.StandardOutput.ReadToEnd();

            Console.WriteLine(output);

            response = JsonConvert.DeserializeObject(output);
            var urlForScriptUpload = response.upload.url.ToString();

            Console.WriteLine("url: " + urlForScriptUpload);

            //Upload script
            rutaAbsoluta = zipPath;

            string paramsForScriptUpload = @"/C curl -T " + rutaAbsoluta + " \"" + urlForScriptUpload + "\"";

            psiNpmRunDist = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                UseShellExecute = false,
                Arguments = paramsForScriptUpload
            };
            pNpmRunDist = Process.Start(psiNpmRunDist);

            output = pNpmRunDist.StandardOutput.ReadToEnd();

            Console.WriteLine(output);

            pNpmRunDist.WaitForExit();

            Console.WriteLine("finished");

            //response = JsonConvert.DeserializeObject(output);                    

            //string output = pNpmRunDist.StandardOutput.ReadToEnd();

            //Thread.Sleep(10000);

            //Execute test
            cmd = @"/C C:\PROGRA~1\Amazon\AWSCLI\bin\aws devicefarm schedule-run --project-arn arn:aws:devicefarm:us-west-2:813226252700:project:45bfef00-da01-48d6-bc69-2b2c7f6fb425  --app-arn arn:aws:devicefarm:us-west-2:813226252700:upload:45bfef00-da01-48d6-bc69-2b2c7f6fb425/cf5e1be8-e01c-4ceb-87d6-0a2970e32957 --device-pool-arn arn:aws:devicefarm:us-west-2::devicepool:1c59cfd0-ee56-4443-b290-7a808d9fd885 --name MyTestRun --test type=CALABASH,testPackageArn=arn:aws:devicefarm:us-west-2:813226252700:upload:45bfef00-da01-48d6-bc69-2b2c7f6fb425/a642b018-2ca7-4b16-9469-92cf7c363849";

            psiNpmRunDist = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                UseShellExecute = false,
                Arguments = cmd
            };
            pNpmRunDist = Process.Start(psiNpmRunDist);

            output = pNpmRunDist.StandardOutput.ReadToEnd();

            Console.WriteLine(output);

            response = JsonConvert.DeserializeObject(output);
            var arnForRun = response.run.arn.ToString();

            Console.WriteLine("arnForRun: " + arnForRun);

            string status = "";

            while (status != "COMPLETED" || status != "STOPPING")
            {
                //Check if finished

                cmd = @"/C C:\PROGRA~1\Amazon\AWSCLI\bin\aws devicefarm get-run --arn" + " \"" + arnForRun + "\"";

                psiNpmRunDist = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    Arguments = cmd
                };
                pNpmRunDist = Process.Start(psiNpmRunDist);

                output = pNpmRunDist.StandardOutput.ReadToEnd();
                Console.WriteLine(output);
                response = JsonConvert.DeserializeObject(output);
                status = response.run.status.ToString();

                Thread.Sleep(60000);
            }

            //For the results is used arnForRun
            //for example:
            //arn:aws:devicefarm:us-west-2:813226252700:run:45bfef00-da01-48d6-bc69-2b2c7f6fb425/ff7d0b49-162b-4bc2-9ee9-9cdc96a1c239

            //Screenshots results
            cmd = @"/C C:\PROGRA~1\Amazon\AWSCLI\bin\aws devicefarm list-artifacts --arn" + " \"" + arnForRun + "\" --type SCREENSHOT";

            psiNpmRunDist = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                UseShellExecute = false,
                Arguments = cmd
            };
            pNpmRunDist = Process.Start(psiNpmRunDist);

            output = pNpmRunDist.StandardOutput.ReadToEnd();

            Console.WriteLine(output);

            response = JsonConvert.DeserializeObject(output);

            //marca como finalizado
            tipoPruebaController.InsertEjecucionTipoPrueba(estrategia.Estrategia_ID, estrategia.TipoPruebas[0].ID, idExecution, "", EstadoEnum.Finalizado);
        }

    }
}
