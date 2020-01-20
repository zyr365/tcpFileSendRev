using System;
using System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// 文件名
        /// </summary>
        private string fileName;
        /// <summary>
        /// 文件路径
        /// </summary>
        private string filePath;
        /// <summary>
        /// 文件大小
        /// </summary>
        private long fileSize;
        public Form1()
        {
            InitializeComponent();
            Thread.CurrentThread.IsBackground = true;
            textBox2.Text = IpUtil.GetLocalIp();
            label1.Text = "您的ip:" + IpUtil.GetLocalIp() + " 您的端口:" + IpUtil.GetRandomPort();
            var s = new FileRecive(this);
            new Thread(s.run).Start();
        }
        /// <summary>
        /// 信息提示框
        /// </summary>
        /// <param name="msg"></param>
        public void Tip(string msg)
        {
            MessageBox.Show(msg, "温馨提示");
        }
        /// <summary>
        /// 发送文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            string ip = textBox2.Text;
            string port = textBox3.Text;
            if (fileName.Length == 0)
            {
                Tip("请选择文件");
                return;
            }
            if (ip.Length == 0 || port.ToString().Length == 0)
            {
                Tip("端口和ip地址是必须的!");
                return;
            }

            var c = new FileSend(this, new string[] { ip, port, fileName, filePath, fileSize.ToString() });
            new Thread(c.Send).Start();
        }
        /// <summary>
        /// 选择文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dig = new OpenFileDialog();
            dig.ShowDialog();
            //获取文件名
            this.fileName = dig.SafeFileName;
            //获取文件路径
            this.filePath = dig.FileName;
            FileInfo f = new FileInfo(this.filePath);
            //获取文件大小
            this.fileSize = f.Length;
            textBox1.Text = filePath;
        }
        /// <summary>
        /// 更新进度条
        /// </summary>
        /// <param name="value"></param>
        public void UpDateProgress(int value)
        {

            this.progressBar1.Value = value;
            this.label2.Text = value + "%";
            System.Windows.Forms.Application.DoEvents();
        }
        /// <summary>
        /// 修改状态
        /// </summary>
        /// <param name="state"></param>
        public void SetState(string state)
        {
            label3.Text = state;
        }
        /// <summary>
        /// 退出程序
        /// </summary>
        public void Exit()
        {

            Application.Exit();
        }
    }
}