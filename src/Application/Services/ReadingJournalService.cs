using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enums.ReadingJournal;

namespace Application.Services
{
    public class ReadingJournalService : IReadingJournalService
    {
        private readonly IReadingJournalRepository _readingJournalRepository;
        public ReadingJournalService(IReadingJournalRepository readingJournalRepository)
        {
            _readingJournalRepository = readingJournalRepository;
        }

        public async Task<List<BookDTO>> GetAllBooksAsync()
        {
            var books = await _readingJournalRepository.GetAllBooksAsync();

            return books.Select(b => new BookDTO
            {
                Id = b.Id,
                Name = b.Name,
                Format = b.Format,
                Genres = b.Genres,
                Pages = b.Pages,
                Publisher = b.Publisher,
                Status = b.Status,
                CurrentPage = b.CurrentPage,
                Description = b.Description,
                Price = b.Price,
                Review = b.Review,
                Series = b.Series,
                Authors = b.Authors.Select(a => new AuthorDTO
                {
                    Id = a.Id,
                    Name = a.Name,
                }).ToArray(),
                StartDate = b.StartDate.HasValue
                    ? b.StartDate.Value.ToDateTime(TimeOnly.MinValue)
                    : (DateTime?)null,
                EndDate = b.EndDate.HasValue
                    ? b.EndDate.Value.ToDateTime(TimeOnly.MinValue)
                    : (DateTime?)null
            }).ToList();
        }

        public async Task<bool> CreateBookAsync(BookDTO dto)
        {
            var authors = await _readingJournalRepository.FindAuthorsById(dto.Authors.Select(a => a.Id).ToList());

            var series = dto.Series != null
                ? await _readingJournalRepository.FindSeriesById(dto.Series.Id)
                : null;

            var model = new Book
            {
                Name = dto.Name,
                Authors = authors,
                Pages = dto.Pages,
                CurrentPage = dto.CurrentPage,
                StartDate = dto.StartDate.HasValue
                    ? DateOnly.FromDateTime(dto.StartDate.Value)
                    : (DateOnly?)null,
                EndDate = dto.EndDate.HasValue
                    ? DateOnly.FromDateTime(dto.EndDate.Value)
                    : (DateOnly?)null,
                Genres = dto.Genres,
                Publisher = dto.Publisher,
                Format = dto.Format,
                Description = dto.Description,
                Review = dto.Review,
                Status = dto.Status,
                Price = dto.Price,
                Series = series
            };

            var result = await _readingJournalRepository.AddBookAsync(model);

            return result != null;
        }

        public async Task<bool> EditBookAsync(int id, BookDTO dto)
        {
            var book = await _readingJournalRepository.FindBookById(id);

            if (book == null)
                return false;

            // Ao tentar inserir EndDate sem StartDate estar preenchida
            if (!book.StartDate.HasValue && dto.EndDate.HasValue)
            {
                return false;
            }

            book.Price = dto.Price;
            book.Description = dto.Description;
            book.Review = dto.Review;
            book.Genres = dto.Genres;
            book.Series = dto.Series != null
                ? await _readingJournalRepository.FindSeriesById(dto.Series.Id)
                : null;
            book.StartDate = DateOnly.FromDateTime(dto.StartDate!.Value);

            // Lógica inteligente de Finished
            bool isCurrentPageFinished = dto.CurrentPage.HasValue && dto.CurrentPage.Value == book.Pages;
            bool isStatusFinished = dto.Status == Status.Finished;
            bool isEndDateSet = dto.EndDate.HasValue;

            if (isCurrentPageFinished || isStatusFinished || isEndDateSet)
            {
                book.CurrentPage = book.Pages;
                book.Status = Status.Finished;
                book.EndDate = isEndDateSet ? DateOnly.FromDateTime(dto.EndDate!.Value) : DateOnly.FromDateTime(DateTime.Now);
            }
            else
            {
                book.CurrentPage = dto.CurrentPage ?? book.CurrentPage;
                book.Status = dto.Status;
                book.EndDate = isEndDateSet ? DateOnly.FromDateTime(dto.EndDate!.Value) : book.EndDate;
            }

            var result = await _readingJournalRepository.UpdateBookAsync(book);

            return result != null;
        }


        public async Task<bool> DeleteBookAsync(int id)
        {
            return await _readingJournalRepository.DeleteBookAsync(id);
        }

        public async Task<List<AuthorDTO>> GetAllAuthorsAsync()
        {
            var authors = await _readingJournalRepository.GetAllAuthorsAsync();

            return authors.Select(b => new AuthorDTO
            {
                Id = b.Id,
                Name = b.Name
            }).ToList();
        }

        public async Task<bool> CreateAuthorAsync(AuthorDTO dto)
        {
            var model = new Author { Name = dto.Name };

            var result = await _readingJournalRepository.AddAuthorAsync(model);

            return result != null;
        }

        public async Task<bool> CreateSeriesAsync(string name)
        {
            var model = new Series
            {
                Name = name,
            };

            var result = await _readingJournalRepository.AddSeriesAsync(model);

            return result != null;
        }
    }
}
