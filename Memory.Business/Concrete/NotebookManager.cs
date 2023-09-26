using AutoMapper;
using FluentValidation.Results;
using Memory.Business.Abstract;
using Memory.Business.ValidationRules.FluentValidation;
using Memory.DataAccess.Abstract;
using Memory.Entities.Concrete;
using Memory.Entities.Concrete.Dtos;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memory.Business.Concrete
{
    public class NotebookManager : INotebookService
    {
        private readonly INotebookDal _notebookDal;
        private readonly IMapper _mapper;
        
        NotebookValidator rules=new NotebookValidator();

        private ValidationResult Validate(Notebook notebook) // sürekli uzun uzun yazmak yerine kısa kısa 
        {
            return rules.Validate(notebook);
        }

        public NotebookManager(INotebookDal notebookDal,IMapper mapper)
        {
            _notebookDal = notebookDal;
            _mapper = mapper;
        }

        public Notebook NotebookDtoConvert(NotebookDto notebookDto)
        {
            return _mapper.Map<Notebook>(notebookDto);
        }
        public async Task<int> AddNotebookAsync(NotebookDto notebookDto)
        {
            Notebook notebook = _mapper.Map<Notebook>(notebookDto);
            ValidationResult result = Validate(notebook);
            //if (result.IsValid)
            //{
            //    return await _notebookDal.AddAsync(NotebookDtoConvert(notebookDto));
            //}

            //return 0;
          
            return result.IsValid ? await _notebookDal.AddAsync(notebook) : 0;
           
          
        }

        public async Task<NotebookDto> GetNotebookAsync(int id)
        {
           Notebook notebook= await _notebookDal.GetAsync(x=>x.Id == id);
            return _mapper.Map<NotebookDto>(notebook);
        }

        public async Task<List<NotebookDto>> GetNotebookListAsync()
        {
           List<Notebook> notebooks= await _notebookDal.GetAllAsync();
           List<NotebookDto> notebookDtos= new List<NotebookDto>();

            foreach (Notebook notebook in notebooks) 
            { 
               
               notebookDtos.Add(_mapper.Map<NotebookDto>(notebook));
            }

            return notebookDtos;
        }

        public async Task<int> RemoveNotebookAsync(NotebookDto notebookDto)
        {
            Notebook notebook=_mapper.Map<Notebook>(notebookDto);
            return await _notebookDal.DeleteAsync(notebook);
            
        }

        public async Task<int> UpdateNotebookAsync(NotebookDto notebookDto)
        {
            Notebook notebook=_mapper.Map<Notebook>(notebookDto);
            ValidationResult result=rules.Validate(notebook);

            return result.IsValid ? await _notebookDal.UpdateAsync(notebook) : 0;
        }
        
            
          
        

        // mapper eşleme
    }
}
