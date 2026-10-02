using Verkstadsloggen.Domain.Models;

namespace Verkstadsloggen.Tests;

public class PersonTests
{
    [Fact]
    public void CreatePerson_ReturnsCorrectInformation()
    {
        // Arrange
        var firstName = "Anders";
        var lastName = "Andersson";
        var address = "Testgatan 8";
        var email = "anders@test.se";
        var phoneNumber = "0701234567";

        // Act
        var customer = new Customer()
        {
            FirstName = firstName,
            LastName = lastName,
            Address = address,
            Email = email,
            PhoneNumber = phoneNumber
        };

        // Assert
        Assert.Equal(firstName, customer.FirstName);
        Assert.Equal(lastName, customer.LastName);
        Assert.Equal(address, customer.Address);
        Assert.Equal(email, customer.Email);
        Assert.Equal(phoneNumber, customer.PhoneNumber);
        
    }

    [Fact]
    public void CreatePerson_ReturnsPersonWithId()
    {
        // Arrange

        // Act
        var customer = new Customer();

        // Assert
        Assert.NotEqual(Guid.Empty, customer.Id);
    }

    [Fact]
    public void CreatePerson_ReturnsPersonsWithDifferentIds()
    {
        // Arrange

        // Act
        var customer1 = new Customer();
        var customer2 = new Customer();

        // Assert
        Assert.NotEqual(customer1.Id, customer2.Id);
        
    }
}