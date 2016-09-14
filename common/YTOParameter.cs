using Entity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;

namespace common
{
    public class YTOParameter
    {

        public string PostParameter(RecPreInputEntity re)
        {
            var clientID = ConfigurationManager.AppSettings["clientID"].ToString();
            var p = "logistics_interface=" + urlEnCode(GetLogisticsInterface(re));
            p += "&data_digest=" + urlEnCode(getDataDigest(re));
            p += "&clientId=" + clientID;
            p += "&type=offline";
            return p;
        }

        /// <summary>
        /// 获取数字签名
        /// </summary>
        /// <returns></returns>
        private string getDataDigest(RecPreInputEntity re)
        {
            var k = ConfigurationManager.AppSettings["k"].ToString();

            LogisticsEncryption le = new LogisticsEncryption();

            var encryptionString = GetLogisticsInterface(re) + k;

            return le.Md5Basece64(encryptionString);
        }

        private string GetLogisticsInterface(RecPreInputEntity re)
        {

            Random random = new Random();
            int n = random.Next(1, 100);
            var clientID = ConfigurationManager.AppSettings["clientID"].ToString();
            var orderType = ConfigurationManager.AppSettings["orderType"].ToString();
            var serviceType = ConfigurationManager.AppSettings["serviceType"].ToString();
            var j_company = ConfigurationManager.AppSettings["j_company"].ToString();
            var j_post_code = ConfigurationManager.AppSettings["j_post_code"].ToString();
            var j_tel = ConfigurationManager.AppSettings["j_tel"].ToString();
            var j_mobile = ConfigurationManager.AppSettings["j_mobile"].ToString();
            var j_province = ConfigurationManager.AppSettings["j_province"].ToString();
            var j_city = ConfigurationManager.AppSettings["j_city"].ToString();
            var j_county = ConfigurationManager.AppSettings["j_county"].ToString();
            var j_address = ConfigurationManager.AppSettings["j_address"].ToString();
            StringBuilder sb = new StringBuilder();
            sb.Append("<RequestOrder>");
            sb.Append("<clientID>" + clientID + "</clientID>");
            sb.Append("<logisticProviderID>YTO</logisticProviderID>");
            sb.Append("<customerId>" + clientID + "</customerId>");
            sb.Append("<txLogisticID>" + re.cnum + re.iid + n + "</txLogisticID>");
            sb.Append("<orderType>" + orderType + "</orderType>");
            sb.Append("<serviceType>" + serviceType + "</serviceType>");
            sb.Append("<sender>");
            sb.Append("<name>" + j_company + "</name>");
            sb.Append("<postCode>" + j_post_code + "</postCode>");
            sb.Append("<phone>" + j_tel + "</phone>");
            sb.Append("<mobile>" + j_mobile + "</mobile>");
            sb.Append("<prov>" + j_province + "</prov>");
            sb.Append("<city>" + j_city + "," + j_county + "</city>");
            sb.Append("<address>" + j_address + "</address>");
            sb.Append("</sender>");

            sb.Append("<receiver>");
            sb.Append("<name>"+re.creceiver+"</name>");
            sb.Append("<postCode></postCode>");
            sb.Append("<phone></phone>");
            sb.Append("<mobile>" + re.cphone + "</mobile>");
            sb.Append("<prov>"+re.cprovince+"</prov>");
            sb.Append("<city>"+re.ccity+"</city>");
            sb.Append("<address>+"+re.caddr+"+</address>");
            sb.Append("</receiver>");
            sb.Append("<items>");
            sb.Append("<item>");
            sb.Append("<itemName>"+re.cgoods+"</itemName>");
            sb.Append("<number>"+re.iitem+"</number>");
            sb.Append("</item>");
            sb.Append("</items>"); 
            sb.Append("</RequestOrder>");
            return sb.ToString();
        }

        private string urlEnCode(string parameter) {

            var count = HttpUtility.UrlEncode(parameter, Encoding.GetEncoding("UTF-8"));
            return count;
        }

    }
}
