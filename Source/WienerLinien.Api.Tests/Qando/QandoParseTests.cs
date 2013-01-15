using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using WienerLinien.Api.Qando;

namespace WienerLinien.Api.Tests.Qando
{
    [TestFixture]
    public class QandoParseTests
    {
        [Test]
        public void TestValidPratersternFromDocumentation()
        {
            var schnittstelle = new EchtzeitdatenSchnittstelle();

            MonitorInformation result = schnittstelle.ParseMonitorResponse(QandoResponseTestStrings.PratersternFromDocumentation);

            Assert.That(result.Lines.Count, Is.EqualTo(24));
        }

        [Test]
        public void TestUmkehrfahrt()
        {
            var schnittstelle = new EchtzeitdatenSchnittstelle();

            MonitorInformation result = schnittstelle.ParseMonitorResponse(QandoResponseTestStrings.HeiligenstadtUmkehrfahrt);

            Assert.That(result.Lines.Count, Is.EqualTo(0));
        }

        private const string EmptyButValidResponse =
            @"<?xml version=""1.0"" encoding=""UTF-8""?>
                <ft>
                  <response>
                    <client device="""" appName="""" clientId=""123"" appVersion="""" />
                    <responseType>api_get_monitor</responseType>
                    <responseTime>2013-01-13 13:25:55</responseTime>
                    <monitor id="""" />
                    <message messageCode=""1"">ok</message>
                  </response>
                </ft>";
    }
}
