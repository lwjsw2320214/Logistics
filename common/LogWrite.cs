using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace common
{
    public class LogWrite
    {
         
            public static void WriteLog(string error)
            {
                WriteLog(error, null);
            }

            public static void WriteLog(string error, Exception ex)
            {

                string dir ="D:\\log";

                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                error = DateTime.Now.ToString() + " " + error + System.Environment.NewLine;

                if (ex != null)
                {
                    error += ex.ToString() + System.Environment.NewLine;
                }

                string logFilePath = dir + "\\log_" + DateTime.Now.ToString("yyyy-MM-dd") + ".log";
                File.AppendAllText(logFilePath, error);
            }
        } 
}
