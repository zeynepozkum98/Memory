using Memory.Entities.Concrete.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memory.Business.Abstract
{
    public interface INotebookService
    {
        Task<int> AddNotebookAsync(NotebookDto notebookDto);
        Task<List<NotebookDto>> GetNotebookListAsync();
        Task<NotebookDto> GetNotebookAsync(int id);
        Task<int> RemoveNotebookAsync(NotebookDto notebookDto);
        Task<int> UpdateNotebookAsync(NotebookDto notebookDto);
    }
}
