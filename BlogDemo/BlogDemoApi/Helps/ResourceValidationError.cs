using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogDemoApi.Helps
{
    /// <summary>
    /// 这下面写的都是为了angular 能显示错误类型
    /// </summary>
    public class ResourceValidationError
    {
        /// <summary>
        ///详细键
        /// </summary>
        public string ValidatorKey { get; set; }
        /// <summary>
        /// 内容，信息
        /// </summary>
        public string Message { get; set; }

        public ResourceValidationError(string messge,string validatorkey="")
        {
            ValidatorKey = validatorkey;
            Message = messge;
        }

    }
}
