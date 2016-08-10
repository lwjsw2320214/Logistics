using common;
using Entity;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Logistics
{
    public partial class ajax : System.Web.UI.Page
    {
        private RecPreInputService recPreInputService = new RecPreInputService();
        protected ExpressPrint ep = new ExpressPrint();
        protected CookiesUtil cookiesUtil = new CookiesUtil();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {

              var icid= cookiesUtil.getCookie("GInfo_999_Vali");
                var expcode = Request.QueryString["expcode"];
                if (!String.IsNullOrEmpty(expcode))
                {
                    ep = recPreInputService.getRowId(expcode,icid,0);
                }
                
            }
        }
    }
}