namespace CandidateManagementSystem.Domain.Skills;

public record Name(string Value)
{
    public string Value { get; } = ValidateAndNormalize(Value);

    private static string ValidateAndNormalize(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new InvalidOperationException(SkillErrors.NameEmpty.Name);
            
        string normalized = value.Trim().ToLowerInvariant();
        
        if (string.IsNullOrWhiteSpace(normalized))
            throw new InvalidOperationException(SkillErrors.NameEmpty.Name);
            
        if (normalized.Length > 200)
            throw new InvalidOperationException(SkillErrors.NameTooLong.Name);

        return normalized;
    }
}