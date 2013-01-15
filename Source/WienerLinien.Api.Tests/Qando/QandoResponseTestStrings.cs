﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WienerLinien.Api.Tests.Qando
{
    public class QandoResponseTestStrings
    {
        #region Heiligenstadt mit Umkehrfahrt

        public const string HeiligenstadtUmkehrfahrt =
            @"<?xml version=""1.0"" encoding=""UTF-8""?>
                <ft>
                  <response>
                    <client device="""" appName="""" clientId=""123"" appVersion="""" />
                    <responseType>api_get_monitor</responseType>
                    <responseTime>2013-01-14 10:02:10</responseTime>
                    <monitor id=""46653"" clientexpiration="""">
                      <request status="""" requestId="""" sessionId="""" />
                      <locationStop status=""identified"">
                        <location name=""60200491"" title=""Heiligenstadt"" municipalityId=""49000000"" municipality=""Wien"" type=""stop"" listIndex="""" coordName=""WGS84"" wgs84Lat=""16.36519"" wgs84Lon=""48.24922"" coordX="""" coordY="""" distanceMeter="""" distanceMinute="""" />
                      </locationStop>
                      <lines count=""17"">
                        <line name=""U4"" type=""ptMetro"" towards="" NICHT EINSTEIGEN !"" direction=""H"" platform="""" barrierFree=""1"" realtimeSupported=""1"">
                          <departures count=""2"">
                            <departure>
                              <departureTime timePlanned="""" timeReal="""" delay="""" countdown="""" />
                            </departure>
                            <departure>
                              <departureTime timePlanned="""" timeReal="""" delay="""" countdown="""" />
                            </departure>
                            <firstDeparture>
                              <departureTime timePlanned="""" timeReal="""" delay="""" countdown="""" />
                              <vehicle name="""" type="""" towards="""" direction="""" platform="""" barrierFree="""" realtimeSupported=""""/>
                            </firstDeparture>
                            <lastDeparture>
                              <departureTime timePlanned="""" timeReal="""" delay="""" countdown="""" />
                              <vehicle name="""" type="""" towards="""" direction="""" platform="""" barrierFree="""" realtimeSupported=""""/>
                            </lastDeparture>
                          </departures>
                        </line>
                      </lines>
                    </monitor>
                    <trafficInfos />
                    <message messageCode=""1"">ok</message>
                  </response>
                </ft>";
        #endregion

        #region Praterstern from documentation

        public const string PratersternFromDocumentation =
@"<?xml version=""1.0"" encoding=""UTF-8""?> 
<ft> 
  <response> 
    <client device="""" appName="""" clientId=""123"" appVersion=""""/> 
    <responseType>api_get_monitor</responseType> 
    <responseTime>2011-05-31 14:41:13</responseTime> 
    <monitor id=""36469"" clientexpiration=""""> 
      <request status="""" requestId="""" sessionId=""""/> 
      <locationStop status=""identified""> 
        <location name=""60201040"" title=""Praterstern"" municipality=""Wien"" type=""stop"" listIndex="""" coordName=""WGS84"" wgs84Lat=""16.39176"" wgs84Lon=""48.21815"" coordX="""" coordY="""" distanceMeter="""" distanceMinute=""""/> 
      </locationStop> 
      <lines count=""24""> 
        <line name=""U1"" type=""ptMetro"" towards=""Leopoldau"" direction=""H"" platform=""U1_H"" barrierFree=""1"" realtimeSupported=""1""> 
          <departures count=""2""> 
            <departure> 
              <departureTime timePlanned="""" timeReal=""2011-05-31 14:44:11"" delay="""" countdown=""3""/> 
            </departure> 
            <departure> 
              <departureTime timePlanned="""" timeReal=""2011-05-31 14:47:11"" delay="""" countdown=""6""/> 
            </departure> 
            <firstDeparture> 
              <departureTime timePlanned=""2011-05-31 05:16:00"" timeReal="""" delay="""" countdown=""""/> 
            </firstDeparture> 
            <lastDeparture> 
              <departureTime timePlanned=""2011-05-31 00:35:00"" timeReal="""" delay="""" countdown=""""/> 
            </lastDeparture> 
          </departures> 
        </line> 
        <line name=""U1"" type=""ptMetro"" towards=""Reumannplatz"" direction=""R"" platform=""U1_R"" barrierFree=""1"" realtimeSupported=""1""> 
          <departures count=""2""> 
            <departure> 
              <departureTime timePlanned="""" timeReal=""2011-05-31 14:42:11"" delay="""" countdown=""1""/> 
            </departure> 
            <departure> 
              <departureTime timePlanned="""" timeReal=""2011-05-31 14:46:11"" delay="""" countdown=""5""/> 
            </departure> 
            <firstDeparture> 
              <departureTime timePlanned=""2011-05-31 05:07:00"" timeReal="""" delay="""" countdown=""""/> 
            </firstDeparture> 
            <lastDeparture> 
              <departureTime timePlanned=""2011-05-31 00:21:00"" timeReal="""" delay="""" countdown=""""/> 
            </lastDeparture> 
          </departures> 
        </line> 
        <line name=""U2"" type=""ptMetro"" towards=""Karlsplatz"" direction=""H"" platform=""U2_H"" barrierFree=""1"" realtimeSupported=""1""> 
          <departures count=""2""> 
            <departure> 
              <departureTime timePlanned="""" timeReal=""2011-05-31 14:46:11"" delay="""" countdown=""5""/> 
            </departure> 
            <departure> 
              <departureTime timePlanned="""" timeReal=""2011-05-31 14:50:11"" delay="""" countdown=""9""/> 
            </departure> 
            <firstDeparture> 
              <departureTime timePlanned=""2011-05-31 05:05:00"" timeReal="""" delay="""" countdown=""""/> 
            </firstDeparture> 
            <lastDeparture> 
              <departureTime timePlanned=""2011-05-31 00:15:00"" timeReal="""" delay="""" countdown=""""/> 
            </lastDeparture> 
          </departures> 
        </line> 
        <line name=""U2"" type=""ptMetro"" towards=""Aspernstraße"" direction=""R"" platform=""U2_R"" barrierFree=""1"" realtimeSupported=""1""> 
          <departures count=""2""> 
            <departure> 
              <departureTime timePlanned="""" timeReal=""2011-05-31 14:42:11"" delay="""" countdown=""1""/> 
            </departure> 
            <departure> 
              <departureTime timePlanned="""" timeReal=""2011-05-31 14:48:11"" delay="""" countdown=""7""/> 
            </departure> 
            <firstDeparture> 
              <departureTime timePlanned=""2011-05-31 05:12:00"" timeReal="""" delay="""" countdown=""""/> 
            </firstDeparture> 
            <lastDeparture> 
              <departureTime timePlanned=""2011-05-31 00:41:00"" timeReal="""" delay="""" countdown=""""/> 
            </lastDeparture> 
          </departures> 
        </line> 
        <line name=""O"" type=""ptTram"" towards=""Raxstraße Rudolfshügelg."" direction=""R"" platform="""" barrierFree=""1"" realtimeSupported=""1""> 
          <departures count=""4""> 
            <departure> 
              <departureTime timePlanned="""" timeReal=""2011-05-31 14:46:00"" delay="""" countdown=""4""/> 
            </departure> 
            <departure> 
              <departureTime timePlanned="""" timeReal=""2011-05-31 14:53:00"" delay="""" countdown=""11""/> 
              <vehicle name=""O"" type=""ptTram"" towards=""Raxstraße Rudolfshügelg."" direction=""R"" platform="""" barrierFree=""0"" realtimeSupported=""1""/> 
            </departure> 
            <departure> 
              <departureTime timePlanned="""" timeReal=""2011-05-31 15:00:00"" delay="""" countdown=""18""/> 
              <vehicle name=""O"" type=""ptTram"" towards=""Raxstraße Rudolfshügelg."" direction=""R"" platform="""" barrierFree=""0"" realtimeSupported=""1""/> 
            </departure> 
            <departure> 
              <departureTime timePlanned="""" timeReal=""2011-05-31 15:06:00"" delay="""" countdown=""24""/> 
            </departure> 
            <firstDeparture> 
              <departureTime timePlanned=""2011-05-31 05:14:00"" timeReal="""" delay="""" countdown=""""/> 
            </firstDeparture> 
            <lastDeparture> 
              <departureTime timePlanned=""2011-05-31 00:17:00"" timeReal="""" delay="""" countdown=""""/> 
            </lastDeparture> 
          </departures> 
        </line> 
        <line name=""5"" type=""ptTram"" towards=""Westbahnhof SU"" direction=""H"" platform="""" barrierFree=""0"" realtimeSupported=""1""> 
          <departures count=""4""> 
            <departure> 
              <departureTime timePlanned="""" timeReal=""2011-05-31 14:46:00"" delay="""" countdown=""4""/> 
            </departure> 
            <departure> 
              <departureTime timePlanned="""" timeReal=""2011-05-31 14:54:00"" delay="""" countdown=""12""/> 
              <vehicle name=""5"" type=""ptTram"" towards=""Westbahnhof SU"" direction=""H"" platform="""" barrierFree=""1"" realtimeSupported=""1""/> 
            </departure> 
            <departure> 
              <departureTime timePlanned="""" timeReal=""2011-05-31 15:01:00"" delay="""" countdown=""19""/> 
            </departure> 
            <departure> 
              <departureTime timePlanned="""" timeReal=""2011-05-31 15:09:00"" delay="""" countdown=""27""/> 
            </departure> 
            <firstDeparture> 
              <departureTime timePlanned=""2011-05-31 05:02:00"" timeReal="""" delay="""" countdown=""""/> 
            </firstDeparture> 
            <lastDeparture> 
              <departureTime timePlanned=""2011-05-31 00:20:00"" timeReal="""" delay="""" countdown=""""/> 
            </lastDeparture> 
          </departures> 
        </line> 
        <line name=""80A"" type=""ptBusCity"" towards=""Taborstraße U"" direction=""H"" platform=""80A_R"" barrierFree=""1"" realtimeSupported=""1""> 
          <departures count=""4""> 
            <departure> 
              <departureTime timePlanned=""2011-05-31 14:47:00"" timeReal="""" delay="""" countdown=""6""/> 
            </departure> 
            <departure> 
              <departureTime timePlanned=""2011-05-31 14:57:00"" timeReal="""" delay="""" countdown=""16""/> 
            </departure> 
            <departure> 
              <departureTime timePlanned=""2011-05-31 15:07:00"" timeReal="""" delay="""" countdown=""26""/> 
            </departure> 
            <departure> 
              <departureTime timePlanned=""2011-05-31 15:17:00"" timeReal="""" delay="""" countdown=""36""/> 
            </departure> 
            <firstDeparture> 
              <departureTime timePlanned=""2011-05-31 05:55:00"" timeReal="""" delay="""" countdown=""""/> 
            </firstDeparture> 
            <lastDeparture> 
              <departureTime timePlanned=""2011-05-31 20:17:00"" timeReal="""" delay="""" countdown=""""/> 
            </lastDeparture> 
          </departures> 
        </line> 
        <line name=""80A"" type=""ptBusCity"" towards=""Schlachthausgasse U"" direction=""R"" platform=""80A_H"" barrierFree=""1"" realtimeSupported=""1""> 
          <departures count=""5""> 
            <departure> 
              <departureTime timePlanned=""2011-05-31 14:40:00"" timeReal="""" delay="""" countdown=""0""/> 
            </departure> 
            <departure> 
              <departureTime timePlanned=""2011-05-31 14:50:00"" timeReal="""" delay="""" countdown=""9""/> 
            </departure> 
            <departure> 
              <departureTime timePlanned=""2011-05-31 15:00:00"" timeReal="""" delay="""" countdown=""19""/> 
            </departure> 
            <departure> 
              <departureTime timePlanned=""2011-05-31 15:10:00"" timeReal="""" delay="""" countdown=""29""/> 
            </departure> 
            <departure> 
              <departureTime timePlanned=""2011-05-31 15:20:00"" timeReal="""" delay="""" countdown=""39""/> 
            </departure> 
            <firstDeparture> 
              <departureTime timePlanned=""2011-05-31 04:49:00"" timeReal="""" delay="""" countdown=""""/> 
            </firstDeparture> 
            <lastDeparture> 
              <departureTime timePlanned=""2011-05-31 22:47:00"" timeReal="""" delay="""" countdown=""""/> 
            </lastDeparture> 
          </departures> 
        </line> 
        <line name=""82A"" type=""ptBusCity"" towards=""Krieau U"" direction=""H"" platform="""" barrierFree=""1"" realtimeSupported=""1""> 
          <departures count=""2""> 
            <departure> 
              <departureTime timePlanned="""" timeReal=""2011-05-31 14:45:00"" delay="""" countdown=""3""/> 
            </departure> 
            <departure> 
              <departureTime timePlanned="""" timeReal=""2011-05-31 15:00:00"" delay="""" countdown=""18""/> 
            </departure> 
            <firstDeparture> 
              <departureTime timePlanned=""2011-05-31 06:00:00"" timeReal="""" delay="""" countdown=""""/> 
            </firstDeparture> 
            <lastDeparture> 
              <departureTime timePlanned=""2011-05-31 20:30:00"" timeReal="""" delay="""" countdown=""""/> 
            </lastDeparture> 
          </departures> 
        </line> 
        <line name=""S1"" type=""ptTrainS"" towards=""Gänserndorf"" direction=""H"" platform=""S1_H"" barrierFree=""0"" realtimeSupported=""1""> 
          <departures count=""1""> 
            <departure> 
              <departureTime timePlanned=""2011-05-31 14:59:00"" timeReal="""" delay="""" countdown=""18""/> 
            </departure> 
            <firstDeparture> 
              <departureTime timePlanned=""2011-05-31 05:05:00"" timeReal="""" delay="""" countdown=""""/> 
            </firstDeparture> 
            <lastDeparture> 
              <departureTime timePlanned=""2011-05-31 01:29:00"" timeReal="""" delay="""" countdown=""""/> 
            </lastDeparture> 
          </departures> 
        </line> 
        <line name=""S2"" type=""ptTrainS"" towards=""Wolkersdorf"" direction=""H"" platform=""S1_H"" barrierFree=""0"" realtimeSupported=""1""> 
          <departures count=""4""> 
            <departure> 
              <departureTime timePlanned=""2011-05-31 14:44:00"" timeReal="""" delay="""" countdown=""3""/> 
            </departure> 
            <departure> 
              <departureTime timePlanned=""2011-05-31 14:50:00"" timeReal="""" delay="""" countdown=""9""/> 
              <vehicle name=""S2"" type=""ptTrainS"" towards=""Floridsdorf"" direction=""H"" platform=""S1_H"" barrierFree=""0"" realtimeSupported=""1""/> 
            </departure> 
            <departure> 
              <departureTime timePlanned=""2011-05-31 15:08:00"" timeReal="""" delay="""" countdown=""27""/> 
            </departure> 
            <departure> 
              <departureTime timePlanned=""2011-05-31 15:20:00"" timeReal="""" delay="""" countdown=""39""/> 
              <vehicle name=""S2"" type=""ptTrainS"" towards=""Mistelbach"" direction=""H"" platform=""S1_H"" barrierFree=""0"" realtimeSupported=""1""/> 
            </departure> 
            <firstDeparture> 
              <departureTime timePlanned=""2011-05-31 05:44:00"" timeReal="""" delay="""" countdown=""""/> 
            </firstDeparture> 
            <lastDeparture> 
              <departureTime timePlanned=""2011-05-31 00:20:00"" timeReal="""" delay="""" countdown=""""/> 
            </lastDeparture> 
          </departures> 
        </line> 
        <line name=""S5"" type=""ptTrainS"" towards=""Absdorf-Hippersdorf"" direction=""H"" platform=""S1_H"" barrierFree=""0"" realtimeSupported=""1""> 
          <departures count=""1""> 
            <departure> 
              <departureTime timePlanned=""2011-05-31 15:05:00"" timeReal="""" delay="""" countdown=""24""/> 
            </departure> 
            <firstDeparture> 
              <departureTime timePlanned=""2011-05-31 06:05:00"" timeReal="""" delay="""" countdown=""""/> 
            </firstDeparture> 
            <lastDeparture> 
              <departureTime timePlanned=""2011-05-31 21:05:00"" timeReal="""" delay="""" countdown=""""/> 
            </lastDeparture> 
          </departures> 
        </line> 
        <line name=""S6"" type=""ptTrainS"" towards=""Wiener Neustadt Hauptbahnhof"" direction=""H"" platform=""_H"" barrierFree=""0"" realtimeSupported=""1""> 
          <departures count=""1""> 
            <departure> 
              <departureTime timePlanned=""2011-05-31 14:54:00"" timeReal="""" delay="""" countdown=""13""/> 
            </departure> 
            <firstDeparture> 
              <departureTime timePlanned=""2011-05-31 04:54:00"" timeReal="""" delay="""" countdown=""""/> 
            </firstDeparture> 
            <lastDeparture> 
              <departureTime timePlanned=""2011-05-31 19:54:00"" timeReal="""" delay="""" countdown=""""/> 
            </lastDeparture> 
          </departures> 
        </line> 
        <line name=""S7"" type=""ptTrainS"" towards=""Flughafen Wien (VIE)"" direction=""H"" platform=""_H"" barrierFree=""0"" realtimeSupported=""1""> 
          <departures count=""2""> 
            <departure> 
              <departureTime timePlanned=""2011-05-31 14:42:00"" timeReal="""" delay="""" countdown=""1""/> 
            </departure> 
            <departure> 
              <departureTime timePlanned=""2011-05-31 15:12:00"" timeReal="""" delay="""" countdown=""31""/> 
              <vehicle name=""S7"" type=""ptTrainS"" towards=""Wolfsthal"" direction=""H"" platform=""_H"" barrierFree=""0"" realtimeSupported=""1""/> 
            </departure> 
            <firstDeparture> 
              <departureTime timePlanned=""2011-05-31 04:27:00"" timeReal="""" delay="""" countdown=""""/> 
            </firstDeparture> 
            <lastDeparture> 
              <departureTime timePlanned=""2011-05-31 22:42:00"" timeReal="""" delay="""" countdown=""""/> 
            </lastDeparture> 
          </departures> 
        </line> 
        <line name=""S7"" type=""ptTrainS"" towards=""Floridsdorf"" direction=""R"" platform=""S1_R"" barrierFree=""0"" realtimeSupported=""1""> 
          <departures count=""2""> 
            <departure> 
              <departureTime timePlanned=""2011-05-31 14:47:00"" timeReal="""" delay="""" countdown=""6""/> 
            </departure> 
            <departure> 
              <departureTime timePlanned=""2011-05-31 15:17:00"" timeReal="""" delay="""" countdown=""36""/> 
            </departure> 
            <firstDeparture> 
              <departureTime timePlanned=""2011-05-31 05:23:00"" timeReal="""" delay="""" countdown=""""/> 
            </firstDeparture> 
            <lastDeparture> 
              <departureTime timePlanned=""2011-05-31 00:47:00"" timeReal="""" delay="""" countdown=""""/> 
            </lastDeparture> 
          </departures> 
        </line> 
        <line name=""S9"" type=""ptTrainS"" towards=""Wiener Neustadt Hauptbahnhof"" direction=""H"" platform=""_H"" barrierFree=""0"" realtimeSupported=""1""> 
          <departures count=""5""> 
            <departure> 
              <departureTime timePlanned=""2011-05-31 14:48:00"" timeReal="""" delay="""" countdown=""7""/> 
            </departure> 
            <departure> 
              <departureTime timePlanned=""2011-05-31 15:00:00"" timeReal="""" delay="""" countdown=""19""/> 
              <vehicle name=""S9"" type=""ptTrainS"" towards=""Mödling"" direction=""H"" platform=""_H"" barrierFree=""0"" realtimeSupported=""1""/> 
            </departure> 
            <departure> 
              <departureTime timePlanned=""2011-05-31 15:18:00"" timeReal="""" delay="""" countdown=""37""/> 
              <vehicle name=""S9"" type=""ptTrainS"" towards=""Leobersdorf"" direction=""H"" platform=""_H"" barrierFree=""0"" realtimeSupported=""1""/> 
            </departure> 
            <departure> 
              <departureTime timePlanned=""2011-05-31 15:21:00"" timeReal="""" delay="""" countdown=""40""/> 
              <vehicle name=""S9"" type=""ptTrainS"" towards=""Wien Mitte"" direction=""H"" platform=""_H"" barrierFree=""0"" realtimeSupported=""1""/> 
            </departure> 
            <departure> 
              <departureTime timePlanned=""2011-05-31 15:24:00"" timeReal="""" delay="""" countdown=""43""/> 
              <vehicle name=""S9"" type=""ptTrainS"" towards=""Meidling"" direction=""H"" platform=""_H"" barrierFree=""0"" realtimeSupported=""1""/> 
            </departure> 
            <firstDeparture> 
              <departureTime timePlanned=""2011-05-31 04:12:00"" timeReal="""" delay="""" countdown=""""/> 
            </firstDeparture> 
            <lastDeparture> 
              <departureTime timePlanned=""2011-05-31 00:48:00"" timeReal="""" delay="""" countdown=""""/> 
            </lastDeparture> 
          </departures> 
        </line> 
        <line name=""S15"" type=""ptTrainS"" towards=""Unter Purkersdorf"" direction=""H"" platform=""_H"" barrierFree=""0"" realtimeSupported=""1""> 
          <departures count=""2""> 
            <departure> 
              <departureTime timePlanned=""2011-05-31 14:39:00"" timeReal="""" delay="""" countdown=""0""/> 
            </departure> 
            <departure> 
              <departureTime timePlanned=""2011-05-31 15:09:00"" timeReal="""" delay="""" countdown=""28""/> 
            </departure> 
            <firstDeparture> 
              <departureTime timePlanned=""2011-05-31 05:09:00"" timeReal="""" delay="""" countdown=""""/> 
            </firstDeparture> 
            <lastDeparture> 
              <departureTime timePlanned=""2011-05-31 22:09:00"" timeReal="""" delay="""" countdown=""""/> 
            </lastDeparture> 
          </departures> 
        </line> 
        <line name=""R 2243 Regionalzug"" type=""ptTrainR"" towards=""Wiener Neustadt Hauptbahnhof"" direction=""R"" platform=""_R"" barrierFree=""0"" realtimeSupported=""1""> 
          <departures count=""1""> 
            <departure> 
              <departureTime timePlanned=""2011-05-31 14:57:00"" timeReal="""" delay="""" countdown=""16""/> 
            </departure> 
            <firstDeparture> 
              <departureTime timePlanned="""" timeReal="""" delay="""" countdown=""""/> 
            </firstDeparture> 
            <lastDeparture> 
              <departureTime timePlanned="""" timeReal="""" delay="""" countdown=""""/> 
            </lastDeparture> 
          </departures> 
        </line> 
        <line name=""R 2340 Regionalzug"" type=""ptTrainR"" towards=""Floridsdorf"" direction=""H"" platform=""S1_H"" barrierFree=""0"" realtimeSupported=""1""> 
          <departures count=""1""> 
            <departure> 
              <departureTime timePlanned=""2011-05-31 14:41:00"" timeReal="""" delay="""" countdown=""0""/> 
            </departure> 
            <firstDeparture> 
              <departureTime timePlanned="""" timeReal="""" delay="""" countdown=""""/> 
            </firstDeparture> 
            <lastDeparture> 
              <departureTime timePlanned="""" timeReal="""" delay="""" countdown=""""/> 
            </lastDeparture> 
          </departures> 
        </line> 
        <line name=""R 2341 Regionalzug"" type=""ptTrainR"" towards=""Wiener Neustadt Hauptbahnhof"" direction=""H"" platform=""_H"" barrierFree=""0"" realtimeSupported=""1""> 
          <departures count=""1""> 
            <departure> 
              <departureTime timePlanned=""2011-05-31 14:45:00"" timeReal="""" delay="""" countdown=""4""/> 
            </departure> 
            <firstDeparture> 
              <departureTime timePlanned="""" timeReal="""" delay="""" countdown=""""/> 
            </firstDeparture> 
            <lastDeparture> 
              <departureTime timePlanned="""" timeReal="""" delay="""" countdown=""""/> 
            </lastDeparture> 
          </departures> 
        </line> 
        <line name=""R 2342 Regionalzug"" type=""ptTrainR"" towards=""Bernhardsthal"" direction=""R"" platform=""S1_R"" barrierFree=""0"" realtimeSupported=""1""> 
          <departures count=""1""> 
            <departure> 
              <departureTime timePlanned=""2011-05-31 15:11:00"" timeReal="""" delay="""" countdown=""30""/> 
            </departure> 
            <firstDeparture> 
              <departureTime timePlanned="""" timeReal="""" delay="""" countdown=""""/> 
            </firstDeparture> 
            <lastDeparture> 
              <departureTime timePlanned="""" timeReal="""" delay="""" countdown=""""/> 
            </lastDeparture> 
          </departures> 
        </line> 
        <line name=""R 2343 Regionalzug"" type=""ptTrainR"" towards=""Wiener Neustadt Hauptbahnhof"" direction=""R"" platform=""_R"" barrierFree=""0"" realtimeSupported=""1""> 
          <departures count=""1""> 
            <departure> 
              <departureTime timePlanned=""2011-05-31 15:15:00"" timeReal="""" delay="""" countdown=""34""/> 
            </departure> 
            <firstDeparture> 
              <departureTime timePlanned="""" timeReal="""" delay="""" countdown=""""/> 
            </firstDeparture> 
            <lastDeparture> 
              <departureTime timePlanned="""" timeReal="""" delay="""" countdown=""""/> 
            </lastDeparture> 
          </departures> 
        </line> 
        <line name=""R 2408 Regionalzug"" type=""ptTrainR"" towards=""Laa an der Thaya"" direction=""R"" platform=""S1_R"" barrierFree=""0"" realtimeSupported=""1""> 
          <departures count=""1""> 
            <departure> 
              <departureTime timePlanned=""2011-05-31 14:54:00"" timeReal="""" delay="""" countdown=""13""/> 
            </departure> 
            <firstDeparture> 
              <departureTime timePlanned="""" timeReal="""" delay="""" countdown=""""/> 
            </firstDeparture> 
            <lastDeparture> 
              <departureTime timePlanned="""" timeReal="""" delay="""" countdown=""""/> 
            </lastDeparture> 
          </departures> 
        </line> 
        <line name=""R 2242 C.M. Hofbauer"" type=""other"" towards=""Satov"" direction=""R"" platform=""S1_R"" barrierFree=""0"" realtimeSupported=""1""> 
          <departures count=""1""> 
            <departure> 
              <departureTime timePlanned=""2011-05-31 15:02:00"" timeReal="""" delay="""" countdown=""21""/> 
            </departure> 
            <firstDeparture> 
              <departureTime timePlanned="""" timeReal="""" delay="""" countdown=""""/> 
            </firstDeparture> 
            <lastDeparture> 
              <departureTime timePlanned="""" timeReal="""" delay="""" countdown=""""/> 
            </lastDeparture> 
          </departures> 
        </line> 
      </lines> 
    </monitor> 
    <trafficInfos/> 
    <message messageCode=""1"">ok</message> 
  </response> 
</ft> ";

        #endregion
    }
}