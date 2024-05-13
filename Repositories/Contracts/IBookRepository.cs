using Entities.Models;
using Entities.RequestFeatures;

namespace Repositories.Contracts
{
    public interface IBookRepository : IRepositoryBase<Book>
    {
        Task<PagedList<Book>> GetAllBooksAsync(RequestParameters requestParameters, bool trackChanges);
        Task<Book?> GetOneBookByIdAsync(int id, bool trackChanges);
    }
}
