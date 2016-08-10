using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entity;
using DBUtility;

namespace Dao
{
    public class ShopAreaDao
    {

        private HelpSQL helpsql = new HelpSQL("sqlconn");

        public List<ShopArea> GetAllArea() { 
            List<ShopArea> list = new List<ShopArea>(); 
            var sql = "select [id] ,[province] ,[city] ,[area]  ,[destination] from shop_area"; 
            using (var db = helpsql.ExecuteReader(sql, null)) { 
                if (db.HasRows) {
                    while (db.Read()) {
                        ShopArea shopArea = new ShopArea();
                        shopArea.id = db.GetInt32(0);
                        shopArea.province = db.GetString(1);
                        shopArea.city = db.GetString(2);
                        shopArea.area =db.IsDBNull(3)?null: db.GetString(3);
                        shopArea.destination = db.GetString(4);
                        list.Add(shopArea);
                    }
                }
            }
            return list;
        }

    }
}
