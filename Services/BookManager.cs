using AutoMapper;
using Entities.DataTransferObject;
using Entities.Exceptions;
using Entities.Models;
using Entities.RequestFeatures;
using Repositories.Contracts;
using Services.Contracts;
using System.Dynamic;

namespace Services
{
    public class BookManager : IBookService
    {
        private readonly IRepositoryManager _manager;
        private readonly IMapper _mapper;
        private readonly IDataShaper<BookDto> _shaper;

		public BookManager(IRepositoryManager manager, IMapper mapper, IDataShaper<BookDto> shaper)
		{
			_manager = manager;
			_mapper = mapper;
			_shaper = shaper;
		}

		public async Task CreateOneBookAsync(BookDto bookDto)
        {
            _manager.BookRepository.Create(_mapper.Map<Book>(bookDto));
            await _manager.SaveAsync();
        }

        public async Task DeleteOneBookAsync(int id, bool trackChanges)
        {
            _manager.BookRepository.Delete(await GetOneBookByIdAndCheckExists(id, trackChanges));
            await _manager.SaveAsync();
        }

        public async Task<(IEnumerable<ExpandoObject> books, MetaData metaData)> GetAllBooksAsync(RequestParameters requestParameters, bool trackChanges)
        {
            if(!requestParameters.ValidPriceRange)
                throw new NotFoundException($"ValidPriceRange is invalid.");
			var booksWithMetaData = await _manager.BookRepository.GetAllBooksAsync(requestParameters, trackChanges);
            var booksDto = _mapper.Map<IEnumerable<BookDto>>(booksWithMetaData);

            var shapedData = _shaper.ShapeData(booksDto, requestParameters.Fields);
            return (shapedData, booksWithMetaData.MetaData);
        }

        public async Task<BookDto> GetOneBookByIdAsync(int id, bool trackChanges)
        {
            return _mapper.Map<BookDto>(await GetOneBookByIdAndCheckExists(id, trackChanges));
        }

        public async Task UpdateOneBookAsync(int id, BookDto bookDto, bool trackChanges)
        {
            var book = await GetOneBookByIdAndCheckExists(id, trackChanges);
			book = _mapper.Map<Book>(bookDto);
            _manager.BookRepository.Update(book);
            await _manager.SaveAsync();
        }

        private async Task<Book> GetOneBookByIdAndCheckExists(int id, bool trackChanges)
        {
            var entity = await _manager.BookRepository.GetOneBookByIdAsync(id, trackChanges);
            if (entity == null)
                throw new NotFoundException($"The book with ID number {id} was not found.");
            return entity;
        }
    }
}
