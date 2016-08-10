using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBUtility;
using System.Data.SqlClient;
using  Entity;
using System.Collections;

namespace Dao
{
   public  class CourierNumberDao
    {
       private HelpSQL helpSql = new HelpSQL("sqlconn");

       /// <summary>
       /// 根据条件获取总数量
       /// </summary>
       /// <param name="state"></param>
       /// <param name="expressType"></param>
       /// <returns></returns>
       public int getCount(int state, string expressType, string updateUser)
       {

           List<SqlParameter> par = new List<SqlParameter>();
           par.Add(new SqlParameter("@state", state));
            
           var sql = "select count(id) from courier_number where 1=1 and state=@state";
            
           if (!string.IsNullOrEmpty(expressType)) {

               sql += " and expressType=@expressType";
               par.Add(new SqlParameter("@expressType",expressType));
           }
           if (!string.IsNullOrEmpty(updateUser))
           {

               sql += " and updateUser=@updateUser";
               par.Add(new SqlParameter("@updateUser", updateUser));
           } 

           return helpSql.RunSql(sql, par.ToArray()); 
       }

       /// <summary>
       /// 获取分页数据
       /// </summary>
       /// <param name="page"></param>
       /// <param name="pageSize"></param>
       /// <param name="state"></param>
       /// <param name="expressType"></param>
       /// <returns></returns>
       public List<CourierNumberEntity> getPageRow(int page, int pageSize, int state, string expressType, string updateUser)
       {

           List<CourierNumberEntity> list = new List<CourierNumberEntity>();

           List<SqlParameter> par = new List<SqlParameter>();
           par.Add(new SqlParameter("@state",state));

           StringBuilder sb = new StringBuilder();

           sb.Append("select * from(select *,row_number() over (order by id) as rowNumber from courier_number ");
           sb.Append(" where 1=1 and state=@state ");
           if (!string.IsNullOrEmpty(expressType)){ 
             sb.Append(" and expressType=@expressType ");
             par.Add(new SqlParameter("@expressType", expressType));
           }
           if (!string.IsNullOrEmpty(updateUser))
           {
               sb.Append("and updateUser=@updateUser");
               par.Add(new SqlParameter("@updateUser", updateUser));
           } 
           sb.Append(" ) as a where rowNumber between @stateNumber and @endNumber");

           par.Add(new SqlParameter("@stateNumber", (page-1)*pageSize+1));
           par.Add(new SqlParameter("@endNumber", page* pageSize));

           using (var db = helpSql.ExecuteReader(sb.ToString(), par.ToArray())) {

               if (db.HasRows) {

                   while (db.Read()) {
                       CourierNumberEntity courierNumberEntity = new CourierNumberEntity();

                       courierNumberEntity.id = db.GetInt32(0);
                       courierNumberEntity.courierNumber = db.GetString(1);
                       courierNumberEntity.expressType = db.GetString(2);
                       courierNumberEntity.state = db.GetByte(3);
                       courierNumberEntity.updateDate=null;
                       courierNumberEntity.updateDate = db.IsDBNull(4) ? courierNumberEntity.updateDate : db.GetDateTime(4);
                       courierNumberEntity.updateUser =db.IsDBNull(5)?"": db.GetString(5);
                       list.Add(courierNumberEntity);
                   }
               }

           }

           return list;
       }

       public int addAll(List<string> list, string cemskind)
       {
           Hashtable hb = new Hashtable();

           var i=0;

           foreach (var s in list)
           {
               if (getRowCount(s, cemskind) <= 0) { 

               var sql = "insert into courier_number (courierNumber,expressType,state) values (@courierNumber" + i + ",@expressType" + i + ",@state" + i + ") ";

               byte c = 0;

               SqlParameter[] par = new SqlParameter[] {
                 new SqlParameter("@courierNumber"+i,s),
                 new SqlParameter("@expressType"+i,cemskind),
                 new SqlParameter("@state"+i,c)
                };
               hb.Add(sql, par);
               i++;
               }
           }
         return  helpSql.ExecuteSqls(hb);
       }

       public int getRowCount(string number, string cemskind) {
           var sql = "select count(id)  from courier_number where courierNumber=@courierNumber and expressType=@expressType";
           SqlParameter[] par = new SqlParameter[] { 
            new SqlParameter("@courierNumber",number),
            new SqlParameter("@expressType",cemskind)
           };
           return helpSql.RunSql(sql, par);
       }

       public int delete(int id) {

           var sql = "delete from courier_number where id=@id";

           SqlParameter[] par = new SqlParameter[] { 
           new SqlParameter("@id",id)
           };

           return helpSql.ExecuteSql(sql, par);

       }

       public CourierNumberEntity getCourierNumberEntity(string exType)
       {
           CourierNumberEntity cn = new CourierNumberEntity();
           var sql = "select top 1 * from courier_number  where expressType=@expressType and state=0"; 
           SqlParameter[] par = new SqlParameter[] { 
           new SqlParameter("@expressType",exType)
           }; 
           using (var db = helpSql.ExecuteReader(sql, par))
           {
               if (db.HasRows) {
                   while (db.Read()) { 
                       cn.id = db.GetInt32(0);
                       cn.courierNumber = db.GetString(1);
                       cn.expressType = db.GetString(2);
                       cn.state = db.GetByte(3); 
                   }
               }
           }
           return cn;
       }
      
       /// <summary>
       /// 更新对应的订单
       /// </summary>
       /// <param name="id"></param>
       /// <returns></returns>
       public int update(int id,string username) {

           var sql = "update courier_number set state=1 ,updateDate=@updateDate,updateUser=@updateUser where id=@id";

           SqlParameter[] par = new SqlParameter[] { 
              new SqlParameter("@id",id),
              new SqlParameter("@updateDate",DateTime.Now),
              new SqlParameter("@updateUser",username)
           };
           return helpSql.ExecuteSql(sql, par);
       }
       /// <summary>
       /// 汇总所有数据
       /// </summary>
       /// <returns></returns>
       public List<CourierNumberCountEntity> getAllCount()
       {

           List<CourierNumberCountEntity> list = new List<CourierNumberCountEntity>();

           var sql = " select expressType ,[0] as 'notUsed',[1] as 'alreadyUsed' from (select expressType ,[state],count(state) as allCount from courier_number group by expressType,state)as b pivot ( sum(allCount) for [state] in ([0],[1]))as t";

           using (var db = helpSql.ExecuteReader(sql, null)) {

               if (db.HasRows) {
                   while (db.Read()) {
                       CourierNumberCountEntity cn = new CourierNumberCountEntity();
                       cn.expressType = db.GetString(0);
                       cn.notUsed = db.IsDBNull(1) ? 0 : db.GetInt32(1);
                       cn.alreadyUsed = db.IsDBNull(2) ? 0 : db.GetInt32(2);
                       list.Add(cn);
                   }
               }
           }
           return list;
       }
    }
}
