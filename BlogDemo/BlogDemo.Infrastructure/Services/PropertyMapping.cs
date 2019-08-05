using System;
using System.Collections.Generic;
using System.Text;
using BlogDemo.Core.interfaces;

namespace BlogDemo.Infrastructure.Services
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TSource">原类型 Resource</typeparam>
    /// <typeparam name="TDestination">封装类型 Entity</typeparam>
    public abstract class PropertyMapping<TSource, TDestination> : IPropertyMapping
        where TDestination : IEntity
    {
        /// <summary>
        /// Resource 属性映射到entity属性
        /// </summary>
        public Dictionary<string, List<MappedProperty>> MappingDictionary { get; }

           protected PropertyMapping(Dictionary<string, List<MappedProperty>> mappingDictionary)
        {
            MappingDictionary = mappingDictionary;
            MappingDictionary[nameof(IEntity.Id)] = new List<MappedProperty>
            {
                new MappedProperty { Name = nameof(IEntity.Id), Revert = false}
            };
        }
    }
}
