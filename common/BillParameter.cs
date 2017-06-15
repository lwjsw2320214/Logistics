using Entity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;

namespace common
{
    public class BillParameter
    {


        public String getParameter(RecPreInputEntity re)
        {
            LogisticsEncryption le = new LogisticsEncryption();
            var billServiceType = ConfigurationManager.AppSettings["billServiceType"].ToString();
            var partnerId = ConfigurationManager.AppSettings["partnerId"].ToString();
            var partnerKey = ConfigurationManager.AppSettings["partnerKey"].ToString();
            var xml = GetLogisticsInterface(re);
            var digest = le.Md5Basece64(xml + partnerKey);
            var p = "bizData=" + xml + "&digest=" + digest + "&msgId=" + Guid.NewGuid().ToString("N") + "&parternID=" + partnerId + "&serviceType=" + billServiceType;
            return p;

        }


        /// <summary>
        /// 构建XML文件
        /// </summary>
        /// <param name="re"></param>
        /// <returns></returns>

        public String GetLogisticsInterface(RecPreInputEntity re)
        { 
            var j_company = ConfigurationManager.AppSettings["j_company"].ToString();
            var j_post_code = ConfigurationManager.AppSettings["j_post_code"].ToString();
            var j_tel = ConfigurationManager.AppSettings["j_tel"].ToString();
            var j_mobile = ConfigurationManager.AppSettings["j_mobile"].ToString();
            var j_province = ConfigurationManager.AppSettings["j_province"].ToString();
            var j_city = ConfigurationManager.AppSettings["j_city"].ToString();
            var j_county = ConfigurationManager.AppSettings["j_county"].ToString();
            var j_address = ConfigurationManager.AppSettings["j_address"].ToString();

            StringBuilder sb = new StringBuilder();
            sb.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            sb.Append("<PrintRequest xmlns:ems=\"http://express.800best.com\">");
            sb.Append("<deliveryConfirm>true</deliveryConfirm>");
            sb.Append("<EDIPrintDetailList>");
            //正式内容
            //发件人名称
            sb.Append("<sendMan><![CDATA[" + j_company + "]]></sendMan>");
            //电话
            sb.Append("<sendManPhone><![CDATA[" + j_tel + "]]></sendManPhone>");
            //地址
            sb.Append("<sendManAddress><![CDATA[" + j_address + "]]></sendManAddress>");
            //邮编
            sb.Append("<sendPostcode><![CDATA[" + j_post_code + "]]></sendPostcode>");
           //省
            sb.Append("<sendProvince><![CDATA[" + j_province + "]]></sendProvince>");
            //市
            sb.Append("<sendCity><![CDATA["+j_city+"]]></sendCity>");
            //区
            sb.Append("<sendCounty><![CDATA[" + j_county + "]]></sendCounty>");
            //收件人信息
            sb.Append("<receiveMan><![CDATA[" + re.creceiver + "]]></receiveMan>");
            //收件人电话
            sb.Append("<receiveManPhone><![CDATA[" + re.cphone + "]]></receiveManPhone>");
            //收件人地址
            sb.Append("<receiveManAddress><![CDATA[" + re.caddr + "]]></receiveManAddress>");
            //邮编
            sb.Append("<receivePostcode><![CDATA[" + re.cpostcode + "]]></receivePostcode>");
            //省
            sb.Append("<receiveProvince><![CDATA[" + re.cprovince + "]]></receiveProvince>");
            //市
            sb.Append("<receiveCity><![CDATA[" + re.ccity + "]]></receiveCity>");
            //区
            sb.Append("<receiveCounty><![CDATA[" + re.ccountry + "]]></receiveCounty>");
            //订单id
            sb.Append("<txLogisticID><![CDATA[" + re.cnum + "]]></txLogisticID>");
            //商品名称
            sb.Append("<itemName><![CDATA[" + re.cgoods + "]]></itemName>");
            //重量
            sb.Append("<itemWeight><![CDATA[" + re.fweight + "]]></itemWeight>");
            //数量
            sb.Append("<itemCount><![CDATA[" + re.iitem + "]]></itemCount>");
            //备注
            sb.Append("<remark><![CDATA[remark]]></remark> ");
            sb.Append("</EDIPrintDetailList>");
            sb.Append("</PrintRequest>");
            return sb.ToString();

        }

        private string urlEnCode(string parameter)
        { 
            var count = HttpUtility.UrlEncode(parameter, Encoding.GetEncoding("UTF-8"));
            return count;
        }

    }
}
