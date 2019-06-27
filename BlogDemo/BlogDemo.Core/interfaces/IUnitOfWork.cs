using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlogDemo.Core.interfaces
{
   public  interface IUnitOfWork
   {
       Task<bool> SaveAsync();
   }
}
