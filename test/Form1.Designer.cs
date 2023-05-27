
namespace test
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.upload = new System.Windows.Forms.Button();
            this.filepath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.open = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.KCIVWriteXS = new System.Windows.Forms.Button();
            this.statusStrip_factor = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelFactor = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip_factor.SuspendLayout();
            this.SuspendLayout();
            // 
            // upload
            // 
            this.upload.Location = new System.Drawing.Point(618, 12);
            this.upload.Name = "upload";
            this.upload.Size = new System.Drawing.Size(129, 21);
            this.upload.TabIndex = 0;
            this.upload.Text = "开始上传/暂停上传";
            this.upload.UseVisualStyleBackColor = true;
            this.upload.Click += new System.EventHandler(this.upload_Click);
            // 
            // filepath
            // 
            this.filepath.Location = new System.Drawing.Point(95, 12);
            this.filepath.Name = "filepath";
            this.filepath.Size = new System.Drawing.Size(478, 21);
            this.filepath.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(12, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 14);
            this.label1.TabIndex = 2;
            this.label1.Text = "文件路径：";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Cursor = System.Windows.Forms.Cursors.Default;
            this.richTextBox1.Location = new System.Drawing.Point(12, 39);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(816, 438);
            this.richTextBox1.TabIndex = 3;
            this.richTextBox1.Text = "";
            // 
            // open
            // 
            this.open.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.open.Location = new System.Drawing.Point(579, 11);
            this.open.Name = "open";
            this.open.Size = new System.Drawing.Size(33, 21);
            this.open.TabIndex = 4;
            this.open.Text = "...";
            this.open.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.open.UseVisualStyleBackColor = true;
            this.open.Click += new System.EventHandler(this.open_Click);
            // 
            // KCIVWriteXS
            // 
            this.KCIVWriteXS.Location = new System.Drawing.Point(753, 12);
            this.KCIVWriteXS.Name = "KCIVWriteXS";
            this.KCIVWriteXS.Size = new System.Drawing.Size(75, 21);
            this.KCIVWriteXS.TabIndex = 5;
            this.KCIVWriteXS.Text = "系数录入";
            this.KCIVWriteXS.UseVisualStyleBackColor = true;
            this.KCIVWriteXS.Click += new System.EventHandler(this.KCIVWriteXS_Click);
            // 
            // statusStrip_factor
            // 
            this.statusStrip_factor.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelFactor});
            this.statusStrip_factor.Location = new System.Drawing.Point(0, 485);
            this.statusStrip_factor.Name = "statusStrip_factor";
            this.statusStrip_factor.Size = new System.Drawing.Size(841, 22);
            this.statusStrip_factor.TabIndex = 6;
            this.statusStrip_factor.Text = "statusStrip1";
            // 
            // toolStripStatusLabelFactor
            // 
            this.toolStripStatusLabelFactor.Name = "toolStripStatusLabelFactor";
            this.toolStripStatusLabelFactor.Size = new System.Drawing.Size(160, 17);
            this.toolStripStatusLabelFactor.Text = "toolStripStatusLabelFactor";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(841, 507);
            this.Controls.Add(this.statusStrip_factor);
            this.Controls.Add(this.KCIVWriteXS);
            this.Controls.Add(this.open);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.filepath);
            this.Controls.Add(this.upload);
            this.MinimumSize = new System.Drawing.Size(857, 546);
            this.Name = "Form1";
            this.Text = "热量仪采集程序";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.statusStrip_factor.ResumeLayout(false);
            this.statusStrip_factor.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button upload;
        private System.Windows.Forms.TextBox filepath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button open;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button KCIVWriteXS;
        private System.Windows.Forms.StatusStrip statusStrip_factor;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelFactor;
    }
}

