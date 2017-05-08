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
using System;
using System.IO;

namespace Intacct.Sdk.Credentials
{

    public class ProfileCredentialProvider
    {

        const string DefaultProfileFile = "/.intacct/credentials.ini";

        const string DefaultProfileName = "default";
        
        private KeyDataCollection GetIniProfileData(SdkConfig config)
        {
            if (String.IsNullOrWhiteSpace(config.ProfileName))
            {
                config.ProfileName = DefaultProfileName;
            }
            if (String.IsNullOrWhiteSpace(config.ProfileFile))
            {
                config.ProfileFile = GetHomeDirProfile();
            }

            var parser = new FileIniDataParser();
            IniData data = parser.ReadFile(config.ProfileFile);
            
            KeyDataCollection sectionData = data[config.ProfileName];
            if (sectionData == null)
            {
                throw new ArgumentException("Profile name \"" + config.ProfileName + "\" not found in credentials file");
            }

            return sectionData;
        }

        public SdkConfig GetLoginCredentials(SdkConfig config)
        {
            KeyDataCollection data = GetIniProfileData(config);
            SdkConfig loginCreds = new SdkConfig();

            if (!String.IsNullOrWhiteSpace(data["company_id"]))
            {
                loginCreds.CompanyId = data["company_id"];
            }
            if (!String.IsNullOrWhiteSpace(data["user_id"]))
            {
                loginCreds.UserId = data["user_id"];
            }
            if (!String.IsNullOrWhiteSpace(data["user_password"]))
            {
                loginCreds.UserPassword = data["user_password"];
            }

            return loginCreds;
        }

        public SdkConfig GetSenderCredentials(SdkConfig config)
        {
            KeyDataCollection data = GetIniProfileData(config);
            SdkConfig senderCreds = new SdkConfig();

            if (!String.IsNullOrWhiteSpace(data["sender_id"]))
            {
                senderCreds.SenderId = data["sender_id"];
            }
            if (!String.IsNullOrWhiteSpace(data["sender_password"]))
            {
                senderCreds.SenderPassword = data["sender_password"];
            }
            if (!String.IsNullOrWhiteSpace(data["endpoint_url"]))
            {
                senderCreds.EndpointUrl = data["endpoint_url"];
            }

            return senderCreds;
        }

        private string GetHomeDirProfile()
        {
            string homeDir = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

            string profile = Path.Combine(homeDir, DefaultProfileFile);

            return profile;
        }
    }
}