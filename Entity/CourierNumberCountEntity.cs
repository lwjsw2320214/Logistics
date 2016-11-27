using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity
{
    public class CourierNumberCountEntity
    {

        /// <summary>
        /// 快递类别
        /// </summary>
        public string expressType { get; set; }

        /// <summary>
        /// 未使用
        /// </summary>
        public int notUsed { get; set; }

        /// <summary>
        /// 已使用
        /// </summary>
        public int alreadyUsed { get; set; }

    }
}
