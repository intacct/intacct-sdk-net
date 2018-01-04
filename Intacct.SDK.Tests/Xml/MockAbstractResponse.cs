using System.IO;
using Intacct.SDK.Xml;

namespace Intacct.SDK.Tests.Xml
{
    public class MockAbstractResponse : AbstractResponse
    {
        public MockAbstractResponse(Stream body) : base(body)
        {
        }
    }
}