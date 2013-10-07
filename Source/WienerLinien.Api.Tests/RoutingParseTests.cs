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
            var result = schnittstelle.ParseRoutingRequestResponse(ResponseFiles.LoadJson(ResponseFiles.RoutingFahrtoptionen8Json));

            Assert.That(result.Succeeded, Is.EqualTo(true));
        }

        [Test]
        public void SalztorbrueckeWestbahnhofTest()
        {
            var schnittstelle = new RoutingSchnittstelle();
            var result = schnittstelle.ParseRoutingRequestResponse(ResponseFiles.LoadJson(ResponseFiles.RoutingSalztorbrueckeWestbahnhofJson));

            Assert.That(result.Succeeded, Is.EqualTo(true));
            Assert.That(result.Trips.Count, Is.EqualTo(4));
        }

        [Test]
        public void KleistgasseWaidhausenstrasseTest()
        {
            var schnittstelle = new RoutingSchnittstelle();
            var result = schnittstelle.ParseRoutingRequestResponse(ResponseFiles.LoadJson(ResponseFiles.RoutingKleistgasseWaidhausenstrasseJson));

            Assert.That(result.Succeeded, Is.EqualTo(true));
            Assert.That(result.Trips.Count, Is.EqualTo(8));
        }
    }
}
