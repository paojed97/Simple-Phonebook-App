using Microsoft.Extensions.Options;
using MongoDB.Driver;
using PhonebookAPI.Models;

namespace PhonebookAPI.Services
{
    public class PhonebookService
    {
        private readonly IMongoCollection<Phonebook> _phonebooksCollection;

        public PhonebookService(IOptions<PhoneBookDBSettings> phoneBookDBSettings)
        {
            var mongoClient = new MongoClient(
                phoneBookDBSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                phoneBookDBSettings.Value.DatabaseName);

            _phonebooksCollection = mongoDatabase.GetCollection<Phonebook>(
                phoneBookDBSettings.Value.PhonebooksCollectionName);
        }

        public async Task<List<Phonebook>> GetAllPhoneBooksAsync() =>
            await _phonebooksCollection.Find(_ => true).ToListAsync();

        public async Task<Phonebook?> GetPhonebookAsync(string name) =>
            await _phonebooksCollection.Find(x => x.Name == name).FirstOrDefaultAsync();

        public async Task CreateNewPhoneBookAsync(Phonebook phonebook) =>
            await _phonebooksCollection.InsertOneAsync(phonebook);

        public async Task AddPhoneBookEntryAsync(string name, Phonebook updatedPhonebook) =>
            await _phonebooksCollection.ReplaceOneAsync(x => x.Name == name, updatedPhonebook);
    }
}
