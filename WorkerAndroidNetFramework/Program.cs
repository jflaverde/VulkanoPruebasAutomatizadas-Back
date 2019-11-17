using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Threading;

namespace WorkerAndroid
{
    class Program
    {
        static void Main(string[] args)
        {
            //Create upload for apk
            String cmd = @"/C C:\PROGRA~1\Amazon\AWSCLI\bin\aws devicefarm create-upload --project-arn arn:aws:devicefarm:us-west-2:813226252700:project:45bfef00-da01-48d6-bc69-2b2c7f6fb425 --name gnucash.apk --type ANDROID_APP";

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
            string rutaAbsoluta = "C:\\Users\\rafael.bermudez\\source\\repos\\worker\\worker\\gnucash.apk";

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
            cmd = @"/C C:\PROGRA~1\Amazon\AWSCLI\bin\aws devicefarm create-upload --project-arn arn:aws:devicefarm:us-west-2:813226252700:project:45bfef00-da01-48d6-bc69-2b2c7f6fb425 --name features.zip --type CALABASH_TEST_PACKAGE";

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
            rutaAbsoluta = "C:\\Users\\rafael.bermudez\\source\\repos\\worker\\worker\\features.zip";

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

            Thread.Sleep(500000);


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


        }
    }
}
