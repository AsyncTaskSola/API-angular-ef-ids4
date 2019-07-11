using System;
using System.Collections.Generic;
using System.Text;

namespace BlogDemo.Core.Entities
{
   public class PaginateList<T>:List<T> where T :class 
    {
        /// <summary>
        /// 每页有多少条数据
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 显示第几页
        /// </summary>
        public int PageIndex { get; set; }

        private int _totalItemsCount;
        /// <summary>
        /// 显示总数
        /// </summary>

        public int TotalItemsCount
        {
            get => _totalItemsCount;
            set => _totalItemsCount = value >= 0 ? value : 0;
        }

        public int PageCount=>TotalItemsCount/PageSize+(TotalItemsCount%PageSize>0?1:0);

        public bool HasPrevious => PageIndex > 0;
        public bool HasNext => PageIndex < PageCount - 1;

        public PaginateList(int pageIndex, int pageSize, int totalItemsCount, IEnumerable<T> data)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalItemsCount = totalItemsCount;
            AddRange(data);
        }

    }
}
