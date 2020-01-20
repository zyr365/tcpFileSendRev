using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class IpUtil
    {
        /// <summary>
        /// 获取本地ip地址
        /// </summary>
        /// <returns></returns>
        public static string GetLocalIp()
        {
            string hostname = Dns.GetHostName();
            IPHostEntry localhost = Dns.GetHostByName(hostname);
            IPAddress localaddr = localhost.AddressList[0];
            return localaddr.ToString();
        }
        /// <summary>
        /// 产生随机端口
        /// </summary>
        /// <returns></returns>
        public static int GetRandomPort()
        {

            return new Random().Next(1000) + 5000;
        }
    }
}
