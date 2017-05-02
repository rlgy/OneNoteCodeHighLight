using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HighLightBuild
{
    public class XmlBuild
    {
        /// <summary>
        /// html file path
        /// </summary>
        private string _fileName;

        /// <summary>
        /// 字体颜色
        /// </summary>
        private string _fontColor;

        /// <summary>
        /// code raea's background color
        /// </summary>
        private string _backgroundColor;

        /// <summary>
        /// code font type
        /// </summary>
        private string _font;

        /// <summary>
        /// code font size
        /// </summary>
        private string _size;

        private string _quickStyleIndex;

        /// <summary>
        /// code line collection
        /// </summary>
        private string[] _lines;

        /// <summary>
        /// xml namespace
        /// </summary>
        private XNamespace _ns;


        public XmlBuild(string fileName, XNamespace ns)
        {
            _fileName = fileName;
            _ns = ns;
            //_lines = new string[100];
        }

        /// <summary>
        /// 调用htmlParser解析由highlight产生的html文件
        /// </summary>
        /// <returns></returns>
        private int InitParam()
        {
            HtmlParser hp = new HtmlParser(_fileName);

            _lines = hp.LineCollect(out _fontColor, out _backgroundColor, out _font, out _size);

            if (_lines.Length <= 0)
                return -1;

            return 0;
        }

        private int XmlTableBuilding(out XElement Table)
        {

            Table = new XElement(_ns + "Table");
            XElement Columns = new XElement(_ns + "Columns");
            XElement Column = new XElement(_ns + "Column");
            XElement Row = new XElement(_ns + "Row");
            XElement Cell = new XElement(_ns + "Cell");
            XElement OEChildren = new XElement(_ns + "OEChildren");

            Column.SetAttributeValue("index", "0");
            Column.SetAttributeValue("width", "0");
            Cell.SetAttributeValue("shadingColor", _backgroundColor);

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

            //build
            Cell.Add(OEChildren);
            Row.Add(Cell);
            Columns.Add(Column);
            Table.Add(Columns);
            Table.Add(Row);
            return 0;
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

            //XElement OECollection = new XElement(_ns + "OEChildren");
            //foreach (string line in _lines)
            //{

            //    if (line != null)
            //    {
            //        OECollection.Add(new XElement(_ns + "OE",
            //        new XElement(_ns + "T", new XCData(line))));
            //    }
            //}

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
            if (this.XmlTableBuilding(out table) < 0)
                throw new Exception("Create table failed!");

            XElement OE = new XElement(_ns + "OE");
            OE.Add(table);
            var pointFather = point.Ancestors(_ns + "OE").First();


            pointFather.AddBeforeSelf(OE);


        }
    }
}
