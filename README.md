# Intacct SDK for .NET

Please note the Intacct SDK for .NET is actively being developed.  This is a developer preview that should not be used in a production environment.

## Resources

* [Intacct][intacct] - Intacct's home page
* [Issues][sdk-issues] - Report issues with the SDK or submit pull requests
* [License][sdk-license] - Apache 2.0 license

## System Requirements

* You must have an active Intacct Web Services Developer license
* .NET Framework >4.5.2

[intacct]: http://www.intacct.com
[sdk-issues]: https://github.com/Intacct/intacct-sdk-net/issues
[sdk-license]: http://www.apache.org/licenses/LICENSE-2.0

## Quick Installation Guide

Install the SDK using NuGet

```bash
PM> Install-Package Intacct.SDK
```

## Quick Example

```c#
using Intacct.Sdk;
using Intacct.Sdk.Xml;
using Intacct.Sdk.Functions.Common;
using System;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                SdkConfig config = new SdkConfig()
                {
                    SenderId = "senderid",
                    SenderPassword = "senderpassword",
                    CompanyId = "company",
                    UserId = "user",
                    UserPassword = "pass",
                };
                IntacctClient client = new IntacctClient(config);

                Console.WriteLine("Current Company ID: " + client.SessionCreds.CurrentCompanyId);
                Console.WriteLine("Current User ID: " + client.SessionCreds.CurrentUserId);

                Read read = new Read()
                {
                    ObjectName = "VENDOR",
                };
                Content content = new Content();
                content.Add(read);

                Task<SynchronousResponse> response = client.Execute(content);

                response.Wait();

                Console.WriteLine("Read function control ID: " + response.Result.Control.ControlId);

                Console.WriteLine("Number of vendor objects read: " + response.Result.Operation.Results[0].Count);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught: " + ex.Message);
            }

            Console.ReadLine();
        }
    }
}
```
