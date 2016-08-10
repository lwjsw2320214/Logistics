using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBUtility;
using System.Data.SqlClient;
using Entity;
using System.Collections;

namespace Dao
{
    public class EmsKindDao
    {

        private HelpSQL helpSql = new HelpSQL("sqlconn");

        /// <summary>
        /// 获取快递公司
        /// </summary>
        /// <returns></returns>
        public List<EmsKindEntity> getAll() {

            List<EmsKindEntity> list = new List<EmsKindEntity>();

            var sql = "select cemskind from ems_kind  where ninput=1 ";

            using (var db = helpSql.ExecuteReader(sql, null)) {

                if (db.HasRows) {

                    while (db.Read()) {
                        EmsKindEntity e = new EmsKindEntity();

                        e.cemskind = db.GetString(0);
                        list.Add(e);
                    }
                }
            }
            return list;
        }

        

    }
}
