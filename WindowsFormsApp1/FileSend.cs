using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
namespace WindowsFormsApp1
{
    class FileSend
    {
        private TcpClient client;
        private NetworkStream stream;
        private string[] param;
        private Form1 fm;
        public FileSend(Form1 fm, params string[] param)
        {
            this.param = param;
            this.fm = fm;

        }
        public void Send()
        {

            try
            {
                //连接接收端
                this.client = new TcpClient(param[0], int.Parse(param[1]));
                string msg = param[2] + "|" + param[4];
                byte[] m = Encoding.UTF8.GetBytes(msg);

                //Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                //IPEndPoint ipep = new IPEndPoint(IPAddress.Parse(param[0]), int.Parse(param[1]));
                //IPEndPoint ipep = new IPEndPoint(IPAddress.Parse(param[0]), 8008);
                //socket.Connect(ipep);

                while (true)
                {
                    this.stream = this.client.GetStream();
                    this.stream.Write(m, 0, m.Length);
                    this.stream.Flush();
                    byte[] data = new byte[1024];
                    int len = this.stream.Read(data, 0, data.Length);
                    msg = Encoding.UTF8.GetString(data, 0, len);
                    //对方要接收我发送的文件
                    if (msg.Equals("1"))
                    {
                        fm.SetState("正在发送：");
                        FileStream os = new FileStream(param[3], FileMode.OpenOrCreate);

                        data = new byte[1024];
                        //记录当前发送进度
                        long currentprogress = 0;
                        len = 0;
                        while ((len = os.Read(data, 0, data.Length)) > 0)
                        {
                            currentprogress += len;
                            //更新进度条
                            fm.UpDateProgress((int)(currentprogress * 100 / long.Parse(param[4])));
                            this.stream.Write(data, 0, len);

                        }
                        os.Flush();
                        this.stream.Flush();
                        os.Close();
                        this.stream.Close();
                        fm.Tip("发送成功!");
                        fm.Exit();
                        //socket.Close();

                    }
                }
            }
            catch (Exception e)
            {
                fm.Tip(e.Message);

            }

        
    }
}
}
