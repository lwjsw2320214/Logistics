using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entity;
using Dao; 

namespace Service
{
    public class EmsKindService
    {

        private EmsKindDao emsKindDao = new EmsKindDao();

        public List<EmsKindEntity> getAll() {

            return emsKindDao.getAll();
        }
         
    }
}
