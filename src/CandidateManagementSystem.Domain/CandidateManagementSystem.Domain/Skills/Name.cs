namespace CandidateManagementSystem.Domain.Skills;

public record Name(string Value)
{
    public virtual bool Equals(Name other)
    {
        return other != null && Value.Equals(other.Value, StringComparison.OrdinalIgnoreCase);
    }

    public override int GetHashCode()
    {
        return Value.ToLowerInvariant().GetHashCode();
    }
}