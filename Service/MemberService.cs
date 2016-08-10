using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dao;
using Entity;

namespace Service
{
    public class MemberService
    {
        private MemberDao memberDao = new MemberDao();

        public int RowCount = 0;

        public int GetMembericid(String username) {

            return memberDao.getMembericid(username);
        }

        public List<ClientUser> getPage(string username, int page, int pageSize) {

            return memberDao.getPage(username, page, pageSize);
        }


        public int getPageCount(string username,int pageSize) {

            var pageCount = 0;

             RowCount = memberDao.getPageCount(username);

            if (RowCount % pageSize == 0)
            {

                pageCount = RowCount / pageSize;
            }
            else
            {
                pageCount = RowCount / pageSize + 1;

            }

            return pageCount;
        }


        public int add(ClientUser cu) {
            int i = 0;
           var count= memberDao.selectCount(cu);
           if (count > 0)
           {

               i=memberDao.update(cu);
           }
           else {
               i=memberDao.add(cu);
           }

            return i;
        }

    }
}
