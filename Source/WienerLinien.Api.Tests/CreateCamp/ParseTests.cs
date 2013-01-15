using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using WienerLinien.Api.CreateCamp;

namespace WienerLinien.Api.Tests.CreateCamp
{
    [TestFixture]
    public class ParseTests
    {
        [Test]
        public void TestValid1043()
        {
            var schnittstelle = new EchtzeitdatenSchnittstelle();

            MonitorInformation result = schnittstelle.ParseMonitorResponse(TestString.Haltepunkt1043);

            Assert.That(result.Lines.Count, Is.EqualTo(1));
        }
    }
}
