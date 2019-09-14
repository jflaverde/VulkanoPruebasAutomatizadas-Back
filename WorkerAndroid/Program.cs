using System;
using System.Diagnostics;
using System.Threading;

namespace WorkerAndroid
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


            pNpmRunDist.StandardInput.WriteLine("cd C:\\Users\\Sistemas\\AppData\\Local\\Android\\Sdk\\emulator");

            pNpmRunDist.StandardInput.WriteLine("emulator -avd Nexus_5_API_26 -netdelay none -netspeed full");

            Thread.Sleep(30000);

            var psiNpmRunDist1 = new ProcessStartInfo
            {
                FileName = "cmd",
                RedirectStandardInput = true,
                //WorkingDirectory = guiProjectDirectory
            };

            var pNpmRunDist1 = Process.Start(psiNpmRunDist1);
            pNpmRunDist1.StandardInput.WriteLine("cd C:\\Users\\Sistemas\\AppData\\Local\\Android\\Sdk\\platform-tools");
            
            pNpmRunDist1.StandardInput.WriteLine("adb shell monkey -p org.gnucash.android -v 10000");
            pNpmRunDist1.WaitForExit();


        
        }
    }
}
