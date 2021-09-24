using ApplicationCore.Etities;
using Ardalis.Specification;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationCore
{
    //T kullanarak generic yaptık ama baseentity den miras alanların türünde olabilir diye kısıtladık
    //example IAsyncRepository<Product> gibi
    public interface IAsyncRepository<T> where T : BaseEntity  
    {

        Task<T> GetByIdAsync(int id);
        Task<List<T>> ListAllAsync();

        Task<List<T>> ListAsync(ISpecification<T> spec);

        Task<T> AddAsync(T entity);

        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);

        Task<int> CountAsync(ISpecification<T> spec);
        Task<T> FirstAsync(ISpecification<T> spec);
        Task<T> FirstOrDefaultAsync(ISpecification<T> spec);




    }
}
