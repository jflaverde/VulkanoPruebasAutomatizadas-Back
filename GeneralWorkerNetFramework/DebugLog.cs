using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GeneralWorkerNetFramework
{
    /// <summary>
    /// Formatea y captura los eventos en un archivo de texto
    /// </summary>
    public class DebugLog
    {
        private static string fileName;

        private static string FileName()
        {

            // Write the string to a file.append mode is enabled so that the log
            // BatchLines get appended to  test.txt than wiping content and writing the log
            string CurrentDate = DateTime.Now.ToShortDateString().Replace("/", "");

            string fileName = "DebugLog" + CurrentDate + ".txt";
            string directory = Directory.GetCurrentDirectory() + "\\Logs";

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            return directory + "\\" + fileName;
        }

        /// <summary>
        /// Escribe en el log la información de eventos de la aplicación
        /// </summary>
        /// <param name="logText"></param>
        public static void EscribirLog(string logText)
        {
            try
            {
                fileName = FileName();
                using (StreamWriter w = System.IO.File.AppendText(fileName))
                {
                    w.WriteLine(DateTime.Now.ToString() + " - " + logText);
                }
            }
            catch { }
        }

        /// <summary>
        /// Escribe en log las excepciones de la aplicación
        /// </summary>
        /// <param name="ex"></param>
        public static void EscribirLog(Exception ex)
        {
            try
            {
                using (StreamWriter w = System.IO.File.AppendText(fileName))
                {
                    w.WriteLine("--------------------------------------------------------------------------------");
                    w.WriteLine(DateTime.Now.ToString() + " - EXCEPCION");
                    w.WriteLine("Message: " + ex.Message);
                    w.WriteLine("Source: " + ex.Source);
                    w.WriteLine("TargetSite: " + ex.TargetSite);
                    w.WriteLine("StackTrace: " + ex.StackTrace);
                    w.WriteLine("InnerException: " + ex.InnerException);
                    w.WriteLine("--------------------------------------------------------------------------------");
                }
            }
            catch { }
        }
    }
}