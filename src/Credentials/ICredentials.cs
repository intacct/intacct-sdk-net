using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intacct.Sdk.Credentials
{

    public interface ICredentials
    {

        SenderCredentials SenderCredentials { get; set; }
    }
}
