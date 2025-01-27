﻿using DataFramework.DTO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using ControllerVulkano;

namespace WorkerWebsiteE2E
{
    class Program
    {

        static void Main(string[] args)
        {
            GeneralWorkerNetFramework.RabbitMQ rabbitMQ = new GeneralWorkerNetFramework.RabbitMQ();

            var estrategia = rabbitMQ.GetMessages("QUEUE_E2E");

            if (estrategia != null)
            {
                TestCypress(estrategia);
            }
        }

        static void TestCypress(EstrategiaDTO estrategia)
        {
            foreach (TipoPruebaDTO tipoPrueba in estrategia.TipoPruebas)
            {
                TipoPruebaController tipoPruebaController = new TipoPruebaController();
                int idExecution = tipoPruebaController.InsertEjecucionTipoPrueba(estrategia.Estrategia_ID, estrategia.TipoPruebas[0].ID, 0, "");

                GeneralWorkerNetFramework.ActionsFile actionsFile = new GeneralWorkerNetFramework.ActionsFile();
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
                //string rutaAbsoluta = "D:\\Takezo316\\GitHub\\VulkanoPruebasAutomatizadas-Front\\VulkanoPruebasAutomatizadas-Front\\wwwroot\\uploads";
                Directory.SetCurrentDirectory(@"C:\vulkanotest");
                string cypath = Path.GetFullPath(@"C:");
                string rutaScript = string.Concat(rutaAbsoluta, estrategia.TipoPruebas.First().Script.Script);
                string rutaDestino = string.Concat(cypath, "\\", estrategia.TipoPruebas.First().Script.ID);

                if (!Directory.Exists(rutaDestino))
                {
                    Directory.CreateDirectory(rutaDestino);
                }

                //rutaDestino= string.Concat("C:\\Users\\Sistemas\\Pruebas automatizadas\\E2E\\E2E\\cypress\\integration", estrategia.TipoPruebas.First().Script.Script);
                rutaDestino = string.Concat(cypath, estrategia.TipoPruebas.First().Script.Script);

                string filename = Path.GetFileNameWithoutExtension(rutaDestino);

                File.Copy(rutaScript, rutaDestino, true);

                actionsFile.UnzipFile(rutaScript, string.Concat(cypath, "\\", estrategia.TipoPruebas.First().Script.ID, "\\"));

                pNpmRunDist.StandardInput.WriteLine(string.Concat("cd ", cypath, "\\", estrategia.TipoPruebas.First().Script.ID, "\\", filename));
                pNpmRunDist.StandardInput.WriteLine("npm i");
                pNpmRunDist.StandardInput.WriteLine("npx cypress run .");
                pNpmRunDist.WaitForExit();

                tipoPruebaController.InsertEjecucionTipoPrueba(estrategia.Estrategia_ID, estrategia.TipoPruebas[0].ID, idExecution, "");

            }
        }

    }
}
