﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Service;
using Entity;
using System.Text;
using common;

namespace Logistics
{
    public partial class Logistic : System.Web.UI.Page
    {
        private MemberService memberService = new MemberService();
        private RecPreInputService recPreInputService = new RecPreInputService();
        private EmsKindService emsKindService = new EmsKindService();
        protected int icid = 0;
        protected List<RecPreInputEntity> list = new List<RecPreInputEntity>();
        protected CookiesUtil cookiesUtil = new CookiesUtil();
        protected List<EmsKindEntity> emsList;
        protected int page = 1;
        protected decimal total;
        protected int pageCount = 0;
        protected int rowCount = 0;
        protected string par = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            Encoding gb2312 = Encoding.GetEncoding("gb2312");
            Response.ContentEncoding = gb2312;
            if (!IsPostBack) {
                emsList = emsKindService.getAll();
                icid = cookiesUtil.getCookie("GInfo_999_Vali"); 

                int.TryParse(Request.QueryString["page"], out page); 

                var cemskind = Request.QueryString["cemskind"];

                if (!string.IsNullOrEmpty(cemskind))
                {
                    par += "&cemskind=" + cemskind;
                }
                var cnum = Request.QueryString ["cnum"];
                if (!string.IsNullOrEmpty(cnum)) {
                    par += "&cnum=" + cnum;
                }
                var cdes = Request.QueryString["cdes"];
                if (!string.IsNullOrEmpty(cdes))
                {
                    par += "&cdes=" + cdes;
                }
                var cmark = Request.QueryString["cmark"];
                if (!string.IsNullOrEmpty(cmark))
                {
                    par += "&cmark=" + cmark;
                }
                var bdate = Request.QueryString["bdate"];
                if (!string.IsNullOrEmpty(bdate))
                {
                    par += "&bdate=" + bdate;
                }
                var edate = Request.QueryString["edate"];
                if (!string.IsNullOrEmpty(edate))
                {
                    par += "&edate=" + edate;
                }
                
                list = recPreInputService.GetPage(page, 100, icid, 0, cemskind, cnum, cdes, cmark, bdate, edate);

                total = recPreInputService.getTotal(icid, 0, cemskind, cnum, cdes, cmark, bdate, edate);

                pageCount = recPreInputService.getPageCount(icid, 0, 100, cemskind, cnum, cdes, cmark, bdate, edate);

                rowCount = recPreInputService.getRowCount(icid, 0, cemskind, cnum, cdes, cmark, bdate, edate);
            }
        }
        
    }
}