using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ConsoleApplication1
{
    public class WriterMessage
    {
        /// <summary>
        /// 写日志文件
        /// </summary>
        /// <param name="sPath">    年月  例  2011-04</param>
        /// <param name="sFileName">月日  例  04-22</param>
        /// <param name="content">时间+  内容</param>
        /// <returns></returns>
        public static bool WriteLog(string sPath, string sFileName, string content)
        {

            try
            {

                StreamWriter sr;

                if (!Directory.Exists(sPath))
                {

                    Directory.CreateDirectory(sPath);

                }

                string v_filename = sPath + "\\" + sFileName + ".txt";

                if (!File.Exists(v_filename)) //如果文件存在,则创建File.AppendText对象
                {

                    sr = File.CreateText(v_filename);

                    sr.Close();

                }

                using (FileStream fs = new FileStream(v_filename, System.IO.FileMode.Append, System.IO.FileAccess.Write, System.IO.FileShare.Write))
                {

                    using (sr = new StreamWriter(fs))
                    {

                        sr.WriteLine(DateTime.Now.ToString("hh:mm:ss") + "     " + content);

                        sr.Close();

                    }

                    fs.Close();

                }

                return true;

            }

            catch { return false; }

        }
    }
}
