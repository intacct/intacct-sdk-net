/*
 * Copyright 2022 Sage Intacct, Inc.
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

using System;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace Intacct.SDK.Credentials
{
    public static class ProfileCredentialProvider
    {

        public const string DefaultProfileFile = "/.intacct/credentials.ini";

        public const string DefaultProfileName = "default";

        public static ClientConfig GetLoginCredentials(ClientConfig config)
        {
            ClientConfig creds = new ClientConfig();
            if (!ProfileFileExists(config)) return creds;
            
            IConfigurationSection data = GetIniProfileData(config);

            string companyId = data.GetSection("company_id").Value;
            string entityId = data.GetSection("entity_id").Value;
            string userId = data.GetSection("user_id").Value;
            string userPassword = data.GetSection("user_password").Value;
            
            if (!string.IsNullOrEmpty(companyId))
            {
                creds.CompanyId = companyId;
            }

            if (!string.IsNullOrEmpty(entityId))
            {
                creds.EntityId = entityId;
            }
            if (!string.IsNullOrEmpty(userId))
            {
                creds.UserId = userId;
            }
            if (!string.IsNullOrEmpty(userPassword))
            {
                creds.UserPassword = userPassword;
            }

            return creds;
        }

        public static ClientConfig GetSenderCredentials(ClientConfig config)
        {
            ClientConfig creds = new ClientConfig();
            if (!ProfileFileExists(config)) return creds;
            
            IConfigurationSection data = GetIniProfileData(config);

            string senderId = data.GetSection("sender_id").Value;
            string senderPassword = data.GetSection("sender_password").Value;
            string endpointUrl = data.GetSection("endpoint_url").Value;

            if (!string.IsNullOrEmpty(senderId))
            {
                creds.SenderId = senderId;
            }
            if (!string.IsNullOrEmpty(senderPassword))
            {
                creds.SenderPassword = senderPassword;
            }
            if (!string.IsNullOrEmpty(endpointUrl))
            {
                creds.EndpointUrl = endpointUrl;
            }

            return creds;
        }

        public static string GetHomeDirProfile()
        {
            string profile = "";
            string homeDir = Environment.GetEnvironmentVariable("HOME");

            if (!string.IsNullOrEmpty(homeDir))
            {
                // Linux/Unix
                profile = Path.Combine(homeDir, DefaultProfileFile);
            }
            else
            {
                // Windows
                string homeDrive = Environment.GetEnvironmentVariable("HOMEDRIVE");
                string homePath = Environment.GetEnvironmentVariable("HOMEPATH");
                if (!string.IsNullOrEmpty(homeDrive) && !string.IsNullOrEmpty(homePath))
                {
                    profile = Path.Combine(homeDrive, homePath, DefaultProfileFile);
                }
            }
            
            return profile;
        }

        private static bool ProfileFileExists(ClientConfig config)
        {
            if (string.IsNullOrEmpty(config.ProfileFile))
            {
                config.ProfileFile = GetHomeDirProfile();
            }
            
            return File.Exists(config.ProfileFile);
        }
        
        private static IConfigurationSection GetIniProfileData(ClientConfig config)
        {
            if (string.IsNullOrEmpty(config.ProfileName))
            {
                config.ProfileName = ProfileCredentialProvider.DefaultProfileName;
            }
            
            ConfigurationBuilder builder = new ConfigurationBuilder();
            builder.SetBasePath(Path.GetDirectoryName(config.ProfileFile));
            builder.AddIniFile(Path.GetFileName(config.ProfileFile));

            IConfigurationRoot data = builder.Build();

            IConfigurationSection section = data.GetSection(config.ProfileName);
            
            if (!section.GetChildren().Any())
            {
                throw new ArgumentException("Profile name \"" + config.ProfileName + "\" not found in credentials file");
            }

            return section;
        }
    }
}