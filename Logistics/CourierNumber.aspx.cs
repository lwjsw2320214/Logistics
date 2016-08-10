using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entity;
using Service;
using common;

namespace Logistics
{
    public partial class CourierNumber : System.Web.UI.Page
    {

        private CourierNumberService courierNumberService = new CourierNumberService();
        private EmsKindService emsKindService = new EmsKindService();
        protected List<CourierNumberEntity> list;
        protected List<EmsKindEntity> emsList;
        protected int pageCount;
        protected int pageSize = 20;
        protected int rowCount = 0;
        protected int page = 0;
        protected string par = "";
        protected CookiesUtil cookiesUtil = new CookiesUtil();
        protected int states = 0;
        protected string expressType = "";
        protected List<CourierNumberCountEntity> countList;
        protected string updateUser = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {

                var icid = cookiesUtil.getCookie("GInfo_999_Vali");
                if (icid != 10) {

                    Response.Write("该用户没有查看此页的权限！");
                    Response.End();
                }

                int.TryParse(Request.QueryString["page"],out page);
                if(page==0){
                    page=1;
                }

                expressType = Request.QueryString["expressType"];
                if (!string.IsNullOrEmpty(expressType))
                {
                    par += "&expressType=" + expressType;
                }

                int.TryParse(Request.QueryString["state"], out states);
                if (!string.IsNullOrEmpty(Request.QueryString["state"])) {

                    par += "&state=" + states;
                }

                updateUser = Request.QueryString["userid"];

                if (!string.IsNullOrEmpty(updateUser)) {
                    par += "&userid=" + updateUser; 
                }

                emsList = emsKindService.getAll();

                list = courierNumberService.getPageRow(page, pageSize, states, expressType, updateUser);

                pageCount = courierNumberService.getPageCount(states, expressType, pageSize, updateUser);

                if (string.IsNullOrEmpty(Request.QueryString["state"]))
                {

                   states=3;
                }
                rowCount = courierNumberService.getAllRowCount();

                countList = courierNumberService.getAllCount();
            }
            
        }
    }
}