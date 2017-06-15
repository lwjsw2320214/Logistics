using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace common
{
    public class BillNetWorkConnect
    {


        /// <summary>
        /// post 提交数据
        /// </summary>
        /// <returns></returns>
        public string getConnect(string parameter)
        { 
            var url = "http://183.129.172.49/ems/api/process"; 
            WebClient webClient = new WebClient();
            Encoding enc = Encoding.GetEncoding("UTF-8");
            Byte[] pageData = webClient.DownloadData(url+"?"+parameter);
            string re = enc.GetString(pageData);
            return re;
        }

        /// <summary>
        /// post 提交数据
        /// </summary>
        /// <returns></returns>
        public string PostConnect(string parameter)
        {

            var url = "http://183.129.172.49/ems/api/process";
            byte[] postData = Encoding.UTF8.GetBytes(parameter);
            WebClient webClient = new WebClient();
            webClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
            byte[] responseData = webClient.UploadData(url, "POST", postData);
            var result = Encoding.UTF8.GetString(responseData); ;
            return result;
        }

    }
}
