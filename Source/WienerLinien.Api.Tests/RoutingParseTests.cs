using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using WienerLinien.Api.Routing;

namespace WienerLinien.Api.Tests
{
    [TestFixture]
    public class RoutingParseTests
    {
        [Test]
        public void SimpleRoutingParseTest()
        {
            var schnittstelle = new RoutingSchnittstelle();
            bool ok = schnittstelle.ParseRoutingRequestResponse(ResponseFiles.Load(ResponseFiles.RoutingFahrtoptionen8));

            Assert.That(ok, Is.EqualTo(true));
        }
    }
}
