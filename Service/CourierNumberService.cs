using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entity;
using Dao;
using System.Transactions;

namespace Service
{
    public class CourierNumberService
    {
        private int RowCount = 0;
        private CourierNumberDao courierNumberDao = new CourierNumberDao();

        /// <summary>
        /// 获取每夜显示的记录
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="state"></param>
        /// <param name="expressType"></param>
        /// <returns></returns>
        public List<CourierNumberEntity> getPageRow(int page, int pageSize, int state, string expressType, string updateUser)
        {
            return courierNumberDao.getPageRow(page, pageSize, state, expressType,updateUser);
        }
         
        /// <summary>
        /// 得到总页数
        /// </summary>
        /// <param name="state"></param>
        /// <param name="expressType"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public int getPageCount(int state, string expressType, int pageSize, string updateUser)
        { 
            var pageCount = 0;

            RowCount=courierNumberDao.getCount(state, expressType,updateUser);

            if (RowCount % pageSize == 0)
            {

                pageCount = RowCount / pageSize;
            }
            else {
                pageCount = RowCount / pageSize+1;

            }

            return pageCount;
        }

        /// <summary>
        /// 获取总记录数
        /// </summary>
        /// <returns></returns>
        public int getAllRowCount() {
            return RowCount;
        }

        /// <summary>
        /// 批量添加
        /// </summary>
        /// <param name="list"></param>
        /// <param name="cemskind"></param>
        /// <returns></returns>
        public int addAll(List<string> list, string cemskind)
        {
            return courierNumberDao.addAll(list, cemskind);
        }

        public int delete(int id) { 
            return courierNumberDao.delete(id);
        }

        /// <summary>
        /// 获取当前类别下的所有快递单号
        /// </summary>
        /// <param name="exType"></param>
        /// <returns></returns>
        public CourierNumberEntity getCourierNumberEntity(string exType)
        {
            return courierNumberDao.getCourierNumberEntity(exType);
        }


        /// <summary>
        /// 更新快递单号
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int update(int id,string username) {

            return courierNumberDao.update(id,username);
        }

        public List<CourierNumberCountEntity> getAllCount() {

            return courierNumberDao.getAllCount();
        }

        /// <summary>
        /// 获取重复订单
        /// </summary>
        /// <param name="number"></param>
        /// <param name="expressType"></param>
        /// <returns></returns>
        public int getRowCount(string number, string expressType)
        {

            return courierNumberDao.getRowCount(number, expressType);
        }
 
    }
}
