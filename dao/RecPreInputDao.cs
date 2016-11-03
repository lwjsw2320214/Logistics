using DBUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using Entity;

namespace Dao
{
    public class RecPreInputDao
    {
        private HelpSQL helpSql = new HelpSQL("sqlconn");

        public List<RecPreInputEntity> GetPage(int page, int pageSize, int icid, int irid, string cemskind, string cnum, string cdes, string cmark, string bdate, string edate,int state)
        {

            List<RecPreInputEntity> list = new List<RecPreInputEntity>();

            List<SqlParameter> par = new List<SqlParameter>();

            par.Add(new SqlParameter("@icid",icid));
            par.Add(new SqlParameter("@irid",irid));
            par.Add(new SqlParameter("@startNumber",(page-1)*pageSize+1));
            par.Add(new SqlParameter("@endNumber", page * pageSize));

            StringBuilder sb=new StringBuilder();
            sb.Append("select * from (SELECT ");
            sb.Append("nitemtype,cdes,fweight,creceiver,cgoods,ddate,cnum,iitem,cmemo,npayway,cemskind,ISNULL(iex1,0) AS iex1,ISNULL(iex2,0) AS iex2,ISNULL(iex3,0) AS iex3,");
            sb.Append("iid,cno,creserve,idvalue,iivalue,ireserve,nctrl,crno,cunitname,caddr,ccity,cprovince,ccountry,cpostcode,cphone,cremail,crsms,faget,fasafe,fapack,faother,facheck,faremote,faby,");
            sb.Append("famount,ctransnote,cby1,cby2,cby3,cby4,cby5,ISNULL(iex4,0) AS iex4,ISNULL(iex5,0) AS iex5,cmark,Row_Number() over(ORDER BY iid DESC) as rowNumber FROM emmis.dbo.rec_pre_input ");
            sb.Append("WHERE icid=@icid AND irid=@irid ");
            //快递名称
            if (!string.IsNullOrEmpty(cemskind))
            {
                sb.Append(" AND cemskind=@cemskind");
                par.Add(new SqlParameter("@cemskind",  cemskind));
            }
            //快递单号
            if (!string.IsNullOrEmpty(cnum))
            {
                sb.Append(" AND cnum=@cnum");
                par.Add(new SqlParameter("@cnum", cnum));
            }
            //快递状体
            if (state == 1) {
                sb.Append(" AND state=0");
            }
            //快递状体
            if (state == 2)
            {
                sb.Append(" AND state=1");
            }
            if (!string.IsNullOrEmpty(cdes))
            {
                sb.Append(" AND cdes=@cdes");
                par.Add(new SqlParameter("@cdes", cdes));
            }
            if (!string.IsNullOrEmpty(cmark))
            {
                sb.Append(" AND cmark=@cmark");
                par.Add(new SqlParameter("@cmark", cmark));
            }
            if (!string.IsNullOrEmpty(bdate))
            {
                sb.Append(" AND ddate>=@ddate");
                par.Add(new SqlParameter("@ddate", DateTime.Parse(bdate)));
            }
            if (!string.IsNullOrEmpty(edate))
            {
                sb.Append(" AND ddate<@ddate");
                par.Add(new SqlParameter("@ddate", DateTime.Parse(edate).AddDays(1)));
            }
            sb.Append(") as a where rowNumber between @startNumber and @endNumber"); 

            using (var db = helpSql.ExecuteReader(sb.ToString(), par.ToArray())) {
                if (db.HasRows) {
                    while (db.Read()) {
                        byte i = 1;
                        RecPreInputEntity rp = new RecPreInputEntity();
                        rp.nitemtype = db.IsDBNull(0)? i : db.GetByte(0);
                        rp.cdes = db.GetString(1);
                        rp.fweight = db.IsDBNull(2) ? 0 : db.GetDecimal(2);
                        rp.creceiver = db.GetString(3);
                        rp.cgoods = db.GetString(4);
                        rp.ddate = db.GetDateTime(5);
                        rp.cnum = db.GetString(6);
                        rp.iitem = db.IsDBNull(7) ? 0 : db.GetInt32(7);
                        rp.cmemo = db.GetString(8);
                        rp.npayway = db.IsDBNull(9) ? i : db.GetByte(9);
                        rp.cemskind = db.GetString(10);
                        rp.iex1 = db.IsDBNull(11) ? 0 : db.GetInt32(11);
                        rp.iex2 = db.IsDBNull(12) ? 0 : db.GetInt32(12);
                        rp.iex3 = db.IsDBNull(13) ? 0 : db.GetInt32(13);
                        rp.iid = db.IsDBNull(14) ? 0 : db.GetInt32(14);
                        rp.cno = db.GetString(15);
                        rp.creserve = db.GetString(16);
                        rp.idvalue = db.IsDBNull(17) ? 0 : db.GetInt32(17);
                        rp.iivalue = db.IsDBNull(18) ? 0 : db.GetInt32(18);
                        rp.ireserve = db.IsDBNull(19) ? 0 : db.GetInt32(19);
                        rp.nctrl = db.IsDBNull(20) ? 0 : db.GetInt32(20);
                        rp.crno = db.GetString(21);
                        rp.cunitname = db.GetString(22);
                        rp.caddr = db.GetString(23);
                        rp.ccity = db.GetString(24);
                        rp.cprovince = db.GetString(25);
                        rp.ccountry = db.GetString(26);
                        rp.cpostcode = db.GetString(27);
                        rp.cphone = db.GetString(28);
                        rp.cremail = db.GetString(29);
                        rp.crsms = db.GetString(30);
                        rp.faget = db.IsDBNull(31) ? 0 : db.GetDecimal(31);
                        rp.fasafe = db.IsDBNull(32) ? 0 : db.GetDecimal(32);
                        rp.fapack = db.IsDBNull(33) ? 0 : db.GetDecimal(33);
                        rp.faother = db.IsDBNull(34) ? 0 : db.GetDecimal(34);
                        rp.facheck = db.IsDBNull(35) ? 0 : db.GetDecimal(35);
                        rp.faremote = db.IsDBNull(36) ? 0 : db.GetDecimal(36);
                        rp.faby = db.IsDBNull(37) ? 0 : db.GetDecimal(37);
                        rp.famount = db.IsDBNull(38) ? 0 : db.GetDecimal(38);
                        rp.ctransnote = db.GetString(39);
                        rp.cby1 = db.GetString(40);
                        rp.cby2 = db.GetString(41);
                        rp.cby3 = db.GetString(42);
                        rp.cby4 = db.GetString(43);
                        rp.cby5 = db.GetString(44);
                        rp.iex4 = db.IsDBNull(45) ? 0 : db.GetInt32(45);
                        rp.iex5 = db.IsDBNull(46) ? 0 : db.GetInt32(46);
                        rp.cmark = db.GetString(47);
                        list.Add(rp);
                    }
                } 
            }
            return list;
        }

        public decimal getTotal(int icid, int irid, string cemskind, string cnum, string cdes, string cmark, string bdate, string edate, int state)
        {
            List<SqlParameter> par = new List<SqlParameter>();
            decimal total = 0M;

            StringBuilder sb = new StringBuilder();
            sb.Append( "select sum(fweight) from [emmis].[dbo].[rec_pre_input] WHERE icid=@icid AND irid=@irid  "); 

            //快递名称
            if (!string.IsNullOrEmpty(cemskind))
            {
                sb.Append(" AND cemskind=@cemskind");
                par.Add(new SqlParameter("@cemskind", cemskind));
            }
            //快递单号
            if (!string.IsNullOrEmpty(cnum))
            {
                sb.Append(" AND cnum=@cnum");
                par.Add(new SqlParameter("@cnum", cnum));
            }
            //快递状体
            if (state == 1)
            {
                sb.Append(" AND state=0");
            }
            //快递状体
            if (state == 2)
            {
                sb.Append(" AND state=1");
            }
            if (!string.IsNullOrEmpty(cdes))
            {
                sb.Append(" AND cdes=@cdes");
                par.Add(new SqlParameter("@cdes", cdes));
            }
            if (!string.IsNullOrEmpty(cmark))
            {
                sb.Append(" AND cmark=@cmark");
                par.Add(new SqlParameter("@cmark", cmark));
            }
            if (!string.IsNullOrEmpty(bdate))
            {
                sb.Append(" AND ddate>=@ddate");
                par.Add(new SqlParameter("@ddate", DateTime.Parse(bdate)));
            }
            if (!string.IsNullOrEmpty(edate))
            {
                sb.Append(" AND ddate<@ddate");
                par.Add(new SqlParameter("@ddate", DateTime.Parse(edate).AddDays(1)));
            }

            par.Add(new SqlParameter("@icid", icid));
            par.Add(new SqlParameter("@irid", irid)); 
            using (var db = helpSql.ExecuteReader(sb.ToString(), par.ToArray())) {

                if (db.HasRows) {
                    while (db.Read()) {
                        total =db.IsDBNull(0)?0: db.GetDecimal(0);
                    }
                }
            }
            return total;
        }

        public int getTotalRow(int icid, int irid, string cemskind, string cnum, string cdes, string cmark, string bdate, string edate,int state)
        {
            List<SqlParameter> par = new List<SqlParameter>();

            StringBuilder sb = new StringBuilder();
            sb.Append("select count(iid) from [emmis].[dbo].[rec_pre_input] WHERE icid=@icid AND irid=@irid "); 

            //快递名称
            if (!string.IsNullOrEmpty(cemskind))
            {
                sb.Append(" AND cemskind=@cemskind");
                par.Add(new SqlParameter("@cemskind", cemskind));
            }
            //快递单号
            if (!string.IsNullOrEmpty(cnum))
            {
                sb.Append(" AND cnum=@cnum");
                par.Add(new SqlParameter("@cnum", cnum));
            }

            //快递状体
            if (state == 1)
            {
                sb.Append(" AND state=0");
            }
            //快递状体
            if (state == 2)
            {
                sb.Append(" AND state=1");
            }

            if (!string.IsNullOrEmpty(cdes))
            {
                sb.Append(" AND cdes=@cdes");
                par.Add(new SqlParameter("@cdes", cdes));
            }
            if (!string.IsNullOrEmpty(cmark))
            {
                sb.Append(" AND cmark=@cmark");
                par.Add(new SqlParameter("@cmark", cmark));
            }
            if (!string.IsNullOrEmpty(bdate))
            {
                sb.Append(" AND ddate>=@ddate");
                par.Add(new SqlParameter("@ddate", DateTime.Parse(bdate)));
            }
            if (!string.IsNullOrEmpty(edate))
            {
                sb.Append(" AND ddate<@ddate");
                par.Add(new SqlParameter("@ddate", DateTime.Parse(edate).AddDays(1)));
            }

            var total=0;
             
            par.Add(new SqlParameter("@icid", icid));
            par.Add(new SqlParameter("@irid", irid)); 
            total= helpSql.RunSql(sb.ToString(), par.ToArray());
            
            return total; 
        }

        public RecPreInputEntity findById(int iid, int icid, int irid)
        {

            List<SqlParameter> par = new List<SqlParameter>();
            RecPreInputEntity rp = new RecPreInputEntity();
            StringBuilder sb = new StringBuilder();
            sb.Append("select nitemtype,cdes,fweight,creceiver,cgoods,ddate,cnum,iitem,cmemo,npayway,cemskind,ISNULL(iex1,0) AS iex1,ISNULL(iex2,0) AS iex2,ISNULL(iex3,0) AS iex3,");
            sb.Append("iid,cno,creserve,idvalue,iivalue,ireserve,nctrl,crno,cunitname,caddr,ccity,cprovince,ccountry,cpostcode,cphone,cremail,crsms,faget,fasafe,fapack,faother,facheck,faremote,faby,");
            sb.Append("famount,ctransnote,cby1,cby2,cby3,cby4,cby5,ISNULL(iex4,0) AS iex4,ISNULL(iex5,0) AS iex5,cmark ");
            sb.Append(" from [emmis].[dbo].[rec_pre_input] WHERE icid=@icid AND irid=@irid And iid=@iid and state=0");
            par.Add(new SqlParameter("@icid", icid));
            par.Add(new SqlParameter("@irid", irid));
            par.Add(new SqlParameter("@iid", iid));
            using (var db = helpSql.ExecuteReader(sb.ToString(), par.ToArray()))
            { 
                if (db.HasRows)
                {
                    while (db.Read())
                    {
                        byte i = 1; 
                        rp.nitemtype = db.IsDBNull(0) ? i : db.GetByte(0);
                        rp.cdes = db.GetString(1);
                        rp.fweight = db.IsDBNull(2) ? 0 : db.GetDecimal(2);
                        rp.creceiver = db.GetString(3);
                        rp.cgoods = db.GetString(4);
                        rp.ddate = db.GetDateTime(5);
                        rp.cnum = db.GetString(6);
                        rp.iitem = db.IsDBNull(7) ? 0 : db.GetInt32(7);
                        rp.cmemo = db.GetString(8);
                        rp.npayway = db.IsDBNull(9) ? i : db.GetByte(9);
                        rp.cemskind = db.GetString(10);
                        rp.iex1 = db.IsDBNull(11) ? 0 : db.GetInt32(11);
                        rp.iex2 = db.IsDBNull(12) ? 0 : db.GetInt32(12);
                        rp.iex3 = db.IsDBNull(13) ? 0 : db.GetInt32(13);
                        rp.iid = db.IsDBNull(14) ? 0 : db.GetInt32(14);
                        rp.cno = db.GetString(15);
                        rp.creserve = db.GetString(16);
                        rp.idvalue = db.IsDBNull(17) ? 0 : db.GetInt32(17);
                        rp.iivalue = db.IsDBNull(18) ? 0 : db.GetInt32(18);
                        rp.ireserve = db.IsDBNull(19) ? 0 : db.GetInt32(19);
                        rp.nctrl = db.IsDBNull(20) ? 0 : db.GetInt32(20);
                        rp.crno = db.GetString(21);
                        rp.cunitname = db.GetString(22);
                        rp.caddr = db.GetString(23);
                        rp.ccity = db.GetString(24);
                        rp.cprovince = db.GetString(25);
                        rp.ccountry = db.GetString(26);
                        rp.cpostcode = db.GetString(27);
                        rp.cphone = db.GetString(28);
                        rp.cremail = db.GetString(29);
                        rp.crsms = db.GetString(30);
                        rp.faget = db.IsDBNull(31) ? 0 : db.GetDecimal(31);
                        rp.fasafe = db.IsDBNull(32) ? 0 : db.GetDecimal(32);
                        rp.fapack = db.IsDBNull(33) ? 0 : db.GetDecimal(33);
                        rp.faother = db.IsDBNull(34) ? 0 : db.GetDecimal(34);
                        rp.facheck = db.IsDBNull(35) ? 0 : db.GetDecimal(35);
                        rp.faremote = db.IsDBNull(36) ? 0 : db.GetDecimal(36);
                        rp.faby = db.IsDBNull(37) ? 0 : db.GetDecimal(37);
                        rp.famount = db.IsDBNull(38) ? 0 : db.GetDecimal(38);
                        rp.ctransnote = db.GetString(39);
                        rp.cby1 = db.GetString(40);
                        rp.cby2 = db.GetString(41);
                        rp.cby3 = db.GetString(42);
                        rp.cby4 = db.GetString(43);
                        rp.cby5 = db.GetString(44);
                        rp.iex4 = db.IsDBNull(45) ? 0 : db.GetInt32(45);
                        rp.iex5 = db.IsDBNull(46) ? 0 : db.GetInt32(46);
                        rp.cmark = db.GetString(47); ;
                    }
                }
            }
            return rp;
        }

        public RecPreInputEntity findOrder(int iid, int icid, int irid)
        {

            List<SqlParameter> par = new List<SqlParameter>();
            RecPreInputEntity rp = new RecPreInputEntity() ;
            StringBuilder sb = new StringBuilder();
            sb.Append("select nitemtype,cdes,fweight,creceiver,cgoods,ddate,cnum,iitem,cmemo,npayway,cemskind,ISNULL(iex1,0) AS iex1,ISNULL(iex2,0) AS iex2,ISNULL(iex3,0) AS iex3,");
            sb.Append("iid,cno,creserve,idvalue,iivalue,ireserve,nctrl,crno,cunitname,caddr,ccity,cprovince,ccountry,cpostcode,cphone,cremail,crsms,faget,fasafe,fapack,faother,facheck,faremote,faby,");
            sb.Append("famount,ctransnote,cby1,cby2,cby3,cby4,cby5,ISNULL(iex4,0) AS iex4,ISNULL(iex5,0) AS iex5,cmark ");
            sb.Append(" from [emmis].[dbo].[rec_pre_input] WHERE icid=@icid AND irid=@irid And iid=@iid ");
            par.Add(new SqlParameter("@icid", icid));
            par.Add(new SqlParameter("@irid", irid));
            par.Add(new SqlParameter("@iid", iid));
            using (var db = helpSql.ExecuteReader(sb.ToString(), par.ToArray()))
            {
                if (db.HasRows)
                {
                    while (db.Read())
                    {
                        byte i = 0;
                        rp.nitemtype = db.IsDBNull(0) ? i : db.GetByte(0);
                        rp.cdes = db.GetString(1);
                        rp.fweight = db.IsDBNull(2) ? 0 : db.GetDecimal(2);
                        rp.creceiver = db.GetString(3);
                        rp.cgoods = db.GetString(4);
                        rp.ddate = db.GetDateTime(5);
                        rp.cnum = db.GetString(6);
                        rp.iitem = db.IsDBNull(7) ? 0 : db.GetInt32(7);
                        rp.cmemo = db.GetString(8);
                        rp.npayway = db.IsDBNull(9) ? i : db.GetByte(9);
                        rp.cemskind = db.GetString(10);
                        rp.iex1 = db.IsDBNull(11) ? 0 : db.GetInt32(11);
                        rp.iex2 = db.IsDBNull(12) ? 0 : db.GetInt32(12);
                        rp.iex3 = db.IsDBNull(13) ? 0 : db.GetInt32(13);
                        rp.iid = db.IsDBNull(14) ? 0 : db.GetInt32(14);
                        rp.cno = db.GetString(15);
                        rp.creserve = db.GetString(16);
                        rp.idvalue = db.IsDBNull(17) ? 0 : db.GetInt32(17);
                        rp.iivalue = db.IsDBNull(18) ? 0 : db.GetInt32(18);
                        rp.ireserve = db.IsDBNull(19) ? 0 : db.GetInt32(19);
                        rp.nctrl = db.IsDBNull(20) ? 0 : db.GetInt32(20);
                        rp.crno = db.GetString(21);
                        rp.cunitname = db.GetString(22);
                        rp.caddr = db.GetString(23);
                        rp.ccity = db.GetString(24);
                        rp.cprovince = db.GetString(25);
                        rp.ccountry = db.GetString(26);
                        rp.cpostcode = db.GetString(27);
                        rp.cphone = db.GetString(28);
                        rp.cremail = db.GetString(29);
                        rp.crsms = db.GetString(30);
                        rp.faget = db.IsDBNull(31) ? 0 : db.GetDecimal(31);
                        rp.fasafe = db.IsDBNull(32) ? 0 : db.GetDecimal(32);
                        rp.fapack = db.IsDBNull(33) ? 0 : db.GetDecimal(33);
                        rp.faother = db.IsDBNull(34) ? 0 : db.GetDecimal(34);
                        rp.facheck = db.IsDBNull(35) ? 0 : db.GetDecimal(35);
                        rp.faremote = db.IsDBNull(36) ? 0 : db.GetDecimal(36);
                        rp.faby = db.IsDBNull(37) ? 0 : db.GetDecimal(37);
                        rp.famount = db.IsDBNull(38) ? 0 : db.GetDecimal(38);
                        rp.ctransnote = db.GetString(39);
                        rp.cby1 = db.GetString(40);
                        rp.cby2 = db.GetString(41);
                        rp.cby3 = db.GetString(42);
                        rp.cby4 = db.GetString(43);
                        rp.cby5 = db.GetString(44);
                        rp.iex4 = db.IsDBNull(45) ? 0 : db.GetInt32(45);
                        rp.iex5 = db.IsDBNull(46) ? 0 : db.GetInt32(46);
                        rp.cmark = db.GetString(47); ;
                    }
                }
            }
            return rp;
        }

        public int UpdateOrder(string orderid, string mailno, string destcode, string cemskind, int iid, decimal fweight)
        {

            var sql = "update rec_pre_input set cemskind=@cemskind ,cdes=@cdes,cnum=@cnum,fweight=@fweight,state=1 where  iid=@iid ";

            SqlParameter[] par = new SqlParameter[] { 
            new SqlParameter("@cemskind",cemskind),
            new SqlParameter("@cdes",destcode),
            new SqlParameter("@cnum",mailno),
            new SqlParameter("@fweight",fweight),
            new SqlParameter("@iid",iid)
            };
            return helpSql.ExecuteSql(sql, par);

        }

        public int updatecdes(int id, string cdes) {

            string sql = "update rec_pre_input set cdes=@cdes  where iid=@id and state=0";
            SqlParameter[] par = new SqlParameter[] { 
            new SqlParameter("@cdes",cdes),
            new SqlParameter("@id",id) 
            };
            return helpSql.ExecuteSql(sql, par);
        }

        /// <summary>
        /// like 查询iid
        /// 
        /// </summary>
        /// <param name="orderNumber"></param>
        /// <returns></returns>
        public ExpressPrint getRowId(string orderNumber,int icid,int irid)
        {

            ExpressPrint ep = new ExpressPrint();

            var iid = 0;

            var sql = "select top 1 iid,cemskind from rec_pre_input where icid=@icid  AND irid=@irid and cmemo like '%'+ @orderNumber + '%'";

            SqlParameter[] par = new SqlParameter[] { 
                new SqlParameter("@orderNumber",orderNumber),
                new SqlParameter("@icid",icid),
                new SqlParameter("@irid",irid)
            };

            using (var db = helpSql.ExecuteReader(sql, par)) {

                if (db.HasRows) {
                    while (db.Read()) {
                        ep.iid = db.GetInt32(0);
                        ep.cemskind = db.GetString(1);
                    }
                }
            }
            return ep;
        }
    }
}
