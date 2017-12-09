using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
namespace Assets.Scripts
{
	public class Locations
	{
		public class Location
		{
			public string City { get; set; }
			public double Latitude { get; set; }
			public double Longitude { get; set; }
		}
		public static Location[] data;
		public static void LoadData()
		{
			string path = "Assets/Resources/cities.csv";

			//Read the text from directly from the test.txt file
			StreamReader reader = new StreamReader(path);
			int cnt = 0;
			while(!reader.EndOfStream)
			{
				reader.ReadLine();
				cnt++;
			}
			reader.BaseStream.Seek(0, SeekOrigin.Begin);
			data = new Location[cnt];
			for(int i = 0; !reader.EndOfStream; i++)
			{
				var cols = reader.ReadLine().Split(',');
				data[i] = new Location
				{
					City = cols[0],
					Latitude = Double.Parse(cols[1]),
					Longitude = Double.Parse(cols[2])
				};
			}
			reader.Close();
		}
		public static Location First()
		{
			return data == null ? null : data[0];
		}
	}
}


