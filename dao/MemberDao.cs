using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using DBUtility;
using Entity; 
namespace Dao
{
    public class MemberDao
    {

        private HelpSQL helpSql = new HelpSQL("sqlconn");
        public int getMembericid(string username) {

            var  icid=0;

            var sql = "select icid from client_arc where cwebac=@cwebac";

            SqlParameter[] par = new SqlParameter[] { 
              new SqlParameter("@cwebac",username)
            };

            using (var db = helpSql.ExecuteReader(sql, par))
            {
                if (db.HasRows) {
                    while (db.Read()) { 
                        icid=db.GetInt32(0);
                    }
                }
            }
            return icid;
        }

        public List<ClientUser> getPage(string username, int page, int pageSize)
        {

            List<ClientUser> list = new List<ClientUser>();

            StringBuilder sb=new StringBuilder();
            List<SqlParameter> par = new List<SqlParameter>();

            sb.Append("select * from ( ");
            sb.Append("select mcaccount,ROW_NUMBER() over (order by wcid asc) as rownumber from [ginfo].[dbo].[member_main] where 1=1 ");
            if (!string.IsNullOrEmpty(username)) {
                sb.Append(" and mcaccount=@mcaccount ");
                par.Add(new SqlParameter("@mcaccount", username));
            }
            sb.Append(") as b ");
            sb.Append("where b.rownumber between @stateNumber and @endNumber ");

            par.Add(new SqlParameter("@stateNumber", (page - 1) * pageSize + 1));
            par.Add(new SqlParameter("@endNumber", page * pageSize));

            using (var db = helpSql.ExecuteReader(sb.ToString(), par.ToArray())) {

                if (db.HasRows) {  
                    while(db.Read()){
                        ClientUser cu = new ClientUser();
                        cu.username = db.GetString(0);
                        list.Add(cu);
                    }
                } 
            } 
            return list;
        }

        public int getPageCount(string username) {

            List<SqlParameter> par = new List<SqlParameter>();
            var sql = " select COUNT(mcaccount)  from [ginfo].[dbo].[member_main]  where 1=1 ";
            if (!string.IsNullOrEmpty(username))
            { 
                sql += " and mcaccount=@mcaccount"; 
                par.Add(new SqlParameter("@mcaccount", username));
            }
            return helpSql.RunSql(sql, par.ToArray());
        }


        /// <summary>
        /// 判断当前用户是否存在
        /// </summary>
        /// <param name="cu"></param>
        /// <returns></returns>
        public int selectCount(ClientUser cu) {

            var sql = "select count(*) from [ginfo].[dbo].[client] where mcaccount=@mcaccount";
            SqlParameter[] par = new SqlParameter[] {
                 new SqlParameter("@mcaccount",cu.username)
            };
            return helpSql.RunSql(sql, par);
        }

        /// <summary>
        /// 添加新用户名和密码
        /// </summary>
        /// <param name="cu"></param>
        /// <returns></returns>
        public int add(ClientUser cu) {
            var sql = "insert into  [ginfo].[dbo].[client] (clientpassword,mcaccount) values (@clientpassword,@mcaccount)";
            SqlParameter[] par = new SqlParameter[] {  
                new SqlParameter("@clientpassword",cu.password),
                 new SqlParameter("@mcaccount",cu.username)
            };  
            return  helpSql.ExecuteSql(sql,par);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="cu"></param>
        /// <returns></returns>
        public int update(ClientUser cu) {
            var sql = " update [ginfo].[dbo].[client] set clientpassword= @clientpassword where mcaccount=@mcaccount";

            SqlParameter[] par = new SqlParameter[] {  
                new SqlParameter("@clientpassword",cu.password),
                 new SqlParameter("@mcaccount",cu.username)
            };
            return helpSql.ExecuteSql(sql, par);
        }

    }
}
