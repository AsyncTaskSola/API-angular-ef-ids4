using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BlogDemo.Core.interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlogDemo.Infrastructure.Database
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MyContext _myContext;

        public UnitOfWork(MyContext myContext)
        {
            _myContext = myContext;
        }
        public async Task<bool> SaveAsync()
        {
            return await _myContext.SaveChangesAsync() > 0;
        }
    }
}
