using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Service;
using common;


namespace Logistics
{
    public partial class delete : System.Web.UI.Page
    {

        /// <summary>
        /// 
        /// </summary>
        private CourierNumberService courierNumberService = new CourierNumberService();
        protected CookiesUtil cookiesUtil = new CookiesUtil(); 
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {
               var  icid = cookiesUtil.getCookie("GInfo_999_Vali");

               var state = 0;
               if (icid == 0) {
                   state = 0;
                   Response.Write(state);
                   Response.End();
               } 
                var id = Request.Form["id"];
                var orderId=0;
                int.TryParse(id,out orderId);

                if (orderId > 0)
                {
                  var count=  courierNumberService.delete(orderId);
                  if (count > 0) {

                      state = 1;
                  }
                }
                Response.Write(state);
                Response.End();
            }
        }
    }
}