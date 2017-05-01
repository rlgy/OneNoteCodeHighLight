namespace HighLightForm
{
    partial class CodeInputForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cb_lineNumber = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cb_theme = new System.Windows.Forms.ComboBox();
            this.cb_lang = new System.Windows.Forms.ComboBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.bt_clear = new System.Windows.Forms.Button();
            this.bt_insert = new System.Windows.Forms.Button();
            this.cb_size = new System.Windows.Forms.ComboBox();
            this.cb_font = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCode = new ICSharpCode.TextEditor.TextEditorControl();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "语言";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Consolas", 12F);
            this.label2.Location = new System.Drawing.Point(218, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 19);
            this.label2.TabIndex = 2;
            this.label2.Text = "主题";
            // 
            // cb_lineNumber
            // 
            this.cb_lineNumber.AutoSize = true;
            this.cb_lineNumber.Font = new System.Drawing.Font("Consolas", 12F);
            this.cb_lineNumber.Location = new System.Drawing.Point(510, 24);
            this.cb_lineNumber.Name = "cb_lineNumber";
            this.cb_lineNumber.Size = new System.Drawing.Size(100, 23);
            this.cb_lineNumber.TabIndex = 4;
            this.cb_lineNumber.Text = "显示行号";
            this.cb_lineNumber.UseVisualStyleBackColor = true;
            this.cb_lineNumber.CheckedChanged += new System.EventHandler(this.cb_lineNumber_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cb_theme);
            this.panel1.Controls.Add(this.cb_lang);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.cb_lineNumber);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(615, 60);
            this.panel1.TabIndex = 5;
            // 
            // cb_theme
            // 
            this.cb_theme.Font = new System.Drawing.Font("Consolas", 12F);
            this.cb_theme.FormattingEnabled = true;
            this.cb_theme.Location = new System.Drawing.Point(269, 23);
            this.cb_theme.Name = "cb_theme";
            this.cb_theme.Size = new System.Drawing.Size(108, 27);
            this.cb_theme.TabIndex = 6;
            this.cb_theme.SelectedIndexChanged += new System.EventHandler(this.cb_theme_SelectedIndexChanged);
            // 
            // cb_lang
            // 
            this.cb_lang.Font = new System.Drawing.Font("Consolas", 12F);
            this.cb_lang.FormattingEnabled = true;
            this.cb_lang.Location = new System.Drawing.Point(63, 23);
            this.cb_lang.Name = "cb_lang";
            this.cb_lang.Size = new System.Drawing.Size(108, 27);
            this.cb_lang.TabIndex = 5;
            this.cb_lang.SelectedIndexChanged += new System.EventHandler(this.cx_lang_SelectedIndexChanged);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.bt_clear);
            this.panel2.Controls.Add(this.bt_insert);
            this.panel2.Controls.Add(this.cb_size);
            this.panel2.Controls.Add(this.cb_font);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Location = new System.Drawing.Point(2, 439);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(612, 58);
            this.panel2.TabIndex = 6;
            // 
            // bt_clear
            // 
            this.bt_clear.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_clear.Location = new System.Drawing.Point(413, 22);
            this.bt_clear.Name = "bt_clear";
            this.bt_clear.Size = new System.Drawing.Size(87, 25);
            this.bt_clear.TabIndex = 9;
            this.bt_clear.Text = "清空";
            this.bt_clear.UseVisualStyleBackColor = true;
            this.bt_clear.Click += new System.EventHandler(this.bt_clear_Click);
            // 
            // bt_insert
            // 
            this.bt_insert.Font = new System.Drawing.Font("Consolas", 12F);
            this.bt_insert.Location = new System.Drawing.Point(523, 22);
            this.bt_insert.Name = "bt_insert";
            this.bt_insert.Size = new System.Drawing.Size(87, 25);
            this.bt_insert.TabIndex = 8;
            this.bt_insert.Text = "插入";
            this.bt_insert.UseVisualStyleBackColor = true;
            this.bt_insert.Click += new System.EventHandler(this.bt_insert_Click);
            // 
            // cb_size
            // 
            this.cb_size.Font = new System.Drawing.Font("Consolas", 12F);
            this.cb_size.FormattingEnabled = true;
            this.cb_size.Location = new System.Drawing.Point(269, 21);
            this.cb_size.Name = "cb_size";
            this.cb_size.Size = new System.Drawing.Size(108, 27);
            this.cb_size.TabIndex = 7;
            this.cb_size.SelectedIndexChanged += new System.EventHandler(this.cb_size_SelectedIndexChanged);
            // 
            // cb_font
            // 
            this.cb_font.Font = new System.Drawing.Font("Consolas", 12F);
            this.cb_font.FormattingEnabled = true;
            this.cb_font.Location = new System.Drawing.Point(63, 21);
            this.cb_font.Name = "cb_font";
            this.cb_font.Size = new System.Drawing.Size(108, 27);
            this.cb_font.TabIndex = 6;
            this.cb_font.SelectedIndexChanged += new System.EventHandler(this.cb_font_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Consolas", 12F);
            this.label4.Location = new System.Drawing.Point(218, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 19);
            this.label4.TabIndex = 1;
            this.label4.Text = "大小";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Consolas", 12F);
            this.label3.Location = new System.Drawing.Point(12, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 19);
            this.label3.TabIndex = 0;
            this.label3.Text = "字体";
            // 
            // txtCode
            // 
            this.txtCode.IsReadOnly = false;
            this.txtCode.Location = new System.Drawing.Point(2, 61);
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(612, 378);
            this.txtCode.TabIndex = 7;
            // 
            // CodeInputForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(617, 497);
            this.Controls.Add(this.txtCode);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "CodeInputForm";
            this.Text = "InputCode";
            this.Load += new System.EventHandler(this.CodeInputForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox cb_lineNumber;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cb_theme;
        private System.Windows.Forms.ComboBox cb_lang;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button bt_insert;
        private System.Windows.Forms.ComboBox cb_size;
        private System.Windows.Forms.ComboBox cb_font;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private ICSharpCode.TextEditor.TextEditorControl txtCode;
        private System.Windows.Forms.Button bt_clear;
    }
}

