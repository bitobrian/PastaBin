using Blazorbin.Data;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Blazorbin.Services;

public class DbService
{
    private readonly IMongoDatabase _db;
    private readonly IMongoCollection<PastaBin> _pastaBins;
    private readonly MongoClient _client;
    
    public DbService(IOptions<DbConfig> config)
    {
        _client = new MongoClient(config.Value.ConnectionString);
        _db = _client.GetDatabase(config.Value.DatabaseName);
        _pastaBins = _db.GetCollection<PastaBin>(config.Value.CollectionName);
    }
    
    public async Task<PastaBin> GetPasta(string id)
    {
        var filter = Builders<PastaBin>.Filter.Eq("Id", id);
        var result = await _pastaBins.FindAsync(filter);
        return result.FirstOrDefault();
    }

    public async Task<List<PastaBin>> GetAllPastas()
    {
        var result = await _pastaBins.FindAsync(_ => true);
        return result.ToList();
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