using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.IO;

namespace common
{
    public class YTONetWorkConnect
    {

        /// <summary>
        /// post 提交数据
        /// </summary>
        /// <returns></returns>
        public string PostConnect(string parameter) {

            var url = "http://service.yto56.net.cn/CommonOrderModeBPlusServlet.action";
            byte[] postData = Encoding.UTF8.GetBytes(parameter);
            WebClient webClient = new WebClient();
            webClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
            byte[] responseData = webClient.UploadData(url, "POST", postData);
            var result = Encoding.UTF8.GetString(responseData); ; 
            return result;
        }
    }
}
