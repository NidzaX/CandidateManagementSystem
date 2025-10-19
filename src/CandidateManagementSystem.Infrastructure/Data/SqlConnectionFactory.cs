using System.Data;
using CandidateManagementSystem.Application.Abstractions.Data;
using Npgsql;

namespace CandidateManagementSystem.Infrastructure.Data;

internal sealed class SqlConnectionFactory : ISqlConnectionFactory
{
    private readonly string _connectionString;

    public SqlConnectionFactory(string connectionString)
    {
        _connectionString = connectionString;
    }
    
    public IDbConnection CreateConnection()
    {
        NpgsqlConnection connection = new(_connectionString);
        connection.Open();
        
        return connection;
    }
}