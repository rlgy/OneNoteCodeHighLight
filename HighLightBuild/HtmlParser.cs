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

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="fileName">需要读取的html文件的路径</param>
        public HtmlParser(string fileName)
        {
            _fileName = fileName;
            if (File.Exists(_fileName))
                _fileContent = File.ReadAllText(_fileName, Encoding.UTF8);
        }

        /// <summary>
        /// 处理由highlight生成的html文件内容
        /// </summary>
        /// <param name="fontColor">字体的颜色</param>
        /// <param name="backgroundColor">背景颜色</param>
        /// <param name="font">字体类型</param>
        /// <param name="size">字体大小</param>
        /// <returns>包含html内容的字符串数组，数组的每个元素对应一行代码</returns>
        public string[] LineCollect(out string fontColor, out string backgroundColor, out string font, out string size)
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
                        if (spanIndex >= 0)
                            returnLines[i] = arrayLines[i].Substring(spanIndex);
                    }
                }
                else if (arrayLines[i].IndexOf("<span") >= 0)
                {
                    returnLines[i] = arrayLines[i];
                }
            }

            //throw new Exception(font);
            returnLines = returnLines.Where(n => !string.IsNullOrEmpty(n)).ToArray();
            //处理独立与span标签之外的代码
            for (int i = 0; i < returnLines.Length; i++)
            {
                if (HtmlParser.AddTagToWords(ref returnLines[i], "span", 0, "background-color:" + backgroundColor + "; color:" + fontColor) == -1)
                    throw new Exception("Html Error");
            }


            return returnLines;
        }

        /// <summary>
        /// 查找html字符串中的标签,将标签之外的内容用标签包裹起来
        /// </summary>
        /// <param name="line">查找的内容</param>
        /// <param name="tag">标签</param>
        /// <param name="start">开始查找的位置</param>
        /// <param name="style">需要添加的内联css</param>
        /// <returns>返回-1表示html内容存在错误</returns>
        private static int AddTagToWords(ref string line, string tag, int start, string style)
        {
            if (start == line.Length)
                return 0;
            int fstart;
            int fend;

            string tagStartT = "<{0}";
            string tagStopT = "</{0}>";

            string repleaseTagT = "<{0} style=\"{1}\">";

            fstart = line.IndexOf(String.Format(tagStartT, tag), start);

            if (fstart < 0)
            {
                line = line.Substring(0, start) + String.Format(repleaseTagT, tag, style)
                    + line.Substring(start) + String.Format(tagStopT, tag);
            }
            else if (fstart == start)
            {
                fend = line.IndexOf(String.Format(tagStopT, tag), start);
                if (fend < 0)
                    return -1;//html error
                else
                    start = fend + String.Format(tagStopT, tag).Length;
            }
            else if (fstart > start)
            {
                line = line.Substring(0, start) + String.Format(repleaseTagT, tag, style)
                    + line.Substring(start, fstart - start) + String.Format(tagStopT, tag) + line.Substring(fstart);
            }

            return HtmlParser.AddTagToWords(ref line, tag, start, style);
        }

    }
}
