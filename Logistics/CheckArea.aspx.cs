using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Service;
using Entity;
using common;

namespace Logistics
{
    public partial class CheckArea : System.Web.UI.Page
    {

        private ShopAreaService shopAreaService = new ShopAreaService();
        private RecPreInputService recPreInputService = new RecPreInputService();
        protected CookiesUtil cookiesUtil = new CookiesUtil();
        protected int s = 0; 
         
        protected void Page_Load(object sender, EventArgs e)
        {
        if (!IsPostBack) { 
            int icid = cookiesUtil.getCookie("GInfo_999_Vali");
          List<ShopArea>  list=  shopAreaService.GetAllArea();
          if (icid == 0)
          {
              s = -2;
              return;
          }
          var iid =Request.QueryString["iid"];

          if (string.IsNullOrEmpty(iid))
          {
              s = -2;
              return;
          } 
          var iidArray = iid.Split(',');
          List<RecPreInputEntity> re = new List<RecPreInputEntity>();
          foreach (var id in iidArray)
          {
              var ert = recPreInputService.findOrder(int.Parse(id), icid, 0);
              if (ert != null) { 
                re.Add(ert);
              }
          } 
          foreach (var r in re)
          {
              var destination = "";

              //获取地址
              foreach (var p in list)
              {
                   if (r.caddr.IndexOf(p.province)>-1&&r.caddr.IndexOf(p.city)>-1) {
                       if (p.area != null && r.caddr.IndexOf(p.area) > -1)
                       {
                           destination = p.destination;
                           s += recPreInputService.updatecdes(r.iid.Value, destination); 
                       }
                       else if (p.area == null && "".Equals(destination))
                       { 
                           destination = p.destination;
                           s += recPreInputService.updatecdes(r.iid.Value, destination); 
                       }
                       
                   } 
               } 
          }

        }
      }
    }
}