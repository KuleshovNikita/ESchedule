using ESchedule.Business.Email;
using ESchedule.Business.Email.Client;
using ESchedule.Domain.Users;
using ESchedule.UnitTestsHelpers.Infrastructure;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Moq;

namespace ESchedule.Business.UnitTests.Email;

public class EmailServiceTests : TestBase<EmailService>
{
    private Mock<IConfiguration> _mockConfig;
    private Mock<IEmailMessageClient> _mockMessageClient;
    private EmailService _sut;

    public EmailServiceTests()
    {
        _mockConfig = new Mock<IConfiguration>();
        _mockMessageClient = new Mock<IEmailMessageClient>();

        _sut = GetNewSut();
    }

    [Fact]
    public async Task SendConfirmEmailMessage_CallsClient()
    {
        string resultEmailMessage = string.Empty;

        var userModel = new UserModel
        {
            Login = "test-login",
            Password = "test-password"
        };
        var mockConfigSection = new Mock<IConfigurationSection>();
        mockConfigSection
            .SetupGet(x => x.Value)
            .Returns("test-url");
        _mockConfig
            .Setup(x => x.GetSection("ClientUrl"))
            .Returns(mockConfigSection.Object);

        await _sut.SendConfirmEmailMessage(userModel);

        _mockMessageClient.Verify(x => x.SendEmail(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
    }

    [Fact]
    public async Task SendConfirmEmailMessage_GeneratesEmailMessage()
    {
        var emailMessage = await RunEmailMessageTestsBase();

        emailMessage.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task SendConfirmEmailMessage_EmailMessage_ContainsConfirmationURL()
    {
        var emailMessage = await RunEmailMessageTestsBase();

        emailMessage.Should().MatchRegex(".+test-url/confirmEmail/test-password*");
    }

    private async Task<string> RunEmailMessageTestsBase()
    {
        string resultEmailMessage = string.Empty;

        var userModel = new UserModel
        {
            Login = "test-login",
            Password = "test-password"
        };
        var mockConfigSection = new Mock<IConfigurationSection>();
        mockConfigSection
            .SetupGet(x => x.Value)
            .Returns("test-url");
        _mockConfig
            .Setup(x => x.GetSection("ClientUrl"))
            .Returns(mockConfigSection.Object);
        _mockMessageClient
            .Setup(x => x.SendEmail(It.IsAny<string>(), It.IsAny<string>()))
            .Callback(new InvocationAction(x => resultEmailMessage = (string)x.Arguments[0]));

        await _sut.SendConfirmEmailMessage(userModel);

        return resultEmailMessage;
    }

    protected override EmailService GetNewSut()
        => new(_mockConfig.Object, _mockMessageClient.Object);
}
