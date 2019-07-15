using PetCat.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PetCat
{
    public partial class MainFrm : Form
    {
        public MainFrm()
        {
            InitializeComponent();
        }

        private CatAct catAct;
        private TalkFrm talkFrm;


        #region 初始化主程序

        private void PetCat_Load(object sender, EventArgs e)
        {
            try
            {
                catAct = new CatAct(this);
                catAct.LoadResources(Path.Combine(Application.StartupPath, "Resources"));
                catAct.CurAct = ActType.Tail;
                talkFrm = new TalkFrm(this);

                TimerMain.Enabled = true;
                Task.Factory.StartNew(async () =>
                {
                    await Task.Delay(500);
                    await ShowSentence(Resources.Hello, Resources.MouseOver);
                });
                LoadTools();
            }
            catch(Exception ex)
            {
                MessageBox.Show($"资源丢失, 程序将自动关闭：{ex.Message}");
                Application.Exit();
            }
        }

        private void LoadTools()
        {
            var toolsPath = Path.Combine(Application.StartupPath, "Tools");
            var toolsDirs = new DirectoryInfo(toolsPath);
            if (!toolsDirs.Exists)
                return;

            var toolStripMenuItems = new List<ToolStripMenuItem>();
            foreach (var dirInfo in toolsDirs.GetDirectories())
            {
                var exeFile = dirInfo.GetFiles("*.exe").FirstOrDefault();
                if (exeFile == null)
                    continue;

                var helloFile = dirInfo.GetFiles("hello.txt").FirstOrDefault();
                toolStripMenuItems.Add(new ToolStripMenuItem(dirInfo.Name, null,
                    (object s, EventArgs args) =>
                    {
                        Process.Start(new ProcessStartInfo(exeFile.FullName));
                        if (helloFile != null)
                        {
                            Task.Factory.StartNew(async () =>
                            {
                                await Task.Delay(1000);
                                var hello = File.ReadAllLines(helloFile.FullName);
                                await ShowSentence(hello);
                            });
                        }
                    }));
            }
            if (toolStripMenuItems.Count > 0)
            {
                工具箱ToolStripMenuItem.DropDownItems.Clear();
                工具箱ToolStripMenuItem.DropDownItems.AddRange(toolStripMenuItems.ToArray());
            }
        }

        public async Task ShowSentence(params string[] sentence)
        {
            if (sentence.Count() == 0)
                return;

            Invoke(new Action(() =>
            {
                Enabled = false;
                catAct.CurAct = ActType.Tail;
            }));
            
            foreach (var item in sentence)
            {
                Invoke(new Action(() => Focus()));
                Invoke(new Action(() => ToolTip.Show(item, this, catAct.TipPoint, 3000)));
                await Task.Delay(1000);
                Invoke(new Action(() => ToolTip.Hide(this)));
                await Task.Delay(500);
            }
            Invoke(new Action(() => Enabled = true));
        }

        #endregion


        #region 响应鼠标

        private void PetCat_MouseHover(object sender, EventArgs e)
        {
            if (catAct.CurAct == ActType.Walk || catAct.CurAct == ActType.WalkR || catAct.CurAct == ActType.Tail)
            {
                catAct.LastAct = catAct.CurAct;
                catAct.CurAct = ActType.Tail;
                Focus();
                ToolTip.Show(Resources.MouseOver, this, catAct.TipPoint, 3000);
            }
        }

        private void PetCat_MouseLeave(object sender, EventArgs e)
        {
            if (catAct.CurAct == ActType.Tail)
            {
                Task.Factory.StartNew(async () =>
                {
                    await Task.Delay(1000);
                    catAct.CurAct = catAct.LastAct;
                    ToolTip.Hide(this);
                });
            }
        }

        private void PetCat_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
                catAct.CurAct = ActType.Tail;
        }

        private void PetCat_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
                catAct.CurAct = ActType.Hit;
        }

        #endregion


        #region 右键菜单

        private void 对话ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            talkFrm.Show();
        }

        private void 喂食ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.catAct.CurAct = ActType.Eat;
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            this.catAct.CurAct = ActType.Tail;
            this.ContextMenuStrip.Enabled = false;
            this.Focus();
            this.ToolTip.Show(Resources.Bye, this, catAct.TipPoint, 2000);

            Task.Factory.StartNew(async () =>
            {
                await Task.Delay(2000);
                Application.Exit();
            });
        }

        #endregion


        #region 窗体可拖动

        private Point mousePosition;

        private void PetCat_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.mousePosition.X = e.X; this.mousePosition.Y = e.Y;
            }
        }

        private void PetCat_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Top = Control.MousePosition.Y - mousePosition.Y;
                this.Left = Control.MousePosition.X - mousePosition.X;
            }
        }

        #endregion


        #region 窗体显示透明图像

        public void SetBitmap(Bitmap bitmap, byte opacity = 255)
        {
            if (bitmap.PixelFormat != PixelFormat.Format32bppArgb)
                throw new ApplicationException("The bitmap must be 32ppp with alpha-channel.");

            IntPtr screenDc = CommonAPI.GetDC(IntPtr.Zero);
            IntPtr memDc = CommonAPI.CreateCompatibleDC(screenDc);
            IntPtr hBitmap = IntPtr.Zero;
            IntPtr oldBitmap = IntPtr.Zero;

            try
            {
                hBitmap = bitmap.GetHbitmap(Color.FromArgb(0));
                oldBitmap = CommonAPI.SelectObject(memDc, hBitmap);

                CommonAPI.Size size = new CommonAPI.Size(bitmap.Width, bitmap.Height);
                CommonAPI.Point pointSource = new CommonAPI.Point(0, 0);
                CommonAPI.Point topPos = new CommonAPI.Point(Left, Top);
                CommonAPI.BLENDFUNCTION blend = new CommonAPI.BLENDFUNCTION();

                blend.BlendOp = CommonAPI.AC_SRC_OVER;
                blend.BlendFlags = 0;
                blend.SourceConstantAlpha = opacity;
                blend.AlphaFormat = CommonAPI.AC_SRC_ALPHA;

                CommonAPI.UpdateLayeredWindow(this.Handle, screenDc, ref topPos, ref size, memDc, ref pointSource, 0, ref blend, CommonAPI.ULW_ALPHA);
            }
            finally
            {
                CommonAPI.ReleaseDC(IntPtr.Zero, screenDc);
                if (hBitmap != IntPtr.Zero)
                {
                    CommonAPI.SelectObject(memDc, oldBitmap);
                    CommonAPI.DeleteObject(hBitmap);
                }
                CommonAPI.DeleteDC(memDc);
            }
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle = 0x80000;
                return cp;
            }
        }

        #endregion
    }
}
