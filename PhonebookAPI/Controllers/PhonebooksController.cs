using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhonebookAPI.Models;
using PhonebookAPI.Services;

namespace PhonebookAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhonebooksController : ControllerBase
    {
        private readonly PhonebookService _phoneBooksService;

        public PhonebooksController(PhonebookService phoneBooksService) =>
            _phoneBooksService = phoneBooksService;

        [HttpGet]
        public async Task<List<Phonebook>> GetPhoneBooks() =>
            await _phoneBooksService.GetAllPhoneBooksAsync();

        [HttpGet("{name}")]
        public async Task<ActionResult<Phonebook>> GetPhoneBook(string name)
        {
            var phonebook = await _phoneBooksService.GetPhonebookAsync(name);

            if (phonebook == null)
            {
                return NotFound();
            }

            return phonebook;
        }

        [HttpPost("create")]
        public async Task<ActionResult> AddPhoneBook(Phonebook newPhoneBook)
        {
            await _phoneBooksService.CreateNewPhoneBookAsync(newPhoneBook);

            return Created("", newPhoneBook);
        }

        [HttpPut("addentry")]
        public async Task<ActionResult> AddPhoneBookEntry(string phoneBookName, Entries entry)
        {
            var phoneBook = await _phoneBooksService.GetPhonebookAsync(phoneBookName);

            if (phoneBook == null)
            {
                return NotFound();
            }

            phoneBook.Entries.Add(entry);

            await _phoneBooksService.AddPhoneBookEntryAsync(phoneBookName, phoneBook);

            return NoContent();
        }

        [HttpGet("findentries")]
        public async Task<ActionResult> GetEntry(string phoneBookName, string entryName)
        {
            var phonebook = await _phoneBooksService.GetPhonebookAsync(phoneBookName);

            if (phonebook == null)
            {
                return NotFound();
            }

            var entries = phonebook.Entries.FindAll(x => x.Name.Contains(entryName));

            if (entries == null)
            {
                return NotFound();
            }

            return Ok(entries);
        }
    }
}
