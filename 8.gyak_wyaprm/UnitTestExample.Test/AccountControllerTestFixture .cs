using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTestExample.Controllers;

namespace UnitTestExample.Test
{
    [Test]
    public void TestValidateEmail(string email, bool expectedResult)
    {
        // Arrange
        var accountController = new AccountController();

        // Act
        var actualResult = accountController.ValidateEmail(email);

        // Assert
        Assert.AreEqual(expectedResult, actualResult);

    }

    [
    Test,
    TestCase("irf@uni-corvinus.hu", "Abcd1234"),
    TestCase("irf@uni-corvinus.hu", "Abcd1234567"),
    ]
    public void TestRegisterHappyPath(string email, string password)
    {
        // Arrange
        var accountController = new AccountController();

        // Act
        var actualResult = accountController.Register(email, password);

        // Assert
        Assert.AreEqual(email, actualResult.Email);
        Assert.AreEqual(password, actualResult.Password);
        Assert.AreNotEqual(Guid.Empty, actualResult.ID);
    }

    [
    Test,
    TestCase("irf@uni-corvinus", "Abcd1234"),
    TestCase("irf.uni-corvinus.hu", "Abcd1234"),
    TestCase("irf@uni-corvinus.hu", "abcd1234"),
    TestCase("irf@uni-corvinus.hu", "ABCD1234"),
    TestCase("irf@uni-corvinus.hu", "abcdABCD"),
    TestCase("irf@uni-corvinus.hu", "Ab1234"),
    ]
    public void TestRegisterValidateException(string email, string password)
    {
        // Arrange
        var accountController = new AccountController();

        // Act
        try
        {
            var actualResult = accountController.Register(email, password);
            Assert.Fail();
        }
        catch (Exception ex)
        {
            Assert.IsInstanceOf<ValidationException>(ex);
        }

        // Assert
    }

    [
    Test,
    TestCase("irf@uni-corvinus.hu", "Abcd1234")
]
    public void TestRegisterApplicationException(string newEmail, string newPassword)
    {
        // Arrange
        var accountServiceMock = new Mock<IAccountManager>(MockBehavior.Strict);
        accountServiceMock
            .Setup(m => m.CreateAccount(It.IsAny<Account>()))
            .Throws<ApplicationException>();
        var accountController = new AccountController();
        accountController.AccountManager = accountServiceMock.Object;

        // Act
        try
        {
            var actualResult = accountController.Register(newEmail, newPassword);
            Assert.Fail();
        }
        catch (Exception ex)
        {
            Assert.IsInstanceOf<ApplicationException>(ex);
        }

        // Assert
    }
}
