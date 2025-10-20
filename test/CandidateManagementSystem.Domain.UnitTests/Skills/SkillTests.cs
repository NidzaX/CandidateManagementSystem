using CandidateManagementSystem.Domain.Skills;
using CandidateManagementSystem.Domain.Skills.Events;
using CandidateManagementSystem.Domain.UnitTests.Infrastructure;
using FluentAssertions;

namespace CandidateManagementSystem.Domain.UnitTests.Skills;

public class SkillTests : BaseTest
{
    [Fact]
    public void Create_WithValidName_Should_SetProperties()
    {
        // Arrange
        Name name = SkillData.ValidName;

        // Act
        Skill skill = Skill.Create(name);

        // Assert
        skill.Name.Should().Be(name);
        skill.Id.Should().NotBe(Guid.Empty);
    }

    [Fact]
    public void Create_WithValidName_Should_RaiseSkillCreatedDomainEvent()
    {
        // Arrange
        Name name = SkillData.ValidName;

        // Act
        Skill skill = Skill.Create(name);

        // Assert
        SkillCreatedDomainEvent domainEvent = AssertDomainEventWasPublished<SkillCreatedDomainEvent>(skill);
        domainEvent.SkillId.Should().Be(skill.Id);
    }

    [Fact]
    public void Create_WithDifferentNames_Should_CreateDifferentSkills()
    {
        // Arrange
        Name name1 = SkillData.ValidName;
        Name name2 = SkillData.AnotherValidName;

        // Act
        Skill skill1 = Skill.Create(name1);
        Skill skill2 = Skill.Create(name2);

        // Assert
        skill1.Name.Should().Be(name1);
        skill2.Name.Should().Be(name2);
        skill1.Id.Should().NotBe(skill2.Id);
        skill1.Should().NotBe(skill2);
    }

    [Fact]
    public void Create_WithSpecialCharacters_Should_SetNameCorrectly()
    {
        // Arrange
        Name nameWithSpecialChars = SkillData.NameWithSpecialCharacters;

        // Act
        Skill skill = Skill.Create(nameWithSpecialChars);

        // Assert
        skill.Name.Should().Be(nameWithSpecialChars);
        skill.Name.Value.Should().Be("c++");
    }

    [Fact]
    public void Create_WithLongName_Should_SetNameCorrectly()
    {
        // Arrange
        Name longName = SkillData.LongName;

        // Act
        Skill skill = Skill.Create(longName);

        // Assert
        skill.Name.Should().Be(longName);
        skill.Name.Value.Should().Be("asp.net core web api development");
    }

    [Fact]
    public void Create_Should_GenerateUniqueIds()
    {
        // Arrange
        Name name = SkillData.ValidName;

        // Act
        Skill skill1 = Skill.Create(name);
        Skill skill2 = Skill.Create(name);

        // Assert
        skill1.Id.Should().NotBe(skill2.Id);
        skill1.Name.Should().Be(skill2.Name); 
    }

    [Fact]
    public void Equals_WithSameName_Should_ReturnTrue()
    {
        // Arrange
        Name name = SkillData.ValidName;
        Skill skill1 = Skill.Create(name);
        Skill skill2 = Skill.Create(name);

        // Act & Assert
        skill1.Equals(skill2).Should().BeTrue();
        (skill1 == skill2).Should().BeFalse(); 
    }

    [Fact]
    public void Equals_WithDifferentName_Should_ReturnFalse()
    {
        // Arrange
        Skill skill1 = Skill.Create(SkillData.ValidName);
        Skill skill2 = Skill.Create(SkillData.AnotherValidName);

        // Act & Assert
        skill1.Equals(skill2).Should().BeFalse();
    }

    [Fact]
    public void GetHashCode_WithSameName_Should_ReturnSameValue()
    {
        // Arrange
        Name name = SkillData.ValidName;
        Skill skill1 = Skill.Create(name);
        Skill skill2 = Skill.Create(name);

        // Act & Assert
        skill1.GetHashCode().Should().Be(skill2.GetHashCode());
    }

    [Fact]
    public void GetHashCode_WithDifferentName_Should_ReturnDifferentValue()
    {
        // Arrange
        Skill skill1 = Skill.Create(SkillData.ValidName);
        Skill skill2 = Skill.Create(SkillData.AnotherValidName);

        // Act & Assert
        skill1.GetHashCode().Should().NotBe(skill2.GetHashCode());
    }
}