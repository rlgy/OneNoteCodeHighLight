using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HighLightEngine
{
    public interface IGenerateHighLight
    {
        /// <summary> 产生HighLight Code </summary>
        /// <returns> 返回输出的文档的路劲 </returns>
        string GenerateHighLightCode(HighLightParameter paramer);
    }

    public class HighLightParameter
    {
        /// <summary> 内容 </summary>
        public string Content { get; set; }

        /// <summary> 语法类型 </summary>
        public string lang { get; set; }

        /// <summary> 高亮样式 </summary>
        public string theme { get; set; }

        /// <summary> 是否显示行号 </summary>
        public bool showLineNumber { get; set; }

        /// <summary>字体类型</summary>
        public string font { get; set; }

        /// <summary>字体大小</summary>
        public string size { get; set; }

        /// <summary> 档案名称 </summary>
        public string fileName { get; set; }
    }
}
