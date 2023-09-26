using Memory.Core.DataAccess;
using Memory.DataAccess.Abstract;
using Memory.DataAccess.Concrete.EntityFramework.Context;
using Memory.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memory.DataAccess.Concrete
{
    public class NotebookDal : RepositoryBase<Notebook>, INotebookDal
    {
        public NotebookDal(MemoryContext memoryContext) : base(memoryContext)
        {
        }
    }
}
