using Intacct.SDK.Logging;
using NLog;
using Xunit;

namespace Intacct.SDK.Tests
{
    public class ClientConfigTest
    {
        [Fact]
        public void ExecuteTest()
        {
            ClientConfig clientConfig = new ClientConfig();
            
            Assert.Null(clientConfig.ProfileFile);
            Assert.Null(clientConfig.ProfileName);
            Assert.Null(clientConfig.EndpointUrl);
            Assert.Null(clientConfig.SenderId);
            Assert.Null(clientConfig.SenderPassword);
            Assert.Null(clientConfig.SessionId);
            Assert.Null(clientConfig.CompanyId);
            Assert.Null(clientConfig.EntityId);
            Assert.Null(clientConfig.UserId);
            Assert.Null(clientConfig.UserPassword);
            Assert.Null(clientConfig.Credentials);
            Assert.Null(clientConfig.Logger);
            Assert.Equal(LogLevel.Debug, clientConfig.LogLevel);
            Assert.IsType<MessageFormatter>(clientConfig.LogMessageFormatter);
        }
    }
}