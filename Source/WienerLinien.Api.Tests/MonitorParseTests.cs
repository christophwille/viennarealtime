using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using WienerLinien.Api.Realtime;

namespace WienerLinien.Api.Tests
{
    [TestFixture]
    public class MonitorParseTests
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

        // This tests towards="ALLE ZÜGE GLEIS 1 "
        [Test]
        public void U6SiebenhirtenTest()
        {
            var schnittstelle = new EchtzeitdatenSchnittstelle();
            MonitorInformation result = schnittstelle.ParseMonitorResponse(ResponseFiles.LoadJson(ResponseFiles.U6Siebenhirten));

            Assert.That(result.Succeeded, Is.EqualTo(true));
            Assert.That(result.Lines.Count, Is.EqualTo(1));
            Assert.That(result.Lines[0].Departures.Count, Is.EqualTo(2));
        }
    }
}
