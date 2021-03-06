﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Service;
using Entity;
using System.Xml;
using common;
using System.Configuration;

namespace Logistics
{
    public partial class SynchronousOrder : System.Web.UI.Page
    {
        private RecPreInputService recPreInputService = new RecPreInputService();
        private CourierNumberService courierNumberService = new CourierNumberService();
        protected CookiesUtil cookiesUtil = new CookiesUtil();
        protected int s = 0;
        private object _obj = new object(); 

        protected void Page_Load(object sender, EventArgs e)
        {
            Encoding gb2312 = Encoding.GetEncoding("gb2312");
            Response.ContentEncoding = gb2312;
            Response.Charset = "gb2312";
            if (!IsPostBack)
            {
                int icid = cookiesUtil.getCookie("GInfo_999_Vali");
                string userName = cookiesUtil.getCookieUserName("GInfo_999_Vali");
                if (icid == 0)
                {
                    s = -2;
                    return;
                }
                var iid = Request.QueryString["iid"];
                if (string.IsNullOrEmpty(iid))
                {
                    s = -2;
                    return;
                }
                var extype = Request.QueryString["extype"];//gb2312_utf8(Request.QueryString["extype"]);
                Response.Write(extype);
                var iidArray = iid.Split(',');
                List<RecPreInputEntity> re = new List<RecPreInputEntity>();
                foreach (var id in iidArray)
                {
                    re.Add(recPreInputService.findById(int.Parse(id), icid, 0));
                }

                var i = 0;

                try
                {
                    lock (_obj)
                    {  
                         
                        foreach (var r in re)
                        {
                            if (r.nitemtype == 0)
                            {

                                continue;
                            }
                            XmlDocument xmlDocument = new XmlDocument();
                            if ("顺丰快递".Equals(extype))
                            {
                                SFXMLMosaic sfxmlMosaic = new SFXMLMosaic();
                                var xmlText = sfxmlMosaic.getXML(r);
                                var stext = sfxmlMosaic.getXMLCheck(sfxmlMosaic.getXML(r));
                                SFService.ExpressServiceClient client = new SFService.ExpressServiceClient();
                                var t = client.sfexpressService(xmlText, stext);
                                LogWrite.WriteLog("SF",t);
                                xmlDocument.LoadXml(t);
                                XmlNode node = xmlDocument.SelectSingleNode("/Response/Head");
                                var head = node.InnerText;
                                if (head == "OK")
                                {
                                    XmlNode orderXml = xmlDocument.SelectSingleNode("/Response/Body/OrderResponse");
                                    var nodAttribute = orderXml.Attributes;
                                    //自身快递号
                                    var orderid = nodAttribute["orderid"].Value;
                                    //顺丰快递号
                                    var mailno = nodAttribute["mailno"].Value;
                                    //区域代码
                                    var destcode = nodAttribute["destcode"].Value;
                                    //更新快递单
                                    var count = recPreInputService.UpdateOrder(orderid, mailno, destcode, extype,r.iid.Value,r.fweight.Value);
                                    if (count > 0)
                                    {
                                        i++;
                                    }
                                }
                            }
                            else if ("圆通快递".Equals(extype))
                            {
                               i= getYt(xmlDocument, r, i, extype);
                            }
                            else if ("百世快递".Equals(extype))
                            {
                                i = getBill(xmlDocument, r, i, extype);
                            }
                            else if (!string.IsNullOrEmpty(extype))
                            {
                                //循环获取快递号
                                var courierNumber = courierNumberService.getCourierNumberEntity(extype);
                                //判断是否获取到了对应的快单号
                                if (courierNumber.id > 0)
                                {
                                    //自身快递单号
                                    var orderid = r.cnum;
                                    var mailno = courierNumber.courierNumber;
                                    var destcode = r.cdes;
                                    var count = recPreInputService.UpdateOrder(orderid, mailno, destcode, extype, r.iid.Value, r.fweight.Value);
                                    var ncount = courierNumberService.update(courierNumber.id, userName);
                                    if (ncount > 0)
                                    {
                                        i++;
                                    }
                                }

                            }
                        }
                    }
                }
                catch(Exception f)
                {
                    Response.Write(f.Message);
                    Response.End();
                }
                s = i;
            }
        }

        public string gb2312_utf8(string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                //声明字符集   
                System.Text.Encoding utf8, gb2312;
                //gb2312   
                gb2312 = System.Text.Encoding.GetEncoding("gb2312");
                //utf8   
                utf8 = System.Text.Encoding.GetEncoding("utf-8");
                byte[] gb;
                gb = gb2312.GetBytes(text);
                gb = System.Text.Encoding.Convert(gb2312, utf8, gb);
                //返回转换后的字符   
                return utf8.GetString(gb);
            }
            else
            {

                return "";
            }
        }


        public int getBill(XmlDocument xmlDocument, RecPreInputEntity r, int i, string extype) {

            BillParameter billParameter = new BillParameter();
            var p = billParameter.getParameter(r);
            LogWrite.WriteLog("BS", p);
            BillNetWorkConnect connect = new BillNetWorkConnect();
            var c = connect.PostConnect(p);
            LogWrite.WriteLog("BS", c);
            xmlDocument.LoadXml(c);
            XmlNode node = xmlDocument.SelectSingleNode("/PrintResponse/result");
            var head = node.InnerText;
            if (head == "SUCCESS")
            {
                //原始订单
                XmlNode orderXml = xmlDocument.SelectSingleNode("/PrintResponse/EDIPrintDetailList/txLogisticID");
                var orderid = orderXml.InnerText;
                //圆通快递号
                var mailno = xmlDocument.SelectSingleNode("/PrintResponse/EDIPrintDetailList/mailNo").InnerText;
                //大头笔
                var destcode = xmlDocument.SelectSingleNode("/PrintResponse/EDIPrintDetailList/markDestination").InnerText;

                var b = courierNumberService.getRowCount(mailno, extype);
                if (b > 0)
                {
                    return getYt(xmlDocument, r, i, extype);
                }
                //更新快递单
                var count = recPreInputService.UpdateOrder(orderid, mailno, destcode, extype, r.iid.Value, r.fweight.Value);
                if (count > 0)
                {
                    i++;
                }
            }
            return i; 
        }


        public int getYt(XmlDocument xmlDocument, RecPreInputEntity r, int i, string extype)
        {
            
            YTOParameter yTOParameter = new YTOParameter();
            var p = yTOParameter.PostParameter(r);
            YTONetWorkConnect nc = new YTONetWorkConnect();
            var c = nc.PostConnect(p);
            LogWrite.WriteLog("YT",c);
            xmlDocument.LoadXml(c);
            XmlNode node = xmlDocument.SelectSingleNode("/Response/success");
            var head = node.InnerText;
            if (head == "true")
            {
                //原始订单
                XmlNode orderXml = xmlDocument.SelectSingleNode("/Response/txLogisticID");
                var orderid = orderXml.InnerText;
                //圆通快递号
                var mailno = xmlDocument.SelectSingleNode("/Response/mailNo").InnerText;
                //三段号
                var destcode = xmlDocument.SelectSingleNode("/Response/distributeInfo/shortAddress").InnerText;

               var b=courierNumberService.getRowCount(mailno, extype);
               if (b > 0) { 
                   return getYt(xmlDocument,r,i,extype);
                } 
                //更新快递单
                var count = recPreInputService.UpdateOrder(orderid, mailno, destcode, extype, r.iid.Value,r.fweight.Value);
                if (count > 0)
                {
                    i++;
                }
            }
            return i;

        }
    }
}