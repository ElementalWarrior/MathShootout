using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MiniJSON;

namespace PlayerLocation {

	public class Locations {

		public static string city;

		public static IEnumerator locate(bool location_on, int km_offset1, int km_offset2) {

			string APIKey = "AIzaSyCRzW0KJ1CIWdnq-eErWh5zVfivXEA4YLo";
			string latitude;
			string longitude;

			/* Player has location settings on */
			if (location_on && (Input.location.status == LocationServiceStatus.Running)) {

				/* Calculate player's current location */
				if ((km_offset1 == 0) && (km_offset2 == 0)) {
					longitude = Input.location.lastData.longitude.ToString ();
					latitude = Input.location.lastData.latitude.ToString ();
				} 

				/* Calculate nearby city based on player's current location */
				else {

					/* Convert offset (in km) to degrees. 
						1 degree in Google maps = 111.32 km */
					double degree1 = km_offset1 * (1 / 111.32);
					double degree2 = km_offset2 * (1 / 111.32);

					double lat = Input.location.lastData.latitude + degree1;
					longitude = (Input.location.lastData.longitude + degree2 / Mathf.Cos ((float) (lat * Mathf.PI / 180))).ToString ();
					latitude = lat.ToString ();
				}

				Input.location.Stop();
			}

			else {
				yield break;
			}

			using (WWW web = new WWW ("https://maps.googleapis.com/maps/api/geocode/json?latlng=" + latitude + "," + longitude + "&key=" + APIKey + "")) {
				yield return web;

				/* Request completed successfully */
				if(web.error == null)
				{
					/* Perform deserialization */
					var location =  Json.Deserialize(web.text) as Dictionary<string, object>;
					var locationList = location["results"] as List<object>;
					var locationDict = locationList[0] as Dictionary<string, object>;

					/* Find the city given the coordinates */
					city = locationDict["formatted_address"].ToString().Substring(locationDict["formatted_address"].ToString().IndexOf(",")+2);

					yield break;
				}
			};
		}

		public static string surrounding(bool location_on, string curr, int offset1, int offset2, int incr, bool incr1, bool incr2) {
			if (location_on && (Input.location.status == LocationServiceStatus.Running)) { 
				locate (location_on, offset1, offset2);
				string other_city = city;

				int km = incr;
				/* If surrounding city has not been found, extend offset */
				while (other_city.Equals (curr)) {
					km = km + incr;

					if (incr1 && incr2) {
						locate (location_on, km, km);
						other_city = city;
					} else if (incr1 && !incr2) {
						locate (location_on, km, offset2);
						other_city = city;
					} else if (!incr1 && incr2) {
						locate (location_on, offset1, km);
						other_city = city;
					}
				}
			}
			return city;
		}
	}
}

