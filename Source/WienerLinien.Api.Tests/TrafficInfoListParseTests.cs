using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using WienerLinien.Api.Ogd;

namespace WienerLinien.Api.Tests
{
    [TestFixture]
    public class TrafficInfoListParseTests
    {
        [Test]
        public void StoerunglangTest()
        {
            var schnittstelle = new EchtzeitdatenSchnittstelle();
            TrafficInformation result = schnittstelle.ParseTrafficInfoListResponse(ResponseFiles.LoadJson(ResponseFiles.Stoerunglang));

            Assert.That(result.Succeeded, Is.EqualTo(true));
            Assert.That(result.Items.Count, Is.EqualTo(7));
        }
    }
}
