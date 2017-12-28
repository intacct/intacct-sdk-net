using Intacct.SDK.Logging;
using Microsoft.Extensions.Logging;
using Xunit;

namespace Intacct.SDK.Tests
{
    public class ClientConfigTest
    {
        [Fact]
        public void ExecuteTest()
        {
            ClientConfig clientConfig = new ClientConfig();
            
            Assert.Equal(null, clientConfig.ProfileFile);
            Assert.Equal(null, clientConfig.ProfileName);
            Assert.Equal(null, clientConfig.EndpointUrl);
            Assert.Equal(null, clientConfig.SenderId);
            Assert.Equal(null, clientConfig.SenderPassword);
            Assert.Equal(null, clientConfig.SessionId);
            Assert.Equal(null, clientConfig.CompanyId);
            Assert.Equal(null, clientConfig.UserId);
            Assert.Equal(null, clientConfig.UserPassword);
            Assert.Equal(null, clientConfig.Credentials);
            Assert.Equal(null, clientConfig.Logger);
            Assert.Equal(LogLevel.Debug, clientConfig.LogLevel);
            Assert.IsType<MessageFormatter>(clientConfig.LogMessageFormatter);
        }
    }
}