using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using Entity;

namespace common
{
    public class SFXMLMosaic
    {

        /// <summary>
        /// 普通xml
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        public string getXML(RecPreInputEntity r)
        {

            //判断重量是否大于3并且小于3.6如果是就把值赋值为3
            if (r.fweight.Value > 3 && r.fweight.Value < decimal.Parse("3.6"))
            {
                r.fweight = 3;
            }

            Random random = new Random();
            int n = random.Next(1, 100);
            var j_company = ConfigurationManager.AppSettings["j_company"].ToString();
            var j_contact = ConfigurationManager.AppSettings["j_contact"].ToString();
            var j_tel = ConfigurationManager.AppSettings["j_tel"].ToString();
            var j_mobile = ConfigurationManager.AppSettings["j_mobile"].ToString();
            var j_province = ConfigurationManager.AppSettings["j_province"].ToString();
            var j_city = ConfigurationManager.AppSettings["j_city"].ToString();
            var j_county = ConfigurationManager.AppSettings["j_county"].ToString();
            var j_address = ConfigurationManager.AppSettings["j_address"].ToString();
            var j_post_code = ConfigurationManager.AppSettings["j_post_code"].ToString();
            var express_type = ConfigurationManager.AppSettings["express_type"].ToString();
            var pay_method = ConfigurationManager.AppSettings["pay_method"].ToString();
            var custid = ConfigurationManager.AppSettings["custid"].ToString();
            var parcel_quantity = ConfigurationManager.AppSettings["parcel_quantity"].ToString();
            var sendstarttime = ConfigurationManager.AppSettings["sendstarttime"].ToString();
            var remark = ConfigurationManager.AppSettings["remark"].ToString();

            StringBuilder sb = new StringBuilder();            sb.Append("<?xml version='1.0' encoding='UTF-8'?>");
            sb.Append("<Request service=\"OrderService\" lang=\"zh-CN\">");
            sb.Append("<Head>0286022070</Head>");
            sb.Append("<Body>");
            //订单号
            sb.Append("<Order orderid='" + r.cnum +r.iid+n+ "' ");
            //sb.Append("<Order orderid='BB123456555525' ");
            sb.Append("j_company='" + j_company + "' ");
            sb.Append("j_contact='" + j_contact + "' ");
            sb.Append("j_tel='" + j_tel + "' ");
            sb.Append("j_mobile='" + j_mobile + "' ");
            sb.Append("j_province='" + j_province + "' ");
            sb.Append("j_city='" + j_city + "' ");
            sb.Append("j_county='" + j_county + "' ");
            sb.Append("j_address='" + j_address + "' ");
            sb.Append("j_post_code='" + j_post_code + "' ");
            sb.Append("cargo_total_weight='" + r.fweight + "' ");
            sb.Append("d_contact='" + r.creceiver + "' ");
            sb.Append("d_tel='" + r.cphone + "' ");
            sb.Append("d_mobile='" + r.cphone + "' ");
            sb.Append("d_province='" + r.cprovince + "' ");
            sb.Append("d_city='" + r.ccity + "' ");
            sb.Append("d_county='' ");
            sb.Append("d_address='" + r.caddr + "' ");
            sb.Append("express_type='" + express_type + "' ");
            sb.Append("pay_method='" + pay_method + "' ");
            sb.Append("custid='" + custid + "' ");
            sb.Append("parcel_quantity='" + parcel_quantity + "' ");
            sb.Append("sendstarttime='" + sendstarttime + "' ");
            sb.Append("remark='" + remark + "'>");
            sb.Append("</Order></Body></Request>");
            var d = sb.ToString();
            return sb.ToString();
        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        public string getXMLCheck(string d)
        {
            LogisticsEncryption le = new LogisticsEncryption();
            d += "Vfk34nZpjzx9fYTPKFuklV6AiaId1nIg";
            var sc =le.Md5Basece64(d);
            return sc;
        }



    }
}
