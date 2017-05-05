using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HighLightBuild
{
    public class XmlBuild
    {
        /// <summary>
        /// html文件路径
        /// </summary>
        private string _fileName;

        /// <summary>
        /// 字体颜色
        /// </summary>
        private string _fontColor;

        /// <summary>
        /// 代码区背景颜色
        /// </summary>
        private string _backgroundColor;

        /// <summary>
        /// 代码字体
        /// </summary>
        private string _font;

        /// <summary>
        /// 代码字体大小
        /// </summary>
        private string _size;

        /// <summary>
        /// 行数颜色
        /// </summary>
        private string _lineColor;

        private string _quickStyleIndex;

        /// <summary>
        /// 包含html内容的字符串数组，其每一个元素对应一行在OneNote中显示的代码
        /// </summary>
        private string[] _lines;

        /// <summary>
        /// OneNote XML 的命名空间
        /// </summary>
        private XNamespace _ns;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="fileName">highlight生成的html文件内容</param>
        /// <param name="ns">OneNote XML 的命名空间</param>
        public XmlBuild(string fileName, XNamespace ns)
        {
            _fileName = fileName;
            _ns = ns;
        }

        /// <summary>
        /// 调用htmlParser解析由highlight产生的html文件
        /// </summary>
        /// <returns>返回-1代表初始化参数失败</returns>
        private int InitParam()
        {
            HtmlParser hp = new HtmlParser(_fileName);

            _lines = hp.LineCollect(out _fontColor, out _backgroundColor, out _font, out _size);
            if (_lines.Length <= 0)
                return -1;

            return 0;
        }

        /// <summary>
        /// 生成需要插入OneNote XML中的XML节点
        /// </summary>
        /// <param name="Table">节点的名称</param>
        private void XmlTableBuilding(out XElement Table)
        {

            Table = new XElement(_ns + "Table");
            XElement Columns = new XElement(_ns + "Columns");
            XElement Column = new XElement(_ns + "Column");
            XElement Row = new XElement(_ns + "Row");
            XElement Cell = new XElement(_ns + "Cell");
            XElement OEChildren = new XElement(_ns + "OEChildren");

            //为Column添加两个基本属性
            Column.SetAttributeValue("index", "0");
            Column.SetAttributeValue("width", "0");
            Cell.SetAttributeValue("shadingColor", _backgroundColor);

            //检测是否需要显示行号
            Regex r = new Regex("<span style=\"color:(?<lineColor>[#0-9A-Za-z]{0,7})\">(?:[0-9]{2,})\\s+</span>");
            Match m = r.Match(_lines[0]);
            if (m.Success)
            {
                _lineColor = m.Groups["lineColor"].Value;

                for (int i = 0; i < _lines.Length; i++)
                {
                    if (_lines[i] != null)
                    {
                        XElement OE = new XElement(_ns + "OE");
                        OE.SetAttributeValue("quickStyleIndex", _quickStyleIndex);
                        //行号以列表显示，方便代码的粘贴复制
                        XElement List = new XElement(_ns + "List");
                        XElement Number = new XElement(_ns + "Number");
                        //< one:Number numberSequence = "0" numberFormat = "##." fontColor = "#0000FF" 
                        //font = "Courier New" language = "2052" text = "1." />

                        Number.SetAttributeValue("numberSequence", "22");
                        Number.SetAttributeValue("numberFormat", "##");
                        Number.SetAttributeValue("fontColor", _lineColor);
                        Number.SetAttributeValue("font", _font);
                        Number.SetAttributeValue("fontSize", _size);
                        Number.SetAttributeValue("text", i + 1);
                        List.Add(Number);

                        OE.Add(List);
                        OE.Add(new XElement(_ns + "T", new XCData(_lines[i].Substring(_lines[i].IndexOf("</span>") + "</span>".Length))));
                        OEChildren.Add(OE);
                    }
                }

            }
            else
            {
                foreach (string line in _lines)
                {

                    if (line != null)
                    {
                        XElement OE = new XElement(_ns + "OE");
                        OE.SetAttributeValue("quickStyleIndex", _quickStyleIndex);
                        OE.Add(new XElement(_ns + "T", new XCData(line)));
                        OEChildren.Add(OE);
                    }
                }
            }
            Cell.Add(OEChildren);
            Row.Add(Cell);
            Columns.Add(Column);
            Table.Add(Columns);
            Table.Add(Row);
        }

        /// <summary>
        /// 将代码插入光标的下一行
        /// </summary>
        /// <param name="page">当前页面</param>
        /// <param name="point">光标所在位置</param>
        public void XmlReBuilding(ref XElement page, ref XElement point)
        {
            if (InitParam() < 0)
                throw new Exception("HtmlBuild Run Error");

            //新建快速样式表
            //<one:QuickStyleDef 
            //index ="2" 
            //name ="p" 
            //fontColor ="automatic" 
            //highlightColor ="automatic" 
            //font="Consolas" 
            //fontSize="12.0" 
            //spaceBefore="0.0" 
            //spaceAfter="0.0" />
            XElement qLast = page.Descendants(_ns + "QuickStyleDef").Last();
            int index = int.Parse(qLast.Attribute("index").Value);
            _quickStyleIndex = (++index).ToString();

            XElement QuickStyleDef = new XElement(_ns + "QuickStyleDef");
            QuickStyleDef.SetAttributeValue("index", _quickStyleIndex);
            QuickStyleDef.SetAttributeValue("name", "p");
            QuickStyleDef.SetAttributeValue("fontColor", _fontColor);
            QuickStyleDef.SetAttributeValue("highlightColor", "automatic");
            QuickStyleDef.SetAttributeValue("font", _font);
            QuickStyleDef.SetAttributeValue("fontSize", _size);
            QuickStyleDef.SetAttributeValue("spaceBefore", "0.0");
            QuickStyleDef.SetAttributeValue("spaceAfter", "0.0");

            qLast.AddAfterSelf(QuickStyleDef);





            XElement table = new XElement(_ns + "Table");
            this.XmlTableBuilding(out table);

            XElement OE = new XElement(_ns + "OE");
            OE.Add(table);
            var pointFather = point.Ancestors(_ns + "OE").First();


            pointFather.AddBeforeSelf(OE);
        }
    }
}
