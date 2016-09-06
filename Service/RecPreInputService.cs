using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dao;
using Entity;

namespace Service
{
    public class RecPreInputService
    {

        private RecPreInputDao recPreInputDao = new RecPreInputDao();

        //获取记录
        public List<RecPreInputEntity> GetPage(int page, int pageSize, int icid, int irid, string cemskind, string cnum, string cdes, string cmark, string bdate, string edate)
        {
            if (page == 0)
            {
                page = 1;
            }
            return recPreInputDao.GetPage(page, pageSize, icid, irid, cemskind, cnum, cdes, cmark, bdate, edate);
        }

        //获取总重量
        public decimal getTotal(int icid, int irid ,string cemskind, string cnum, string cdes, string cmark, string bdate, string edate) {
            return recPreInputDao.getTotal(icid, irid, cemskind, cnum, cdes, cmark, bdate, edate);
        }

        //获取总页数
        public int getPageCount(int icid, int irid, int pageSize, string cemskind, string cnum, string cdes, string cmark, string bdate, string edate)
        {

            var total = recPreInputDao.getTotalRow(icid, irid, cemskind, cnum, cdes, cmark, bdate, edate);

            var page = 0;

            if (total % pageSize == 0)
            {

                page = total / pageSize;
            }
            else {

                page = total / pageSize+1;
            }

            return page;
        }

        //获取总数量
        public int getRowCount(int icid, int irid, string cemskind, string cnum, string cdes, string cmark, string bdate, string edate)
        {

            return recPreInputDao.getTotalRow(icid, irid, cemskind, cnum, cdes, cmark, bdate, edate);
        }

        public RecPreInputEntity findById(int iid, int icid, int irid) {

            return recPreInputDao.findById(iid, icid, irid);
        }

        public int UpdateOrder(string orderid, string mailno, string destcode, string cemskind, int iid)
        {
            return recPreInputDao.UpdateOrder(orderid, mailno, destcode, cemskind, iid);
        }

        public RecPreInputEntity findOrder(int iid, int icid, int irid) {

            return recPreInputDao.findOrder(iid, icid, irid);
        }

        public int updatecdes(int id, string cdes) {

            return recPreInputDao.updatecdes(id, cdes);
        }

        public ExpressPrint getRowId(string orderNumber, int icid, int irid)
        {

            return recPreInputDao.getRowId(orderNumber,icid,irid);
        }
    }
}
