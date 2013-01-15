Vienna RealTime
==============

A Windows Phone 8 App to display realtime public transport information for Vienna, Austria.
The source code is released under the MIT License.

The app contains two implementations for data / realtime providers:

* `CreateCamp` This is an implementation created while at the CreateCamp in Vienna, Jan 12th & 13th 2013. This was a "preview event" of how
	a future official Wiener Linien API could look like, with a focus on feedback / app ideas. The API was not fully built out at that event,
	and access to it was limited for participants during the weekend (ie you cannot access it any more).
* `Qando` Calls the qando.at Web Service API for monitor information. Station search (nearby, fulltext) is based on a stop list 
	provided by [OGD Wien](http://data.wien.gv.at/katalog/haltestellen.html) 
	(open government data for the city of Vienna, Austria) and is performed entirely on the device. This fulfills
	the purpose of not having to send PII (personally identifyable information) to a server (eg current location for nearby search).

## Features

* `Nearby` - list / map stations near my current location
* `Search` - search stations by (partial) name
* `List` - list all stations
* `Departures` - list all departures by line / direction for a station (selected via one of the previous three options)

## What is Missing

* There is no public information on stops per line (you could type that in yourself, but...). Thus you cannot list the lines that serve a specific
	stop, or do a "reverse" search by listing the stops on a certain line to pick a stop.
	
## Screenshots

Please see the StoreSubmissionAssets folder, it contains screenshots in various sizes as well as the App logo.