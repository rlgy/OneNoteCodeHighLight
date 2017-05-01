using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace HighLightBuild
{

    class HtmlParser
    {
        /// <summary>
        /// html文件位置
        /// </summary>
        private string _fileName;

        /// <summary>
        /// html文件内容
        /// </summary>
        private string _fileContent;


        public HtmlParser(string fileName)
        {
            _fileName = fileName;
            if (File.Exists(_fileName))
                _fileContent = File.ReadAllText(_fileName, Encoding.UTF8);
        }

        public string[] LineCollect(out string fontColor,out string backgroundColor, out string font, out string size)
        {
            fontColor = "#000000";
            backgroundColor = "#ffffff";
            font = "Consolas";
            size = "12";
            


            string[] arrayLines = _fileContent.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            string[] returnLines = new string[arrayLines.Length];

            for (int i = 0; i < arrayLines.Length; i++)
            {
                if (arrayLines[i].IndexOf("<pre") >= 0)
                {
                    Regex r = new Regex("color:(.+); background-color:(.+); font-size:([0-9]{2})pt; font-family:'(.+)';");
                    Match m = r.Match(arrayLines[i]);
                    if (m.Success)
                    {
                        GroupCollection outParam = m.Groups;
                        if (outParam.Count >= 5)
                        {
                            fontColor = outParam[1].Value;
                            backgroundColor = outParam[2].Value;
                            size = outParam[3].Value;
                            font = outParam[4].Value;
                        }
                        int spanIndex = arrayLines[i].IndexOf("<span");
                        returnLines[i] = arrayLines[i].Substring(spanIndex);
                    }
                }
                else if (arrayLines[i].IndexOf("<span") >= 0)
                {
                    returnLines[i] = arrayLines[i];
                }
            }

            //throw new Exception(font);

            return returnLines;
        }
    }
}
