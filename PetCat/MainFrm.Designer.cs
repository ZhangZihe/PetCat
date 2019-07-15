namespace PetCat
{
    partial class MainFrm
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
            this.components = new System.ComponentModel.Container();
            this.TimerMain = new System.Windows.Forms.Timer(this.components);
            this.NotifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.ToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.对话ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.喂食ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.工具箱ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.空ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.退出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // TimerMain
            // 
            this.TimerMain.Interval = 30;
            // 
            // NotifyIcon
            // 
            this.NotifyIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.NotifyIcon.BalloonTipText = "喵";
            this.NotifyIcon.BalloonTipTitle = "喵";
            this.NotifyIcon.Text = "喵";
            this.NotifyIcon.Visible = true;
            // 
            // ToolTip
            // 
            this.ToolTip.AutoPopDelay = 5000;
            this.ToolTip.InitialDelay = 1000;
            this.ToolTip.IsBalloon = true;
            this.ToolTip.OwnerDraw = true;
            this.ToolTip.ReshowDelay = 100;
            this.ToolTip.ShowAlways = true;
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.对话ToolStripMenuItem,
            this.喂食ToolStripMenuItem,
            this.toolStripSeparator2,
            this.工具箱ToolStripMenuItem,
            this.toolStripSeparator1,
            this.退出ToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(113, 104);
            // 
            // 对话ToolStripMenuItem
            // 
            this.对话ToolStripMenuItem.Name = "对话ToolStripMenuItem";
            this.对话ToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.对话ToolStripMenuItem.Text = "对话";
            this.对话ToolStripMenuItem.Click += new System.EventHandler(this.对话ToolStripMenuItem_Click);
            // 
            // 喂食ToolStripMenuItem
            // 
            this.喂食ToolStripMenuItem.Name = "喂食ToolStripMenuItem";
            this.喂食ToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.喂食ToolStripMenuItem.Text = "喂食";
            this.喂食ToolStripMenuItem.Click += new System.EventHandler(this.喂食ToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(109, 6);
            // 
            // 工具箱ToolStripMenuItem
            // 
            this.工具箱ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.空ToolStripMenuItem});
            this.工具箱ToolStripMenuItem.Name = "工具箱ToolStripMenuItem";
            this.工具箱ToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.工具箱ToolStripMenuItem.Text = "工具箱";
            // 
            // 空ToolStripMenuItem
            // 
            this.空ToolStripMenuItem.Name = "空ToolStripMenuItem";
            this.空ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.空ToolStripMenuItem.Text = "空";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(109, 6);
            // 
            // 退出ToolStripMenuItem
            // 
            this.退出ToolStripMenuItem.Name = "退出ToolStripMenuItem";
            this.退出ToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.退出ToolStripMenuItem.Text = "退出";
            this.退出ToolStripMenuItem.Click += new System.EventHandler(this.退出ToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(192, 22);
            this.toolStripMenuItem1.Text = "toolStripMenuItem1";
            // 
            // MainFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(144, 152);
            this.ContextMenuStrip = this.contextMenuStrip;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MainFrm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "喵";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.PetCat_Load);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.PetCat_MouseClick);
            this.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.PetCat_MouseDoubleClick);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PetCat_MouseDown);
            this.MouseLeave += new System.EventHandler(this.PetCat_MouseLeave);
            this.MouseHover += new System.EventHandler(this.PetCat_MouseHover);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PetCat_MouseMove);
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.NotifyIcon NotifyIcon;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem 对话ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem 退出ToolStripMenuItem;
        protected internal System.Windows.Forms.Timer TimerMain;
        public System.Windows.Forms.ToolTip ToolTip;
        private System.Windows.Forms.ToolStripMenuItem 喂食ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem 工具箱ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 空ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
    }
}

