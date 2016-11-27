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
    public partial class ClienPassWord : System.Web.UI.Page
    {

        protected string username;
        protected int pageCount;
        protected int pageSize = 20;
        protected int rowCount = 0;
        protected int page = 0;
        protected string par = "";
        protected CookiesUtil cookiesUtil = new CookiesUtil();
        protected List<ClientUser> list = new List<ClientUser>();

        private MemberService memberService = new MemberService();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {

                var icid = cookiesUtil.getCookie("GInfo_999_Vali");
                if (icid != 10)
                {

                    Response.Write("该用户没有查看此页的权限！");
                    Response.End();
                }

                int.TryParse(Request.QueryString["page"], out page);
                if (page == 0)
                {
                    page = 1;
                }

                username = Request.QueryString["username"];  
               list= memberService.getPage(username, page, pageSize);
               pageCount = memberService.getPageCount(username, pageSize);
               rowCount = memberService.RowCount;
            }
        }
    }
}