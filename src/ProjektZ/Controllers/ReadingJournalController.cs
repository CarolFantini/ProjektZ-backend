using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ProjektZ.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReadingJournalController : ControllerBase
    {
        private readonly IReadingJournalRepository _iReadingJournalRepository;

        public ReadingJournalController(IReadingJournalRepository iReadingJournalRepository)
        {
            _iReadingJournalRepository = iReadingJournalRepository;
        }

        [HttpGet]
        [Route("getall-books")]
        public async Task<IActionResult> GetAllBooks()
        {
            try
            {
                var result = await _iReadingJournalRepository.GetAllBooksAsync();

                if (result.Count() <= 0)
                {
                    return StatusCode(500, "An error occurred while retrieving the books.");
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [Route("create-book")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateBook([FromBody] Book book)
        {
            try
            {
                var model = new Book
                {
                    Name = book.Name,
                    Author = book.Author,
                    Pages = book.Pages,
                    CurrentPage = book.CurrentPage,
                    StartDate = book.StartDate,
                    EndDate = book.EndDate,
                    Genre = book.Genre,
                    Language = book.Language,
                    Publisher = book.Publisher,
                    Format = book.Format,
                    Description = book.Description,
                    Rating = book.Rating,
                    Status = book.Status,
                    Price = book.Price,
                    Series = book.Series
                };

                var result = await _iReadingJournalRepository.AddBookAsync(model);

                if (!result)
                {
                    return StatusCode(500, "An error occurred while saving the book.");
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPatch]
        [Route("edit-book")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditBook([FromBody] Book bookToUpdate)
        {
            try
            {
                var model = new Book
                {
                    Name = bookToUpdate.Name,
                    Author = bookToUpdate.Author,
                    Pages = bookToUpdate.Pages,
                    CurrentPage = bookToUpdate.CurrentPage,
                    StartDate = bookToUpdate.StartDate,
                    EndDate = bookToUpdate.EndDate,
                    Genre = bookToUpdate.Genre,
                    Language = bookToUpdate.Language,
                    Publisher = bookToUpdate.Publisher,
                    Format = bookToUpdate.Format,
                    Description = bookToUpdate.Description,
                    Rating = bookToUpdate.Rating,
                    Status = bookToUpdate.Status,
                    Price = bookToUpdate.Price,
                    Series = bookToUpdate.Series
                };

                var result = await _iReadingJournalRepository.UpdateBookAsync(model);

                if (!result)
                {
                    return StatusCode(500, "An error occurred while editing the book.");
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
