using Application.DTOs;
using Application.Interfaces;
using Application.Validation;
using Microsoft.AspNetCore.Mvc;

namespace ProjektZ.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReadingJournalController : ControllerBase
    {
        private readonly IReadingJournalRepository _iReadingJournalRepository;
        private readonly IReadingJournalService _iReadingJournalService;

        public ReadingJournalController(IReadingJournalRepository iReadingJournalRepository, IReadingJournalService iReadingJournalService)
        {
            _iReadingJournalRepository = iReadingJournalRepository;
            _iReadingJournalService = iReadingJournalService;

        }

        [HttpGet("books/getall")]
        public async Task<IActionResult> GetAllBooks()
        {
            try
            {
                var result = await _iReadingJournalService.GetAllBooksAsync();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("book/create")]
        public async Task<IActionResult> CreateBook([FromBody] BookDTO book)
        {
            try
            {
                var validator = new BookDTOValidator();
                var results = validator.Validate(book);

                if (!results.IsValid)
                {
                    return BadRequest(results.Errors);
                }

                var result = await _iReadingJournalService.CreateBookAsync(book);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPatch("book/edit")]
        public async Task<IActionResult> EditBook([FromBody] BookDTO bookToUpdate)
        {
            try
            {
                var validator = new BookDTOValidator();
                var results = validator.Validate(bookToUpdate);

                if (!results.IsValid)
                {
                    return BadRequest(results.Errors);
                }

                var result = await _iReadingJournalService.EditBookAsync(bookToUpdate.Id, bookToUpdate);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("book/{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            try
            {
                var result = await _iReadingJournalService.DeleteBookAsync(id);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("authors/getall")]
        public async Task<IActionResult> GetAllAuthors()
        {
            try
            {
                var result = await _iReadingJournalService.GetAllAuthorsAsync();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("author/create")]
        public async Task<IActionResult> CreateAuthor([FromBody] AuthorDTO authorDto)
        {
            try
            {
                var validator = new AuthorDTOValidator();
                var results = validator.Validate(authorDto);

                if (!results.IsValid)
                {
                    return BadRequest(results.Errors);
                }

                var result = await _iReadingJournalService.CreateAuthorAsync(authorDto);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("series/getall")]
        public async Task<IActionResult> GetAllSeries()
        {
            try
            {
                var result = await _iReadingJournalRepository.GetAllSeriesAsync();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("series/create")]
        public async Task<IActionResult> CreateSeries([FromBody] string name)
        {
            try
            {
                var result = await _iReadingJournalService.CreateSeriesAsync(name);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
