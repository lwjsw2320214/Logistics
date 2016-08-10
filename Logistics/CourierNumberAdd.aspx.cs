using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entity;
using common;


namespace Logistics
{
    public partial class CourierNumberAdd : System.Web.UI.Page
    {
        private CourierNumberService courierNumberService = new CourierNumberService();

        private EmsKindService emsKindService = new EmsKindService();
        protected List<EmsKindEntity> emsList;
        protected CookiesUtil cookiesUtil = new CookiesUtil();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {
                var icid = cookiesUtil.getCookie("GInfo_999_Vali");
                if (icid != 10)
                { 
                    Response.Write("该用户没有查看此页的权限！");
                    Response.End();
                }
                emsList = emsKindService.getAll();
            }

            if (Request.Form.Count > 0)
            { 
                var cemskind = Request.Form["cemskind"]; 
                var numberlist = Request.Form["numberlist"];

                if (string.IsNullOrEmpty(cemskind) || string.IsNullOrEmpty(numberlist))
                {

                    Response.Write("<script>alert('输入数据有误请重新填写');history.back(); </script>");
                    Response.End();
                }

                var numberArray = numberlist.Split(',');
                List<string> list = new List<string>();
                //遍历数据
                foreach (var n in numberArray) {
                    list.Add(n);
                }
              var count= courierNumberService.addAll(list, cemskind);

              Response.Write("<script>alert('添加成功" + count + "条');location.href='CourierNumber.aspx'; </script>");
              Response.End();

            }
        }
    }
}