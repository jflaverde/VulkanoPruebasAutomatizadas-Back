using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace WorkerWebsiteBDT
{
    class JsonFile
    {
        /// <summary>
        /// Generate random data from Mockaroo
        /// </summary>
        /// <param name="parametersRequest"><see cref="ParametersRequest"/></param>
        /// <param name="filePath">Destination path of the test data file</param>
        /// <returns>REturns the number of the data generated</returns>        
        public int GenerateData(ParametersRequest parametersRequest, string filePath)
        {
            string url = string.Format("https://my.api.mockaroo.com/{0}", parametersRequest.ApiController);
            string parameters = string.Format("?key={0}", parametersRequest.Key);

            HttpClient client = new HttpClient
            {
                BaseAddress = new Uri(url)
            };

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            JArray jsonResponse = null;

            // List data response
            HttpResponseMessage response = client.GetAsync(parameters).Result;
            if (response.IsSuccessStatusCode)
            {
                jsonResponse = JArray.Parse(response.Content.ReadAsStringAsync().Result);
                CreateFile(filePath, jsonResponse.ToString());
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }

            client.Dispose();
            return jsonResponse.Count;
        }

        /// <summary>
        /// Create a json file in a path specified
        /// </summary>
        /// <param name="filePath">The path where will be created the file</param>
        /// <param name="content">The content of the text file</param>
        private void CreateFile(string filePath, string content)
        {
            try
            {
                // Create the file, or overwrite if the file exists.
                using FileStream fs = File.Create(filePath);
                byte[] info = new UTF8Encoding(true).GetBytes(content);

                // Add some information to the file.
                fs.Write(info, 0, info.Length);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
