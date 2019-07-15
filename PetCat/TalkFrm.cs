using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PetCat
{
    public partial class TalkFrm : Form
    {
        private MainFrm CatFrm;
        private HttpClient Client;
        
        public TalkFrm(MainFrm catFrm)
        {
            InitializeComponent();
            CatFrm = catFrm;
            Client = new HttpClient();
        }

        private void TalkFrm_Load(object sender, EventArgs e)
        {
            Task.Factory.StartNew(async () =>
            {
                var tip = "请在此输入您想说的话, 发送给小咪~";
                foreach(var item in tip)
                {
                    Invoke(new Action(() => txtSend.Text += item));
                    await Task.Delay(100);
                }
                await Task.Delay(1000);
                foreach (var item in tip)
                {
                    Invoke(new Action(() => txtSend.Text = txtSend.Text.Remove(txtSend.Text.Length - 1, 1)));
                    await Task.Delay(20);
                }
                tip = "小咪你好";
                foreach (var item in tip)
                {
                    Invoke(new Action(() => txtSend.Text += item));
                    await Task.Delay(100);
                }
                await Task.Delay(1000);
                Invoke(new Action(() => btnSend_Click(sender, null)));
            });
        }

        private void TalkFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSend.Text.Trim()))
            {
                txtSend.Clear();
                return;
            }

            var question = txtSend.Text;
            txtTalk.AppendText($"【我】（{DateTime.Now.ToString("HH:mm:ss")}）：\r\n    {txtSend.Text}\r\n\r\n");
            txtSend.Clear();

            Task.Factory.StartNew(async () =>
            {
                var url = $"http://i.itpk.cn/api.php?question={question}&limit=8&api_key=0810cfdbff6a22ad8d56f82be89b29c1&api_secret=c06syegrfvxj";
                var answer = await Client.GetStringAsync(url);
                Invoke(new Action(async () =>
                {
                    txtTalk.AppendText($"【小咪】（{DateTime.Now.ToString("HH:mm:ss")}）：\r\n    {answer}\r\n\r\n");
                    CatFrm.Focus();

                    if (answer.Length <= 20)
                        await CatFrm.ShowSentence(answer);
                }));
                await Task.Delay(2000);
                Invoke(new Action(() =>
                {
                    Focus();
                    txtSend.Focus();
                }));
            });
        }

        private void txtTalk_TextChanged(object sender, EventArgs e)
        {
            txtTalk.SelectionStart = txtTalk.Text.Length;
            txtTalk.ScrollToCaret();
        }

        private void txtSend_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSend_Click(sender, null);
                e.Handled = true;
            }
        }

        protected override void WndProc(ref Message m)
        {
            const int WM_SYSCOMMAND = 0x0112;
            const int SC_CLOSE = 0xF060;

            //捕捉关闭窗体消息
            if (m.Msg == WM_SYSCOMMAND && (int)m.WParam == SC_CLOSE)
            {
                Hide();
                return;
            }
            base.WndProc(ref m);
        }
    }
}
