using Entities.DataTransferObject;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using System.Text.Json;

namespace StoreApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IServiceManager _manager;

        public BooksController(IServiceManager manager)
        {
            _manager = manager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBooks([FromQuery] RequestParameters requestParameters)
        {
            var pagedResult = await _manager.BookService.GetAllBooksAsync(requestParameters, false);
			Response.Headers["X-Pagination"] = JsonSerializer.Serialize(pagedResult.metaData);
			return Ok(pagedResult.books);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetOneBookAsync([FromRoute(Name = "id")] int id)
        {
            return Ok(await _manager.BookService.GetOneBookByIdAsync(id, false));
        }

        [HttpPost]
        public async Task<IActionResult> CreateOneBookAsync([FromBody] BookDto bookDto)
        {
            if (!ModelState.IsValid)
                return UnprocessableEntity();
            await _manager.BookService.CreateOneBookAsync(bookDto);
            return Created();
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateOneBookAsync([FromRoute(Name = "id")] int id, [FromBody] BookDto bookDto)
        {
            if (!ModelState.IsValid)
                return UnprocessableEntity();
            await _manager.BookService.UpdateOneBookAsync(id, bookDto, false);
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteOneBookAsync([FromRoute(Name = "id")] int id)
        {
            if (!ModelState.IsValid)
                return UnprocessableEntity();
            await _manager.BookService.DeleteOneBookAsync(id, false);
            return Ok();
        }
    }
}
