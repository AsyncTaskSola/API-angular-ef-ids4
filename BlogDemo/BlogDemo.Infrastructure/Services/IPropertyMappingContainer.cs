using System;
using System.Collections.Generic;
using System.Text;
using BlogDemo.Core.interfaces;

namespace BlogDemo.Infrastructure.Services
{
    /// <summary>
    /// 容器接口
    /// </summary>
    public interface IPropertyMappingContainer
    {
        /// <summary>
        /// 寄存器
        /// </summary>
        /// <typeparam name="T"></typeparam>
        void Register<T>() where T : IPropertyMapping, new();
        /// <summary>
        /// 注册
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TDestination"></typeparam>
        /// <returns></returns>
        IPropertyMapping Resolve<TSource, TDestination>() where TDestination : IEntity;
        /// <summary>
        /// 验证
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="fields"></param>
        /// <returns></returns>
        bool ValidateMappingExistsFor<TSource, TDestination>(string fields) where TDestination : IEntity;
    }
}
