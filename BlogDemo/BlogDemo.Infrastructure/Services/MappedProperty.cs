
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BlogDemo.Infrastructure.Services
{
    /// <summary>
    /// 映射属性
    /// </summary>
    public class MappedProperty
    {
        public string Name { get; set; }
        /// <summary>
        /// 排序是否相反
        /// </summary>
        public bool Revert { get; set; }
    }
}