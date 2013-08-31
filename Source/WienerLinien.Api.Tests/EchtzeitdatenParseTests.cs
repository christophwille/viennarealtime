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
    public class EchtzeitdatenParseTests
    {
        [Test]
        public void InvalidKeyTest()
        {
            var schnittstelle = new EchtzeitdatenSchnittstelle();
            MonitorInformation result = schnittstelle.ParseMonitorResponse(ResponseFiles.LoadJson(ResponseFiles.InvalidKeyResponse));

            Assert.That(result.ErrorCode, Is.EqualTo(MonitorInformationErrorCode.ServerAuthenticationFailed));
        }

        [Test]
        public void EmptyResponseTest()
        {
            var schnittstelle = new EchtzeitdatenSchnittstelle();
            MonitorInformation result = schnittstelle.ParseMonitorResponse(ResponseFiles.LoadJson(ResponseFiles.EmptyOkResponse));

            Assert.That(result.ErrorCode, Is.EqualTo(MonitorInformationErrorCode.MonitorsEmpty));
        }

        [Test]
        public void RBL2170HubertusdammTest()
        {
            var schnittstelle = new EchtzeitdatenSchnittstelle();
            MonitorInformation result = schnittstelle.ParseMonitorResponse(ResponseFiles.LoadJson(ResponseFiles.RBL2170Hubertusdamm));

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
