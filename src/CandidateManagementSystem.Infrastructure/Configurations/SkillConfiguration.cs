using CandidateManagementSystem.Domain.Skills;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CandidateManagementSystem.Infrastructure.Configurations;

internal sealed class SkillConfiguration : IEntityTypeConfiguration<Skill>
{
    public void Configure(EntityTypeBuilder<Skill> builder)
    {
        builder.ToTable("skills");
        
        builder.HasKey(skill => skill.Id);

        builder.Property(skill => skill.Name)
            .HasMaxLength(200)
            .HasConversion(name => name.Value, value => new Name(value))
            .IsRequired();

        builder.HasIndex("Name").IsUnique();
    }
}