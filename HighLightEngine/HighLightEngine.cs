using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Reflection;
using System.Diagnostics;

namespace HighLightEngine
{
    public class HighLightEngine : IGenerateHighLight
    {

        public string lang { get; set; }
        public string theme { get; set; }
        public string font { get; set; }
        public string size { get; set; }
        public string content { get; set; }
        public bool showLineNumber { get; set; }
        public string fileName { get; set; }
        
        //private static HighLightSection _section;
        private void initParamer(HighLightParameter paramer)
        {
            this.lang = paramer.lang;
            this.theme = paramer.theme;
            this.font = paramer.font;
            this.size = paramer.size;
            this.content = paramer.Content;
            this.showLineNumber = paramer.showLineNumber;
            this.fileName = paramer.fileName;
        }
        /// <summary>
        /// 代码渲染的主要函数
        /// </summary>
        /// <param name="paramer"></param>
        /// <returns></returns>
        public string GenerateHighLightCode(HighLightParameter paramer)
        {
            initParamer(paramer);
            string tempPath = Path.GetTempPath();
            string inputFileName = Path.Combine(tempPath, fileName);
            string outputFileName = inputFileName + ".html";

            File.WriteAllText(inputFileName, this.content, Encoding.UTF8);

            

            //调用highlight
            //var workingDirectory = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),_section.FolderName);
            string workingDirectory = "E:\\www\\visual studio\\HighLightNoteAddIns\\Build\\highlight\\";
            

            ProcessStartInfo info = new ProcessStartInfo();
            info.WorkingDirectory = workingDirectory;
            info.FileName = "highlight.exe";
            info.Arguments = " ";
            info.WindowStyle = ProcessWindowStyle.Hidden;


            Process p = new Process();
            p.StartInfo = info;
            p.Start();
            
            if (!p.HasExited)
                p.WaitForExit();

            if (!File.Exists(outputFileName))
                throw new FileNotFoundException("找不到outputFile");

            File.Delete(inputFileName);
            return outputFileName;
        }
    }
}
