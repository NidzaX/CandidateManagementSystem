using CandidateManagementSystem.Domain.JobCandidates;
using Microsoft.EntityFrameworkCore;

namespace CandidateManagementSystem.Infrastructure.Repositories;

internal sealed class JobCandidateRepository : Repository<JobCandidate>, IJobCandidateRepository
{
    public JobCandidateRepository(ApplicationDbContext dbContext) 
        : base(dbContext)
    {
    }

    public override async Task<JobCandidate?> GetByIdAsync(
        Guid id, 
        CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<JobCandidate>() 
            .Include(jc => jc.Skills)  
            .FirstOrDefaultAsync(jc => jc.Id == id, cancellationToken);
    }

   public async Task<List<JobCandidate>> SearchAsync(string? name, List<string>? skills, CancellationToken cancellationToken)
   {
       List<JobCandidate> candidates = await DbContext.Set<JobCandidate>()
           .Include(jc => jc.Skills)
           .ToListAsync(cancellationToken);
   
       IQueryable<JobCandidate> query = candidates.AsQueryable();
   
       if (!string.IsNullOrWhiteSpace(name))
       {
           string searchTerm = name.Trim().ToLower();
           query = query.Where(jc => 
               jc.FirstName.Value.ToLower().Contains(searchTerm) ||
               jc.LastName.Value.ToLower().Contains(searchTerm) ||
               (jc.FirstName.Value + " " + jc.LastName.Value).ToLower().Contains(searchTerm));
       }
   
       if (skills != null && skills.Any())
       {
           List<string> normalizedSkills = skills.Select(s => s.Trim().ToLower()).ToList();
           
           query = query.Where(jc => 
               normalizedSkills.All(requiredSkill => 
                   jc.Skills.Any(candidateSkill => 
                       candidateSkill.Name.Value.ToLower() == requiredSkill)));
       }
   
       return query.ToList();
   }
}