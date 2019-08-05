
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BlogDemo.Infrastructure.Resources
{
   public class PostImageResource
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string location => $"/uploads/{FileName}";
    }
}