﻿
namespace Plugin.MarkMatSingle
{
    partial class WinMarkMatSingle
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WinMarkMatSingle));
            this.axMMMark1 = new AxMMMARKLib.AxMMMark();
            ((System.ComponentModel.ISupportInitialize)(this.axMMMark1)).BeginInit();
            this.SuspendLayout();
            // 
            // axMMMark1
            // 
            this.axMMMark1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.axMMMark1.Enabled = true;
            this.axMMMark1.Location = new System.Drawing.Point(0, 3);
            this.axMMMark1.Name = "axMMMark1";
            this.axMMMark1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axMMMark1.OcxState")));
            this.axMMMark1.Size = new System.Drawing.Size(147, 144);
            this.axMMMark1.TabIndex = 0;
            // 
            // UCMarkMatSingle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.axMMMark1);
            this.Name = "UCMarkMatSingle";
            ((System.ComponentModel.ISupportInitialize)(this.axMMMark1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private AxMMMARKLib.AxMMMark axMMMark1;
    }
}
