using Data.DTO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace WorkerCypress
{
    class Program
    {

        static void Main(string[] args)
        {
            RabbitMQ rabbitMQ = new RabbitMQ();

            var estrategia = rabbitMQ.GetMessages("QUEUE_E2E");

            if(estrategia!=null)
            {
                TestCypress(estrategia);
            }
        }


        static void TestCypress(EstrategiaDTO estrategia)
        {
            var psiNpmRunDist = new ProcessStartInfo
            {
                FileName = "cmd",
                RedirectStandardInput = true,
                //WorkingDirectory = guiProjectDirectory
            };
            var pNpmRunDist = Process.Start(psiNpmRunDist);
            pNpmRunDist.StandardInput.WriteLine("cd C:\\Windows\\System32\\cmd.exe");

            //Traer y copiar el archivo script al sitio donde están los scripts 
            string rutaAbsoluta = "C:\\Users\\Sistemas\\source\\repos\\VulkanoPruebasAutomatizadas-Front\\VulkanoPruebasAutomatizadas-Front\\wwwroot";

            string rutaScript = string.Concat(rutaAbsoluta,estrategia.TipoPruebas.First().Script.Script);

            string rutaDestino = string.Concat("C:\\Users\\Sistemas\\Pruebas automatizadas\\E2E\\E2E\\cypress\\integration","\\",estrategia.TipoPruebas.First().Script.ID); 

            if(!Directory.Exists(rutaDestino))
            {
                Directory.CreateDirectory(rutaDestino);
            }

            rutaDestino= string.Concat("C:\\Users\\Sistemas\\Pruebas automatizadas\\E2E\\E2E\\cypress\\integration", estrategia.TipoPruebas.First().Script.Script);

            File.Copy(rutaScript, rutaDestino,true);

            pNpmRunDist.StandardInput.WriteLine("cd C:\\Users\\Sistemas\\Pruebas automatizadas\\E2E\\E2E");

            pNpmRunDist.StandardInput.WriteLine("npx cypress run .");
            pNpmRunDist.WaitForExit();

        }

    }
}
