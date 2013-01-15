using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WienerLinien.Api.Tests.CreateCamp
{
    public class TestString
    {
        public const string Haltepunkt1043 =
            @"<?xml version=""1.0"" encoding=""UTF-8"" standalone=""yes""?>
<ns2:DatenAbrufenAntwort xmlns:ns2=""vdv453ger"">
  <AZBNachricht AboID=""createcamp20"">
    <AZBFahrplanlage Zst=""2013-01-12T13:01:56"">
      <AbfahrtszeitAZBPlan>2013-01-12T13:05:30</AbfahrtszeitAZBPlan>
      <AbfahrtszeitAZBPrognose>2013-01-12T13:05:30</AbfahrtszeitAZBPrognose>
      <AnkunftszeitAZBPlan>2013-01-12T13:05:30</AnkunftszeitAZBPlan>
      <AnkunftszeitAZBPrognose>2013-01-12T13:05:30</AnkunftszeitAZBPrognose>
      <AZBID>101063</AZBID>
      <FahrtID>
        <Betriebstag>2013-01-12</Betriebstag>
        <FahrtBezeichner>120113-10866008</FahrtBezeichner>
      </FahrtID>
      <FahrtInfo>
        <FahrzeugID>8078</FahrzeugID>
        <ServiceMerkmal>geeignet für mobilitätseingeschränkte Fahrgäste</ServiceMerkmal>
      </FahrtInfo>
      <FahrtStatus>Ist</FahrtStatus>
      <HstSeqZaehler>26</HstSeqZaehler>
      <LinienID>415</LinienID>
      <LinienText>15A</LinienText>
      <RichtungsText>Enkplatz U Grillgasse</RichtungsText>
      <Stauindikator>false</Stauindikator>
      <ZielHst>Mayredergasse</ZielHst>
    </AZBFahrplanlage>
    <AZBFahrplanlage Zst=""2013-01-12T13:00:45"">
      <AbfahrtszeitAZBPlan>2013-01-12T13:05:30</AbfahrtszeitAZBPlan>
      <AbfahrtszeitAZBPrognose>2013-01-12T13:05:38</AbfahrtszeitAZBPrognose>
      <AnkunftszeitAZBPlan>2013-01-12T13:05:30</AnkunftszeitAZBPlan>
      <AnkunftszeitAZBPrognose>2013-01-12T13:05:38</AnkunftszeitAZBPrognose>
      <AZBID>101063</AZBID>
      <FahrtID>
        <Betriebstag>2013-01-12</Betriebstag>
        <FahrtBezeichner>120113-10866008</FahrtBezeichner>
      </FahrtID>
      <FahrtInfo>
        <FahrzeugID>8078</FahrzeugID>
        <ServiceMerkmal>geeignet für mobilitätseingeschränkte Fahrgäste</ServiceMerkmal>
      </FahrtInfo>
      <FahrtStatus>Ist</FahrtStatus>
      <HstSeqZaehler>26</HstSeqZaehler>
      <LinienID>415</LinienID>
      <LinienText>15A</LinienText>
      <RichtungsText>Enkplatz U Grillgasse</RichtungsText>
      <Stauindikator>false</Stauindikator>
      <ZielHst>Mayredergasse</ZielHst>
    </AZBFahrplanlage>
    <AZBFahrplanlage Zst=""2013-01-12T13:00:45"">
      <AbfahrtszeitAZBPlan>2013-01-12T13:15:30</AbfahrtszeitAZBPlan>
      <AbfahrtszeitAZBPrognose>2013-01-12T13:16:46</AbfahrtszeitAZBPrognose>
      <AnkunftszeitAZBPlan>2013-01-12T13:15:30</AnkunftszeitAZBPlan>
      <AnkunftszeitAZBPrognose>2013-01-12T13:16:46</AnkunftszeitAZBPrognose>
      <AZBID>101063</AZBID>
      <FahrtID>
        <Betriebstag>2013-01-12</Betriebstag>
        <FahrtBezeichner>120113-10866004</FahrtBezeichner>
      </FahrtID>
      <FahrtInfo>
        <FahrzeugID>8094</FahrzeugID>
        <ServiceMerkmal>geeignet für mobilitätseingeschränkte Fahrgäste</ServiceMerkmal>
      </FahrtInfo>
      <FahrtStatus>Ist</FahrtStatus>
      <HstSeqZaehler>26</HstSeqZaehler>
      <LinienID>415</LinienID>
      <LinienText>15A</LinienText>
      <RichtungsText>Enkplatz U Grillgasse</RichtungsText>
      <Stauindikator>false</Stauindikator>
      <ZielHst>Mayredergasse</ZielHst>
    </AZBFahrplanlage>
    <AZBFahrplanlage Zst=""2013-01-12T13:01:56"">
      <AbfahrtszeitAZBPlan>2013-01-12T13:15:30</AbfahrtszeitAZBPlan>
      <AbfahrtszeitAZBPrognose>2013-01-12T13:16:54</AbfahrtszeitAZBPrognose>
      <AnkunftszeitAZBPlan>2013-01-12T13:15:30</AnkunftszeitAZBPlan>
      <AnkunftszeitAZBPrognose>2013-01-12T13:16:54</AnkunftszeitAZBPrognose>
      <AZBID>101063</AZBID>
      <FahrtID>
        <Betriebstag>2013-01-12</Betriebstag>
        <FahrtBezeichner>120113-10866004</FahrtBezeichner>
      </FahrtID>
      <FahrtInfo>
        <FahrzeugID>8094</FahrzeugID>
        <ServiceMerkmal>geeignet für mobilitätseingeschränkte Fahrgäste</ServiceMerkmal>
      </FahrtInfo>
      <FahrtStatus>Ist</FahrtStatus>
      <HstSeqZaehler>26</HstSeqZaehler>
      <LinienID>415</LinienID>
      <LinienText>15A</LinienText>
      <RichtungsText>Enkplatz U Grillgasse</RichtungsText>
      <Stauindikator>false</Stauindikator>
      <ZielHst>Mayredergasse</ZielHst>
    </AZBFahrplanlage>
    <AZBFahrplanlage Zst=""2013-01-12T13:01:56"">
      <AbfahrtszeitAZBPlan>2013-01-12T13:25:30</AbfahrtszeitAZBPlan>
      <AbfahrtszeitAZBPrognose>2013-01-12T13:27:03</AbfahrtszeitAZBPrognose>
      <AnkunftszeitAZBPlan>2013-01-12T13:25:30</AnkunftszeitAZBPlan>
      <AnkunftszeitAZBPrognose>2013-01-12T13:27:03</AnkunftszeitAZBPrognose>
      <AZBID>101063</AZBID>
      <FahrtID>
        <Betriebstag>2013-01-12</Betriebstag>
        <FahrtBezeichner>120113-10866000</FahrtBezeichner>
      </FahrtID>
      <FahrtInfo>
        <FahrzeugID>8087</FahrzeugID>
        <ServiceMerkmal>geeignet für mobilitätseingeschränkte Fahrgäste</ServiceMerkmal>
      </FahrtInfo>
      <FahrtStatus>Ist</FahrtStatus>
      <HstSeqZaehler>26</HstSeqZaehler>
      <LinienID>415</LinienID>
      <LinienText>15A</LinienText>
      <RichtungsText>Enkplatz U Grillgasse</RichtungsText>
      <Stauindikator>false</Stauindikator>
      <ZielHst>Mayredergasse</ZielHst>
    </AZBFahrplanlage>
    <AZBFahrplanlage Zst=""2013-01-12T13:00:45"">
      <AbfahrtszeitAZBPlan>2013-01-12T13:25:30</AbfahrtszeitAZBPlan>
      <AbfahrtszeitAZBPrognose>2013-01-12T13:27:11</AbfahrtszeitAZBPrognose>
      <AnkunftszeitAZBPlan>2013-01-12T13:25:30</AnkunftszeitAZBPlan>
      <AnkunftszeitAZBPrognose>2013-01-12T13:27:11</AnkunftszeitAZBPrognose>
      <AZBID>101063</AZBID>
      <FahrtID>
        <Betriebstag>2013-01-12</Betriebstag>
        <FahrtBezeichner>120113-10866000</FahrtBezeichner>
      </FahrtID>
      <FahrtInfo>
        <FahrzeugID>8087</FahrzeugID>
        <ServiceMerkmal>geeignet für mobilitätseingeschränkte Fahrgäste</ServiceMerkmal>
      </FahrtInfo>
      <FahrtStatus>Ist</FahrtStatus>
      <HstSeqZaehler>26</HstSeqZaehler>
      <LinienID>415</LinienID>
      <LinienText>15A</LinienText>
      <RichtungsText>Enkplatz U Grillgasse</RichtungsText>
      <Stauindikator>false</Stauindikator>
      <ZielHst>Mayredergasse</ZielHst>
    </AZBFahrplanlage>
    <AZBFahrplanlage Zst=""2013-01-12T13:00:45"">
      <AbfahrtszeitAZBPlan>2013-01-12T13:35:30</AbfahrtszeitAZBPlan>
      <AbfahrtszeitAZBPrognose>2013-01-12T13:36:20</AbfahrtszeitAZBPrognose>
      <AnkunftszeitAZBPlan>2013-01-12T13:35:30</AnkunftszeitAZBPlan>
      <AnkunftszeitAZBPrognose>2013-01-12T13:36:20</AnkunftszeitAZBPrognose>
      <AZBID>101063</AZBID>
      <FahrtID>
        <Betriebstag>2013-01-12</Betriebstag>
        <FahrtBezeichner>120113-10865997</FahrtBezeichner>
      </FahrtID>
      <FahrtInfo>
        <FahrzeugID>8090</FahrzeugID>
        <ServiceMerkmal>geeignet für mobilitätseingeschränkte Fahrgäste</ServiceMerkmal>
      </FahrtInfo>
      <FahrtStatus>Ist</FahrtStatus>
      <HstSeqZaehler>26</HstSeqZaehler>
      <LinienID>415</LinienID>
      <LinienText>15A</LinienText>
      <RichtungsText>Enkplatz U Grillgasse</RichtungsText>
      <Stauindikator>false</Stauindikator>
      <ZielHst>Mayredergasse</ZielHst>
    </AZBFahrplanlage>
    <AZBFahrplanlage Zst=""2013-01-12T13:01:56"">
      <AbfahrtszeitAZBPlan>2013-01-12T13:35:30</AbfahrtszeitAZBPlan>
      <AbfahrtszeitAZBPrognose>2013-01-12T13:36:59</AbfahrtszeitAZBPrognose>
      <AnkunftszeitAZBPlan>2013-01-12T13:35:30</AnkunftszeitAZBPlan>
      <AnkunftszeitAZBPrognose>2013-01-12T13:36:59</AnkunftszeitAZBPrognose>
      <AZBID>101063</AZBID>
      <FahrtID>
        <Betriebstag>2013-01-12</Betriebstag>
        <FahrtBezeichner>120113-10865997</FahrtBezeichner>
      </FahrtID>
      <FahrtInfo>
        <FahrzeugID>8090</FahrzeugID>
        <ServiceMerkmal>geeignet für mobilitätseingeschränkte Fahrgäste</ServiceMerkmal>
      </FahrtInfo>
      <FahrtStatus>Ist</FahrtStatus>
      <HstSeqZaehler>26</HstSeqZaehler>
      <LinienID>415</LinienID>
      <LinienText>15A</LinienText>
      <RichtungsText>Enkplatz U Grillgasse</RichtungsText>
      <Stauindikator>false</Stauindikator>
      <ZielHst>Mayredergasse</ZielHst>
    </AZBFahrplanlage>
    <AZBFahrplanlage Zst=""2013-01-12T13:01:56"">
      <AbfahrtszeitAZBPlan>2013-01-12T13:45:30</AbfahrtszeitAZBPlan>
      <AbfahrtszeitAZBPrognose>2013-01-12T13:45:30</AbfahrtszeitAZBPrognose>
      <AnkunftszeitAZBPlan>2013-01-12T13:45:30</AnkunftszeitAZBPlan>
      <AnkunftszeitAZBPrognose>2013-01-12T13:45:30</AnkunftszeitAZBPrognose>
      <AZBID>101063</AZBID>
      <FahrtID>
        <Betriebstag>2013-01-12</Betriebstag>
        <FahrtBezeichner>120113-10864940</FahrtBezeichner>
      </FahrtID>
      <FahrtInfo>
        <FahrzeugID>8074</FahrzeugID>
        <ServiceMerkmal>geeignet für mobilitätseingeschränkte Fahrgäste</ServiceMerkmal>
      </FahrtInfo>
      <FahrtStatus>Ist</FahrtStatus>
      <HstSeqZaehler>26</HstSeqZaehler>
      <LinienID>415</LinienID>
      <LinienText>15A</LinienText>
      <RichtungsText>Enkplatz U Grillgasse</RichtungsText>
      <Stauindikator>false</Stauindikator>
      <ZielHst>Mayredergasse</ZielHst>
    </AZBFahrplanlage>
    <AZBFahrplanlage Zst=""2013-01-12T13:01:56"">
      <AbfahrtszeitAZBPlan>2013-01-12T13:55:30</AbfahrtszeitAZBPlan>
      <AbfahrtszeitAZBPrognose>2013-01-12T13:55:30</AbfahrtszeitAZBPrognose>
      <AnkunftszeitAZBPlan>2013-01-12T13:55:30</AnkunftszeitAZBPlan>
      <AnkunftszeitAZBPrognose>2013-01-12T13:55:30</AnkunftszeitAZBPrognose>
      <AZBID>101063</AZBID>
      <FahrtID>
        <Betriebstag>2013-01-12</Betriebstag>
        <FahrtBezeichner>120113-10864935</FahrtBezeichner>
      </FahrtID>
      <FahrtInfo>
        <FahrzeugID>8071</FahrzeugID>
        <ServiceMerkmal>geeignet für mobilitätseingeschränkte Fahrgäste</ServiceMerkmal>
      </FahrtInfo>
      <FahrtStatus>Ist</FahrtStatus>
      <HstSeqZaehler>26</HstSeqZaehler>
      <LinienID>415</LinienID>
      <LinienText>15A</LinienText>
      <RichtungsText>Enkplatz U Grillgasse</RichtungsText>
      <Stauindikator>false</Stauindikator>
      <ZielHst>Mayredergasse</ZielHst>
    </AZBFahrplanlage>
    <AZBFahrplanlage Zst=""2013-01-12T13:01:56"">
      <AbfahrtszeitAZBPlan>2013-01-12T14:05:30</AbfahrtszeitAZBPlan>
      <AbfahrtszeitAZBPrognose>2013-01-12T14:05:30</AbfahrtszeitAZBPrognose>
      <AnkunftszeitAZBPlan>2013-01-12T14:05:30</AnkunftszeitAZBPlan>
      <AnkunftszeitAZBPrognose>2013-01-12T14:05:30</AnkunftszeitAZBPrognose>
      <AZBID>101063</AZBID>
      <FahrtID>
        <Betriebstag>2013-01-12</Betriebstag>
        <FahrtBezeichner>120113-10864929</FahrtBezeichner>
      </FahrtID>
      <FahrtInfo>
        <FahrzeugID>8092</FahrzeugID>
        <ServiceMerkmal>geeignet für mobilitätseingeschränkte Fahrgäste</ServiceMerkmal>
      </FahrtInfo>
      <FahrtStatus>Ist</FahrtStatus>
      <HstSeqZaehler>26</HstSeqZaehler>
      <LinienID>415</LinienID>
      <LinienText>15A</LinienText>
      <RichtungsText>Enkplatz U Grillgasse</RichtungsText>
      <Stauindikator>false</Stauindikator>
      <ZielHst>Mayredergasse</ZielHst>
    </AZBFahrplanlage>
  </AZBNachricht>
  <Bestaetigung Zst=""2013-01-12T13:02:27"" Ergebnis=""ok""/>
</ns2:DatenAbrufenAntwort>";
    }
}
