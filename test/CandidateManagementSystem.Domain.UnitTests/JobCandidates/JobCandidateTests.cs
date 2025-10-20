using CandidateManagementSystem.Domain.JobCandidates;
using CandidateManagementSystem.Domain.JobCandidates.Events;
using CandidateManagementSystem.Domain.UnitTests.Infrastructure;
using FluentAssertions;

namespace CandidateManagementSystem.Domain.UnitTests.JobCandidates;

public class JobCandidateTests : BaseTest
{
    [Fact]
    public void Create_Should_SetPropertyValues()
    {
        // Act
        JobCandidate jobCandidate = JobCandidate.Create(
            JobCandidateData.FirstName,
            JobCandidateData.LastName,
            JobCandidateData.Birth,
            JobCandidateData.ContactNumber,
            JobCandidateData.Email);
        
        // Assert
        jobCandidate.FirstName.Should().Be(JobCandidateData.FirstName);
        jobCandidate.LastName.Should().Be(JobCandidateData.LastName);
        jobCandidate.Birth.Should().Be(JobCandidateData.Birth);
        jobCandidate.ContactNumber.Should().Be(JobCandidateData.ContactNumber);
        jobCandidate.Email.Should().Be(JobCandidateData.Email);
    }

    [Fact]
    public void Create_Should_RaiseJobCandidateCreatedDomainEvent()
    {
        // Act
        JobCandidate jobCandidate = JobCandidate.Create(
            JobCandidateData.FirstName,
            JobCandidateData.LastName,
            JobCandidateData.Birth,
            JobCandidateData.ContactNumber,
            JobCandidateData.Email);
        
        // Assert
        JobCandidateCreatedDomainEvent domainEvent = AssertDomainEventWasPublished<JobCandidateCreatedDomainEvent>(jobCandidate);
        domainEvent.JobCandidateId.Should().Be(jobCandidate.Id);
    }

    [Fact]
    public void UpdatePersonalInfo_Should_UpdateAllProperties()
    {
        // Arrange
        JobCandidate jobCandidate = JobCandidate.Create(
            JobCandidateData.FirstName,
            JobCandidateData.LastName,
            JobCandidateData.Birth,
            JobCandidateData.ContactNumber,
            JobCandidateData.Email);

        FirstName newFirstName = new FirstName("UpdatedFirst");
        LastName newLastName = new LastName("UpdatedLast");
        DateTime newBirth = new DateTime(1985, 5, 15);
        ContactNumber newContactNumber = new ContactNumber("+9876543210");
        Email newEmail = new Email("updated@test.com");

        // Act
        jobCandidate.UpdatePersonalInfo(newFirstName, newLastName, newBirth, newContactNumber, newEmail);

        // Assert
        jobCandidate.FirstName.Should().Be(newFirstName);
        jobCandidate.LastName.Should().Be(newLastName);
        jobCandidate.Birth.Should().Be(newBirth);
        jobCandidate.ContactNumber.Should().Be(newContactNumber);
        jobCandidate.Email.Should().Be(newEmail);
    }

    [Fact]
    public void UpdatePersonalInfo_Should_UpdatePartialInformation()
    {
        // Arrange
        JobCandidate jobCandidate = JobCandidate.Create(
            JobCandidateData.FirstName,
            JobCandidateData.LastName,
            JobCandidateData.Birth,
            JobCandidateData.ContactNumber,
            JobCandidateData.Email);

        Email newEmail = new Email("partialupdate@test.com");
        ContactNumber newContactNumber = new ContactNumber("+1111111111");

        jobCandidate.UpdatePersonalInfo(
            JobCandidateData.FirstName, 
            JobCandidateData.LastName,  
            JobCandidateData.Birth,     
            newContactNumber,           
            newEmail);                 

        // Assert
        jobCandidate.FirstName.Should().Be(JobCandidateData.FirstName);
        jobCandidate.LastName.Should().Be(JobCandidateData.LastName);
        jobCandidate.Birth.Should().Be(JobCandidateData.Birth);
        jobCandidate.ContactNumber.Should().Be(newContactNumber);
        jobCandidate.Email.Should().Be(newEmail);
    }
}