using System.Data;

namespace CandidateManagementSystem.Application.Abstractions.Data;

public interface ISqlConnectionFactory
{
    IDbConnection CreateConnection();
}