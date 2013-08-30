using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using WienerLinien.Api.Ogd;

namespace WienerLinien.Api.Tests
{
    // Tscherttegasse: http://www.wienerlinien.at/ogd_realtime/monitor?rbl=4640&rbl=4629&sender=<SENDER>

    [TestFixture]
    public class ParseTests
    {
        private string LoadJson(string filename)
        {
            return System.IO.File.ReadAllText(filename);
        }

        [Test]
        public void InvalidKeyTest()
        {
            var schnittstelle = new EchtzeitdatenSchnittstelle();
            MonitorInformation result = schnittstelle.ParseMonitorResponse(LoadJson("InvalidKeyResponse.json"));

            Assert.That(result.ErrorCode, Is.EqualTo(MonitorInformationErrorCode.ServerAuthenticationFailed));
        }

        [Test]
        public void EmptyResponseTest()
        {
            var schnittstelle = new EchtzeitdatenSchnittstelle();
            MonitorInformation result = schnittstelle.ParseMonitorResponse(LoadJson("EmptyOkResponse.json"));

            Assert.That(result.ErrorCode, Is.EqualTo(MonitorInformationErrorCode.MonitorsEmpty));
        }

        [Test]
        public void RBL2170HubertusdammTest()
        {
            var schnittstelle = new EchtzeitdatenSchnittstelle();
            MonitorInformation result = schnittstelle.ParseMonitorResponse(LoadJson("RBL2170Hubertusdamm.json"));

            Assert.That(result.Succeeded, Is.EqualTo(true));
            Assert.That(result.Lines.Count, Is.EqualTo(1));
            Assert.That(result.Lines[0].Departures.Count, Is.EqualTo(9));
        }

        //[Test]
        //  // towards="" NICHT EINSTEIGEN !""
        //public void TestUmkehrfahrt()
        //{
        //    var schnittstelle = new Api.Qando.EchtzeitdatenSchnittstelle();

        //    MonitorInformation result = schnittstelle.ParseMonitorResponse(QandoResponseTestStrings.HeiligenstadtUmkehrfahrt);

        //    Assert.That(result.Lines.Count, Is.EqualTo(0));
        //}
    }
}
