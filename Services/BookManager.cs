﻿using AutoMapper;
using Entities.DataTransferObject;
using Entities.Exceptions;
using Entities.Models;
using Entities.RequestFeatures;
using Repositories.Contracts;
using Services.Contracts;

namespace Services
{
    public class BookManager : IBookService
    {
        private readonly IRepositoryManager _manager;
        private readonly IMapper _mapper;

        public BookManager(IRepositoryManager manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
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

        public async Task<(IEnumerable<BookDto> books, MetaData metaData)> GetAllBooksAsync(RequestParameters requestParameters, bool trackChanges)
        {
            if(!requestParameters.ValidPriceRange)
                throw new NotFoundException($"ValidPriceRange is invalid.");
			var booksWithMetaData = await _manager.BookRepository.GetAllBooksAsync(requestParameters, trackChanges);
            var booksDto = _mapper.Map<IEnumerable<BookDto>>(booksWithMetaData);
            return (booksDto, booksWithMetaData.MetaData);
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
