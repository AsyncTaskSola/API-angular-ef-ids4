using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace BlogDemo.Infrastructure.Services
{
    /// <summary>
    /// 判断字段是否存在
    /// </summary>
    public class TypeHelperService : ITypeHelperService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="fields">字段</param>
        /// <returns></returns>
        public bool TypeHasProperties<T>(string fields)
        {
            if (string.IsNullOrEmpty(fields))
            {
                return true;
            }

            var fieldsAfterSplit = fields.Split(',');
            foreach (var field in fieldsAfterSplit)
            {
                var propertyName = field.Trim();
                if (string.IsNullOrEmpty(propertyName))
                {
                    continue;
                }
                var propertyInfo = typeof(T).GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public| BindingFlags.Instance);
                if (propertyInfo == null)
                {
                    return false;
                }

            }

            return true;
        }
    }
}
