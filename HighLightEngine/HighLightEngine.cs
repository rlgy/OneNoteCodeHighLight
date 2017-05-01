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
        private static HighLightSection _section = ConfigurationManager.GetSection("HighLightSection") as HighLightSection;
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
            var workingDirectory = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),_section.FolderName);
            ProcessStartInfo info = new ProcessStartInfo();
            info.WorkingDirectory = workingDirectory;
            info.FileName = _section.ProcessName;
            info.Arguments = " " + GenerateArguments(inputFileName, outputFileName);
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
        /// <summary> 产生HighLight.exe 参数 </summary>
        private string GenerateArguments(string inputFileName, string outputFileName)
        {
            //HighLightSection _section = ConfigurationManager.GetSection("HighLightSection") as HighLightSection;
            //HighLightSection _section = (HighLightSection)ConfigurationManager.GetSection("HighLightSection");
            if (_section == null) throw new Exception("Config 内找不到 HighLightSection 区段");

            StringBuilder sb = new StringBuilder();

            ReadConfigCollection(sb, _section.GeneralArguments);
            ReadConfigCollection(sb, _section.OutputArguments);

            if (this.showLineNumber)
                sb.Append(" " + _section.OutputArguments["LineNumbers"].Key);


            string arguments = sb.ToString().TemplateSubstitute(new
            {
                inputFileName = String.Format("\"{0}\"", inputFileName),
                outputFileName = String.Format("\"{0}\"", outputFileName),
                lang = this.lang,
                theme = this.theme,
                font = String.Format("\"{0}\"", this.font),
                size = this.size
            });

            return arguments;
        }

        /// <summary> 读取 ConfigurationElementCollection </summary>
        private void ReadConfigCollection(StringBuilder sb, ConfigurationElementCollection collection)
        {
            foreach (Argument item in collection)
            {
                if (item.Option)
                    continue;

                sb.Append(item.Key);
                if (!String.IsNullOrEmpty(item.Value))
                    sb.Append(" " + item.Value);
               
                sb.Append(" ");
            }
        }

    }
}
