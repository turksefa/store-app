using AutoMapper;
using Repositories.Contracts;
using Services.Contracts;

namespace Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IBookService> _bookService;

        public ServiceManager(IRepositoryManager manager, IMapper mapper)
        {
            _bookService = new Lazy<IBookService>(() => new BookManager(manager, mapper));
        }

        public IBookService BookService => _bookService.Value;
    }
}
