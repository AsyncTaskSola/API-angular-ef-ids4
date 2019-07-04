using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogDemoApi.Resources
{
    public class PostResource
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Boby { get; set; }
        public string Author { get; set; }
        public DateTime UpDateTime { get; set; }

        public string Remark { get; set; }
    }
}
