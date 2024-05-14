using AutoMapper;
using Entities.DataTransferObject;
using Repositories.Contracts;
using Services.Contracts;

namespace Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IBookService> _bookService;

        public ServiceManager(IRepositoryManager manager, IMapper mapper, IDataShaper<BookDto> dataShaper)
        {
            _bookService = new Lazy<IBookService>(() => new BookManager(manager, mapper, dataShaper));
        }

        public IBookService BookService => _bookService.Value;
    }
}
