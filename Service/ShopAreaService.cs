using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entity;
using Dao;
using System.Web.Caching;
using System.Web;

namespace Service
{
    public class ShopAreaService
    {
        private ShopAreaDao shopAreaDao = new ShopAreaDao();
        Cache cache = HttpRuntime.Cache;
        public List<ShopArea> GetAllArea() { 
           
             List<ShopArea> list=new List<ShopArea>();
             list = (List<ShopArea>)cache["area"];
             if (list == null)
             {
                list= shopAreaDao.GetAllArea();
                cache.Insert("area", list,null,System.DateTime.MaxValue,TimeSpan.FromDays(30));
             } 
            return list;
        }

    }
}
