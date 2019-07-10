using System;
using System.Collections.Generic;
using System.Text;

namespace BlogDemo.Infrastructure.Resources
{
    public class LinkResource
    {
        public string Href { get; set; }
        public string Rel { get; set; }
        public string Method { get; set; }
        public LinkResource(string href, string rel, string method)
        {
            Href = href;
            Rel = rel;
            Method = method;
        }
    }
}
