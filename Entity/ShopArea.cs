using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity
{
    public class ShopArea
    {

        /// <summary>
        /// 主键id
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// 省
        /// </summary>
        public String province { get; set; }

        /// <summary>
        /// 市
        /// </summary>
        public String city { get; set; }

        /// <summary>
        /// 区
        /// </summary>
        public string area { get; set; }

        /// <summary>
        /// 目的地
        /// </summary>
        public string destination { get; set; }

    }
}
