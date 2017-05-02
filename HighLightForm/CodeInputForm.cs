﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ICSharpCode.TextEditor.Document;
using HighLightEngine;


namespace HighLightForm
{
    public partial class CodeInputForm : Form
    {
        private string fileName;
        public CodeInputForm(string fileName)
        {
            this.fileName = fileName;
            InitializeComponent();
        }

        public CodeInputForm()
        {
            InitializeComponent();
        }

        private void CodeInputForm_Load(object sender, EventArgs e)
        {
            //添加控件元素
            string[] font_family = new string[2] { "Consolas", "Courier New" };//字体类型
            int[] font_size = new int[7] { 10, 11, 12, 13, 14, 15, 16 };//字体大小类型

            //获取lang文件
            string path = "./highlight/langDefs";
            DirectoryInfo folder = new DirectoryInfo(path);
            foreach (FileInfo file in folder.GetFiles("*.lang"))
            {
                cb_lang.Items.Add(file.Name.Substring(0, file.Name.IndexOf(".")));
            }
            cb_lang.Text = Properties.Settings.Default.default_lang;

            //获取theme文件
            path = "./highlight/themes";
            folder = new DirectoryInfo(path);
            foreach (FileInfo file in folder.GetFiles("*.theme"))
            {
                cb_theme.Items.Add(file.Name.Substring(0, file.Name.IndexOf(".")));
            }
            cb_theme.Text = Properties.Settings.Default.default_theme;

            //添加字体类型
            cb_font.Items.AddRange(font_family);
            cb_font.Text = Properties.Settings.Default.default_font;

            //添加字体大小
            for (int i = 0; i < 7; i++)
            {
                cb_size.Items.Add(font_size[i].ToString());
            }
            cb_size.Text = Properties.Settings.Default.default_size;

            //是否显示行号
            cb_lineNumber.Checked = Properties.Settings.Default.default_showLineNum;

            this.txtCode.Document.HighlightingStrategy = HighlightingStrategyFactory.CreateHighlightingStrategy(this.CodeTypeTransform(this.cb_lang.Text));
            this.txtCode.Encoding = Encoding.UTF8;


        }

        private void cx_lang_SelectedIndexChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.default_lang = (string)cb_lang.SelectedItem;
            Properties.Settings.Default.Save();
            this.txtCode.Document.HighlightingStrategy = HighlightingStrategyFactory.CreateHighlightingStrategy(this.CodeTypeTransform(this.cb_lang.Text));
        }


        private void cb_theme_SelectedIndexChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.default_theme = (string)cb_theme.SelectedItem;
            Properties.Settings.Default.Save();
        }

        private void cb_font_SelectedIndexChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.default_font = (string)cb_font.SelectedItem;
            Properties.Settings.Default.Save();
        }

        private void cb_size_SelectedIndexChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.default_size = (string)cb_size.SelectedItem;
            Properties.Settings.Default.Save();
        }

        private void cb_lineNumber_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.default_showLineNum = cb_lineNumber.Checked;
            Properties.Settings.Default.Save();
        }
        private string CodeTypeTransform(string codeType)
        {
            string result = string.Empty;
            switch (codeType.ToLower())
            {
                case "csharp":
                    result = "C#";
                    break;
                case "php":
                    result = "PHP";
                    break;
                case "java":
                    result = "Java";
                    break;
                case "c":
                    result = "C++.NET";
                    break;
                default:
                    result = "";
                    break;
            }
            return result;
        }

        private void bt_clear_Click(object sender, EventArgs e)
        {
            this.txtCode.Text = "";
            this.txtCode.Refresh();
        }

        private void bt_insert_Click(object sender, EventArgs e)
        {
            if(this.txtCode.Text=="")
            {
                this.Close();
                return;
            }

            IGenerateHighLight generate = new HighLightEngine.HighLightEngine();

            string outPutFileName = String.Empty;
            var v = Properties.Settings.Default;

            HighLightParameter paramer = new HighLightParameter()
            {
                Content = this.txtCode.Text,
                lang = v.default_lang,
                theme = v.default_theme,
                font = v.default_font,
                size = v.default_size,
                showLineNumber = v.default_showLineNum,
                fileName = this.fileName
            };

            try
            {
                outPutFileName = generate.GenerateHighLightCode(paramer);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                this.Dispose();
                this.Close();
                return;
            }
            this.Dispose();
            this.Close();
        }
    }
}
