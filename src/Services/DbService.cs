using Blazorbin.Data;
using MongoDB.Driver;

namespace Blazorbin.Services;

public class DbService
{
    private readonly IMongoDatabase _db;
    private readonly IMongoCollection<PastaBin> _pastaBins;
    private readonly MongoClient _client;
    
    public DbService()
    {
        _client = new MongoClient("mongodb://localhost:27017");
        _db = _client.GetDatabase("Pasta");
        _pastaBins = _db.GetCollection<PastaBin>("PastaBins");
    }
    
    public async Task<PastaBin> GetPasta(string id)
    {
        var filter = Builders<PastaBin>.Filter.Eq("Id", id);
        var result = await _pastaBins.FindAsync(filter);
        return result.FirstOrDefault();
    }
    
    public async Task<PastaBin> CreatePasta(PastaBin pastaBin)
    {
        await _pastaBins.InsertOneAsync(pastaBin);
        return pastaBin;
    }
    
    public async Task<PastaBin> UpdatePasta(PastaBin pastaBin)
    {
        var filter = Builders<PastaBin>.Filter.Eq("Id", pastaBin.Id);
        await _pastaBins.ReplaceOneAsync(filter, pastaBin);
        return pastaBin;
    }
    
    public async Task DeletePasta(string id)
    {
        var filter = Builders<PastaBin>.Filter.Eq("Id", id);
        await _pastaBins.DeleteOneAsync(filter);
    }
}