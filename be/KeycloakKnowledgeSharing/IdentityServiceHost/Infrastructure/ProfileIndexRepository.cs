using Dapper;
using IdentityServiceHost.DTOs;
using Npgsql;

namespace IdentityServiceHost.Infrastructure;

public class ProfileIndexRepository : IProfileIndexRepository
{
    private const string TableName = "ProfileIndex";
    
    private readonly NpgsqlConnection _connection;

    public ProfileIndexRepository(string connectionString)
    {
        _connection = new NpgsqlConnection(connectionString);
        _connection.Open();
    }

    public async Task CreateProfileIndexAsync(ProfileIndex profileIndex)
    {
        const string query = $"INSERT INTO {TableName} (Email, GlobalProfileId) VALUES (@Email, @GlobalProfileId)";
        var queryArguments = new { profileIndex.Email, profileIndex.GlobalProfileId };

        await _connection.ExecuteAsync(query, queryArguments);
    }
}