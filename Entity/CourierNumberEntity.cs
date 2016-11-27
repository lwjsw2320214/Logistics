using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity
{
    public class CourierNumberEntity
    {

        /// <summary>
        /// 主键
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// 快递单号
        /// </summary>
        public string courierNumber { get; set; }

        /// <summary>
        /// 快递类别
        /// </summary>
        public string expressType { get; set; }

        /// <summary>
        /// 快递单号是否使用
        /// 1为使用0未使用
        /// </summary>
        public byte state { get; set; }

        /// <summary>
        /// 使用日期
        /// </summary>
        public DateTime? updateDate { get; set; }

        /// <summary>
        /// 使用人
        /// </summary>
        public string updateUser { get; set; }

    }
}
