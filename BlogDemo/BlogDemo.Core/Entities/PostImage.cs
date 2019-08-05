using System;
using System.Collections.Generic;
using System.Text;

namespace BlogDemo.Core.Entities
{
    /// <summary>
    /// 上传图片
    /// </summary>
    public class PostImage:Entity
    {
        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName { get; set; }
    }
}
