using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Service;
using Entity;
using System.Xml;
using common;
using System.Configuration;
using System.Text;


namespace Logistics
{
    public partial class WebApi : System.Web.UI.Page
    {
        private RecPreInputService recPreInputService = new RecPreInputService();
        private CourierNumberService courierNumberService = new CourierNumberService();
        protected CookiesUtil cookiesUtil = new CookiesUtil();
        protected int s = 0;
        private object _obj = new object();
        protected void Page_Load(object sender, EventArgs e)
        { 
            if (!IsPostBack)
            {

                var keytoken = Request.Form["token"];
                if (keytoken == "sdfsdftsfcvsrser")
                {
                    s = -2;
                    return;

                }

                int icid = 0;
                int.TryParse(Request.Form["icid"], out icid);
                if (icid == 0)
                {
                    s = -2;
                    return;
                
                }
                var userName = Request.Form["userName"];

                var iid = Request.Form["iid"];
                if (string.IsNullOrEmpty(iid))
                {
                    s = -2;
                    return;
                }
                var extype = Server.UrlDecode(Request.Form["extype"].ToString()); ;//gb2312_utf8(Request.QueryString["extype"]);
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

                        SFXMLMosaic sfxmlMosaic = new SFXMLMosaic();
                        foreach (var r in re)
                        {
                            if (r.nitemtype == 0)
                            {

                                continue;
                            }

                            if ("顺丰快递".Equals(extype))
                            {
                                var xmlText = sfxmlMosaic.getXML(r);
                                var stext = sfxmlMosaic.getXMLCheck(sfxmlMosaic.getXML(r));

                                SFService.ExpressServiceClient client = new SFService.ExpressServiceClient();

                                var t = client.sfexpressService(xmlText, stext);
                                XmlDocument xmlDocument = new XmlDocument();
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
                                    var count = recPreInputService.UpdateOrder(orderid, mailno, destcode, extype,r.iid.Value);
                                    if (count > 0)
                                    {
                                        i++;
                                    }
                                }
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
                                    var count = recPreInputService.UpdateOrder(orderid, mailno, destcode, extype,r.iid.Value);
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
                catch (Exception f)
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
    }
}