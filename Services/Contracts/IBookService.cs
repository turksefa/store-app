using Entities.DataTransferObject;
using Entities.RequestFeatures;
using System.Dynamic;

namespace Services.Contracts
{
    public interface IBookService
    {
        Task<(IEnumerable<ExpandoObject> books, MetaData metaData)> GetAllBooksAsync(RequestParameters requestParameters, bool trackChanges);
        Task<BookDto> GetOneBookByIdAsync(int id, bool trackChanges);
        Task CreateOneBookAsync(BookDto bookDto);
        Task UpdateOneBookAsync(int id, BookDto bookDto, bool trackChanges);
        Task DeleteOneBookAsync(int id, bool trackChanges);
    }
}
