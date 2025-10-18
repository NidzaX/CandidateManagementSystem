using CandidateManagementSystem.Domain.JobCandidates;
using CandidateManagementSystem.Domain.Skills;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CandidateManagementSystem.Infrastructure.Configurations;

internal sealed class JobCandidateConfiguration : IEntityTypeConfiguration<JobCandidate>
{
    public void Configure(EntityTypeBuilder<JobCandidate> builder)
    {
        builder.ToTable("job_candidates");
        
        builder.HasKey(jobCandidate => jobCandidate.Id);

        builder.Property(jobCandidate => jobCandidate.FirstName)
            .HasMaxLength(100)
            .HasConversion(firstName => firstName.Value, value => new FirstName(value));
        
        builder.Property(jobCandidate => jobCandidate.LastName)
            .HasMaxLength(100)
            .HasConversion(lastName => lastName.Value, value => new LastName(value));
        
        builder.Property(jobCandidate => jobCandidate.ContactNumber)
            .HasMaxLength(20)
            .HasConversion(contactNumber => contactNumber.Value, value => new ContactNumber(value));
        
        builder.Property(jobCandidate => jobCandidate.Email)
            .HasMaxLength(400)
            .HasConversion(email => email.Value, value => new Email(value));
        
        builder.Property(jobCandidate => jobCandidate.Birth);
        
        builder.HasIndex(jobCandidate => jobCandidate.Email).IsUnique();

        builder.HasMany(jobCandidate => jobCandidate.Skills)
            .WithMany()
            .UsingEntity<Dictionary<string, object>> (
            "job_candidate_skills",
            j => j.HasOne<Skill>()
                .WithMany()
                .HasForeignKey("SkillId"),
            j => j.HasOne<JobCandidate>()
                .WithMany()
                .HasForeignKey("JobCandidateId"),
            j =>
            {
                j.HasKey("JobCandidateId", "SkillId");
                j.ToTable("job_candidate_skills");
            });
    }
}