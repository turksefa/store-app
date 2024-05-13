using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;

namespace Repositories.EFCore
{
    public class BookRepository : RepositoryBase<Book>, IBookRepository
    {
        public BookRepository(RepositoryContext context) : base(context)
        {
        }

		public async Task<PagedList<Book>> GetAllBooksAsync(RequestParameters requestParameters, bool trackChanges)
        {
            var books = trackChanges ?
                await FindAll(trackChanges)
                .FilterBooks(requestParameters.MinPrice, requestParameters.MaxPrice)
                .Search(requestParameters.SearchTerm)
                .ToListAsync() :
                await FindAll(trackChanges)
                .FilterBooks(requestParameters.MinPrice, requestParameters.MaxPrice)
                .AsNoTracking()
                .ToListAsync();
            return PagedList<Book>.ToPagedList(books, requestParameters.PageNumber, requestParameters.PageSize);
        }

        public async Task<Book?> GetOneBookByIdAsync(int id, bool trackChanges) => await FindByCondition(b => b.BookId.Equals(id), trackChanges).SingleOrDefaultAsync();
	}
}
