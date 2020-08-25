/*
 * Copyright 2020 Sage Intacct, Inc.
 * 
 * Licensed under the Apache License, Version 2.0 (the "License"). You may not
 * use this file except in compliance with the License. You may obtain a copy 
 * of the License at
 * 
 * http://www.apache.org/licenses/LICENSE-2.0
 * 
 * or in the "LICENSE" file accompanying this file. This file is distributed on 
 * an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either 
 * express or implied. See the License for the specific language governing 
 * permissions and limitations under the License.
 */

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