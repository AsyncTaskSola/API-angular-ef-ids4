using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace BlogDemo.Infrastructure.Exceptions
{
    public static class EnumerableExtensions
    {
        /// <summary>
        /// 塑性模型（针对集合对象）
        /// </summary>
        /// <typeparam name="TSource">数据源</typeparam>
        /// <param name="source"></param>
        /// <param name="fields">字段</param>
        /// <returns></returns>
        public static IEnumerable<ExpandoObject> ToDynamicIEnumerable<TSource>(this IEnumerable<TSource> source, string fields = null)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var expandoObjectList = new List<ExpandoObject>();
            var propertyInfoList = new List<PropertyInfo>();//放需要的属性信息
            if (string.IsNullOrWhiteSpace(fields))
            {
                var propertyInfos = typeof(TSource).GetProperties(BindingFlags.Public | BindingFlags.Instance);
                propertyInfoList.AddRange(propertyInfos);
            }
            else
            {
                var fieldsAfterSplit = fields.Split(',').ToList();
                foreach (var field in fieldsAfterSplit)
                {
                    var propertyName = field.Trim();
                    if (string.IsNullOrEmpty(propertyName))
                    {
                        continue;
                    }
                    var propertyInfo = typeof(TSource).GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                    if (propertyInfo == null)
                    {
                        throw new Exception($"Property {propertyName} wasn't found on {typeof(TSource)}");
                    }
                    propertyInfoList.Add(propertyInfo);
                }
            }

            foreach (TSource sourceObject in source)
            {
                var dataShapedObject = new ExpandoObject();
                foreach (var propertyInfo in propertyInfoList)
                {
                    //反射的方式
                    var propertyValue = propertyInfo.GetValue(sourceObject);
                    ((IDictionary<string, object>)dataShapedObject).Add(propertyInfo.Name, propertyValue);
                }
                expandoObjectList.Add(dataShapedObject);
            }

            return expandoObjectList;
        }
    }
}
