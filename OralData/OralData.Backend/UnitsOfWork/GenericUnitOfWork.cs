using OralData.Backend.Interfaces;
using Microsoft.EntityFrameworkCore;
using OralData.Shared.Responses;
using OralData.Shared.Entities;

namespace OralData.Backend.UnitsOfWork
{
    public class GenericUnitOfWork<T> : IGenericUnitOfWork<T> where T : class
    {
        private readonly IGenericUnitOfWork<T> _repository;

        public GenericUnitOfWork(IGenericUnitOfWork<T> repository)
        {
            _repository = repository;
        }

        public async Task<Response<T>> AddAsync(T model) => await _repository.AddAsync(model);

        public async Task<Response<T>> DeleteAsync(int id) => await _repository.DeleteAsync(id);

        public async Task<Response<IEnumerable<T>>> GetAsync() => await _repository.GetAsync();

        public async Task<Response<T>> GetAsync(int id) => await _repository.GetAsync(id);

        public async Task<Response<T>> UpdateAsync(T model) => await _repository.UpdateAsync(model);
    }
}