/*
 * Copyright 2017 Intacct Corporation.
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

using IniParser;
using IniParser.Model;
using IniParser.Parser;
using System;
using System.IO;
using System.IO.Abstractions;

namespace Intacct.Sdk.Credentials
{

    public static class ProfileCredentialProvider
    {

        const string DefaultProfileFile = "/.intacct/credentials.ini";

        const string DefaultProfileName = "default";

        public static ClientConfig GetLoginCredentials(ClientConfig config)
        {
            return ProfileCredentialProvider.getLoginCredentials(config, new FileSystem());
        }
        public static ClientConfig GetLoginCredentials(ClientConfig config, IFileSystem fileSystem)
        {
            return ProfileCredentialProvider.getLoginCredentials(config, fileSystem);
        }

        private static ClientConfig getLoginCredentials(ClientConfig config, IFileSystem fileSystem)
        {
            ClientConfig creds = new ClientConfig();
            KeyDataCollection data = getIniProfileData(config, fileSystem);
            
            if (!string.IsNullOrEmpty(data["company_id"]))
            {
                creds.CompanyId = data["company_id"];
            }
            if (!string.IsNullOrEmpty(data["user_id"]))
            {
                creds.UserId = data["user_id"];
            }
            if (!string.IsNullOrEmpty(data["user_password"]))
            {
                creds.UserPassword = data["user_password"];
            }

            return creds;
        }

        public static ClientConfig GetSenderCredentials(ClientConfig config)
        {
            return ProfileCredentialProvider.getSenderCredentials(config, new FileSystem());
        }
        public static ClientConfig GetSenderCredentials(ClientConfig config, IFileSystem fileSystem)
        {
            return ProfileCredentialProvider.getSenderCredentials(config, fileSystem);
        }

        private static ClientConfig getSenderCredentials(ClientConfig config, IFileSystem fileSystem)
        {
            ClientConfig creds = new ClientConfig();
            KeyDataCollection data = getIniProfileData(config, fileSystem);

            if (!string.IsNullOrEmpty(data["sender_id"]))
            {
                creds.SenderId = data["sender_id"];
            }
            if (!string.IsNullOrEmpty(data["sender_password"]))
            {
                creds.SenderPassword = data["sender_password"];
            }
            if (!string.IsNullOrEmpty(data["endpoint_url"]))
            {
                creds.EndpointUrl = data["endpoint_url"];
            }

            return creds;
        }

        public static string GetHomeDirProfile()
        {
            string homeDir = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

            string profile = Path.Combine(homeDir, DefaultProfileFile);

            return profile;
        }
        
        private static KeyDataCollection getIniProfileData(ClientConfig config, IFileSystem fileSystem)
        {
            if (string.IsNullOrEmpty(config.ProfileName))
            {
                config.ProfileName = ProfileCredentialProvider.DefaultProfileName;
            }
            if (string.IsNullOrEmpty(config.ProfileFile))
            {
                config.ProfileFile = ProfileCredentialProvider.GetHomeDirProfile();
            }

            string dataString = fileSystem.File.ReadAllText(config.ProfileFile);
            IniDataParser parser = new IniDataParser();
            IniData data = parser.Parse(dataString);
            
            KeyDataCollection sectionData = data[config.ProfileName];
            if (sectionData == null)
            {
                throw new ArgumentException("Profile name \"" + config.ProfileName + "\" not found in credentials file");
            }

            return sectionData;
        }
    }
}