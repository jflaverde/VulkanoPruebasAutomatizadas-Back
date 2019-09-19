using System;
using System.Collections.Generic;
using System.Diagnostics;
using RabbitMQ.Client;

namespace WorkerCypress
{
    class Program
    {
        static void Main(string[] args)
        {
            TestCypress();

        }


        static void TestCypress()
        {
            var psiNpmRunDist = new ProcessStartInfo
            {
                FileName = "cmd",
                RedirectStandardInput = true,
                //WorkingDirectory = guiProjectDirectory
            };
            var pNpmRunDist = Process.Start(psiNpmRunDist);
            pNpmRunDist.StandardInput.WriteLine("cd C:\\Users\\Sistemas\\Pruebas automatizadas\\Taller Cypress");


            pNpmRunDist.StandardInput.WriteLine("npx cypress run .");
            pNpmRunDist.WaitForExit();
        }

    }
}
