using System;
using System.Collections.Generic;
using System.Text;
using BlogDemo.Core.interfaces;

namespace BlogDemo.Core.Entities
{
    public class Entity : IEntity
    {
        public int Id { get; set; }
    }
}
