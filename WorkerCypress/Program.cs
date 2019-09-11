using System;
using System.Diagnostics;

namespace WorkerCypress
{
    class Program
    {
        static void Main(string[] args)
        {
            var psiNpmRunDist = new ProcessStartInfo
            {
                FileName = "cmd",
                RedirectStandardInput = true,
                //WorkingDirectory = guiProjectDirectory
            };
            var pNpmRunDist = Process.Start(psiNpmRunDist);
            pNpmRunDist.StandardInput.WriteLine("npx cypress run");
            pNpmRunDist.WaitForExit();
            

            

        }
    }
}
