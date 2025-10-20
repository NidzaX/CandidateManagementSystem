using Bogus;
using CandidateManagementSystem.Domain.JobCandidates;
using CandidateManagementSystem.Domain.Skills;
using CandidateManagementSystem.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace CandidateManagementSystem.Api.Extensions;

public class DataSeeder
{
    private readonly ApplicationDbContext _context;

    public DataSeeder(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task SeedAsync()
    {
        bool hasData = await _context.Set<Skill>().AnyAsync() ||
                       await _context.Set<JobCandidate>().AnyAsync();

        if (hasData)
        {
            Console.WriteLine("Database already contains data. Skipping seeding.");
            return;
        }

        Console.WriteLine("Seeding database...");
        await SeedSkillsAsync();
        await SeedJobCandidatesAsync();
        Console.WriteLine("Database seeded successfully!");
    }

    private async Task SeedSkillsAsync()
    {
        if (await _context.Set<Skill>().AnyAsync())
            return;

        string[] skillNames = new[]
        {
            "Java Programming", "C# Programming", "Python", "JavaScript", "TypeScript",
            "React", "Angular", "Vue.js", "Node.js", "ASP.NET Core",
            "SQL", "PostgreSQL", "MongoDB", "Database Design", "Entity Framework",
            "Docker", "Kubernetes", "Azure", "AWS", "Git",
            "English", "German", "French", "Russian", "Spanish",
            "Machine Learning", "Data Analysis", "DevOps", "CI/CD", "REST APIs"
        };

        List<Skill> skills = skillNames.Select(name => Skill.Create(new Name(name))).ToList();
        
        await _context.Set<Skill>().AddRangeAsync(skills);
        await _context.SaveChangesAsync();
    }

    private async Task SeedJobCandidatesAsync()
    {
        if (await _context.Set<JobCandidate>().AnyAsync())
            return;

        List<Skill> skills = await _context.Set<Skill>().ToListAsync();

        Faker<JobCandidate> candidateFaker = new Faker<JobCandidate>()
            .CustomInstantiator(f =>
            {
                string rawPhone = f.Phone.PhoneNumber();
                string cleanPhone = new string(rawPhone.Where(char.IsDigit).ToArray());
                if (cleanPhone.Length > 20)
                    cleanPhone = cleanPhone.Substring(0, 20);

                return JobCandidate.Create(
                    new FirstName(f.Name.FirstName()),
                    new LastName(f.Name.LastName()),
                    DateTime.SpecifyKind(f.Date.Past(30, DateTime.UtcNow.AddYears(-18)), DateTimeKind.Utc),
                    new ContactNumber(cleanPhone),
                    new Email(f.Internet.Email())
                );
            })
            .FinishWith((f, candidate) =>
            {
                IList<Skill> randomSkills = f.Random.ListItems(skills, f.Random.Int(1, 5));
                foreach (Skill skill in randomSkills)
                {
                    candidate.Skills.Add(skill);
                }
            });

        List<JobCandidate> candidates = candidateFaker.Generate(50);

        await _context.Set<JobCandidate>().AddRangeAsync(candidates);
        await _context.SaveChangesAsync();
    }
}