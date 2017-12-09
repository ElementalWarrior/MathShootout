using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class StandingsBehaviour : MonoBehaviour {

    public enum Round
    {
        QuarterFinal,
        SemiFinal,
        Finals,
        Finished
    }
    GameObject[] QuarterFinalLabels;
	string[] QuarterFinalTeams;
    GameObject[] SemiFinalLabels;
    GameObject[] FinalistLabels;

    public Round CurrentRound;

	public string[] animals = new string[26] {
		"Alligators",
		"Bears",
		"Cougars",
		"Dolphins",
		"Elephants",
		"Frogs",
		"Grasshoppers",
		"Hippos",
		"Iguanas",
		"Jaguars",
		"Kangaroos",
		"Llamas",
		"Monkeys",
		"Nighthawks",
		"Otters",
		"Penguins",
		"Quail",
		"Roadrunners",
		"Squirrels",
		"Tigers",
		"Unicorns",
		"Vipers",
		"Wolverines",
		"Xenons",
		"Yellowjackets",
		"Zebras"
	};

    private GameObject Shop;

    public static bool ShopOpen;

    private double rad2deg(double rad)
    {
        return (rad / Math.PI * 180.0);
    }

    private double deg2rad(double deg)
    {
        return (deg * Math.PI / 180.0);
    }

    private double distance(double lat1, double lon1, double lat2, double lon2, char unit = 'K')
    {
        double theta = lon1 - lon2;
        double dist = Math.Sin(deg2rad(lat1)) * Math.Sin(deg2rad(lat2)) + Math.Cos(deg2rad(lat1)) * Math.Cos(deg2rad(lat2)) * Math.Cos(deg2rad(theta));
        dist = Math.Acos(dist);
        dist = rad2deg(dist);
        dist = dist * 60 * 1.1515;
        if (unit == 'K')
        {
            dist = dist * 1.609344;
        }
        else if (unit == 'N')
        {
            dist = dist * 0.8684;
        }
        return (dist);
    }
    // Use this for initialization
    void Start () {
        Shop = Resources.Load<GameObject>("Standings/shop");
		CurrentRound = Round.QuarterFinal;

        if (PlayerPrefs.HasKey("CurrentRound"))
        {
			Debug.Log (JsonUtility.FromJson<Round> (PlayerPrefs.GetString ("CurrentRound")));
            CurrentRound = JsonUtility.FromJson<Round>(PlayerPrefs.GetString("CurrentRound"));
        } else
        {
            CurrentRound = Round.QuarterFinal;
        }
        Transform canvas = GameObject.Find("Canvas").transform.root;

        // if we lose a game, we don't progress, but don't regress, so just set all top labels to our username
        GameObject.Find("Username").GetComponent<Text>().text = PlayerPrefs.GetString("TeamName");
        GameObject.Find("Winner1").GetComponent<Text>().text = PlayerPrefs.GetString("TeamName");
        GameObject.Find("Finalist1").GetComponent<Text>().text = PlayerPrefs.GetString("TeamName");
        GameObject.Find("Champion").GetComponent<Text>().text = PlayerPrefs.GetString("TeamName");

        //hide user upper bracket labels
        canvas.Find("Winner1").gameObject.SetActive(false);
        canvas.Find("Finalist1").gameObject.SetActive(false);

        /* Location services enabled */
        if (Input.location.status == LocationServiceStatus.Running) {

            float latitude = Input.location.lastData.latitude;
            float longitude = Input.location.lastData.longitude;
            //float latitude = 49.798654f;
            //float longitude = -119.512252f;

            /* Open cities database */

            QuarterFinalTeams = new string[7];

            Locations.Location[] cities = new Locations.Location[8];

            double max = distance(latitude, longitude, Locations.data[0].Latitude, Locations.data[0].Longitude);
            for (int i = 0; i < Locations.data.Length; i++)
            {
                Locations.Location location = Locations.data[i];
                double dist = distance(latitude, longitude, location.Latitude, location.Longitude);
                location.Distance = dist;
                if(i < 8)
                {
                    cities[i] = location;
                    max = Math.Max(dist, max);
                } else
                {
                    if(dist < max)
                    {
                        cities[7] = location;
                        Array.Sort(cities, new Comparison<Locations.Location>((obj1, obj2) => obj1.Distance.CompareTo((Double)obj2.Distance)));
                    }
                }
            }

            //add animal names to end of city names
            for (int i = 0; i < 7; i++)
            {
                QuarterFinalTeams[i] = (cities[i].City + " " + animals[char.ToUpper(cities[i].City[0]) - 65]);
                GameObject.Find("Team" + (i + 1)).GetComponent<Text>().text = QuarterFinalTeams[i];
            }
		}

		QuarterFinalLabels = new GameObject[]
		{
			canvas.Find("Team1").gameObject,
			canvas.Find("Team2").gameObject,
			canvas.Find("Team3").gameObject,
			canvas.Find("Team4").gameObject,
			canvas.Find("Team5").gameObject,
			canvas.Find("Team6").gameObject,
			canvas.Find("Team7").gameObject,
		};

        SemiFinalLabels = new GameObject[]
        {
            canvas.Find("Winner2").gameObject,
            canvas.Find("Winner3").gameObject,
            canvas.Find("Winner4").gameObject,
        };
        FinalistLabels = new GameObject[]
        {
            canvas.Find("Finalist2").gameObject,
        };
        switch (CurrentRound)
        {
            case Round.QuarterFinal:
                SetLabelVisibility(SemiFinalLabels, false);
                SetLabelVisibility(FinalistLabels, false);
                SetLabelVisibility(new GameObject[] { canvas.Find("Champion").gameObject }, false);
                break;
            case Round.SemiFinal:

                //decide which teams that won the quarter finals if this is the first time visiting the standings page at the semi final level
                if (!PlayerPrefs.HasKey("QuarterFinalWinners"))
                {
                    PlayerPrefs.SetString("QuarterFinalWinners", SerializeStringArray(DecideQuarterFinalWinners()));
                }


                canvas.Find("Winner1").gameObject.SetActive(true);
                SetLabelVisibility(SemiFinalLabels, true);
                SetLabelVisibility(FinalistLabels, false);
                SetLabelVisibility(new GameObject[] { canvas.Find("Champion").gameObject }, false);
                break;
            case Round.Finals:
                if (!PlayerPrefs.HasKey("SemiFinalWinner") || true)
                {
                    PlayerPrefs.SetString("SemiFinalWinner", DecideSemiFinalWinner());
                }

                canvas.Find("Winner1").gameObject.SetActive(true);
                canvas.Find("Finalist1").gameObject.SetActive(true);
                SetLabelVisibility(SemiFinalLabels, true);
                SetLabelVisibility(FinalistLabels, true);
                SetLabelVisibility(new GameObject[] { canvas.Find("Champion").gameObject }, false);
                break;
            case Round.Finished:
                canvas.Find("Winner1").gameObject.SetActive(true);
                canvas.Find("Finalist1").gameObject.SetActive(true);
                SetLabelVisibility(SemiFinalLabels, true);
                SetLabelVisibility(FinalistLabels, true);
                SetLabelVisibility(new GameObject[] { canvas.Find("Champion").gameObject }, true);
                DiscolorLabel(canvas.transform.Find("Finalist2").gameObject);

                GameObject.Find("Continue").SetActive(false);
                break;
        }
        if(CurrentRound != Round.Finished)
        {
            GameObject.Find("ButtonChampion").SetActive(false);
        }

        if (PlayerPrefs.HasKey("QuarterFinalWinners"))
        {
            string[] quarterFinalWinners = DeserializeStringArray(PlayerPrefs.GetString("QuarterFinalWinners"));
            for(int i = 0; i < SemiFinalLabels.Length; i++)
            {
                GameObject label = SemiFinalLabels[i];
                label.GetComponent<Text>().text = quarterFinalWinners[i];
            }
            DiscolourNonWinners(QuarterFinalLabels, quarterFinalWinners);
        }
        if (PlayerPrefs.HasKey("SemiFinalWinner"))
        {
            string semiFinalWinner = PlayerPrefs.GetString("SemiFinalWinner");
            canvas.transform.Find("Finalist2").GetComponent<Text>().text = semiFinalWinner;
            DiscolourNonWinners(SemiFinalLabels, new string[] { semiFinalWinner });
        }
    }

	Assets.Scripts.Locations.Location newCoordinates(float lat, float longi, int km_offset1, int km_offset2) {

		/* Convert offset (in km) to degrees. 
					1 degree in Google maps = 111.32 km */
		double degree1 = km_offset1 * (1 / 111.32);
		double degree2 = km_offset2 * (1 / 111.32);

		double lat2 = lat + degree1;

		Assets.Scripts.Locations.Location new_loc = new Assets.Scripts.Locations.Location {
			City = "",
			Latitude = lat2,
			Longitude = (longi + degree2 / Mathf.Cos ((float)(lat2 * Mathf.PI / 180)))
		};

		return new_loc;
	}

    string SerializeStringArray(string[] values)
    {
        string serialized = "";
        foreach(string value in values)
        {
            serialized += value + ",";
        }
        return serialized;
    }
    string[] DeserializeStringArray(string value)
    {
        return value.Split(new char[] { ',' }, System.StringSplitOptions.RemoveEmptyEntries);
    }
    string[] DecideQuarterFinalWinners()
    {
		string[] teams = new string[6];
        
		/* If location services are off, use default teams */
		if (Input.location.status == LocationServiceStatus.Running) {
			teams [0] = "East Coast Eagles";
			teams [1] = "Northern Owls";
			teams [2] = "Southern Snakes";
			teams [3] = "Midwest Minstrels";
			teams [4] = "Cape Camels";
			teams [5] = "Gulf Gophers";
		} 

		/* Location services are enabled, use local teams previously constructed */
		else {
			teams [0] = QuarterFinalTeams [1];
			teams [1] = QuarterFinalTeams [2];
			teams [2] = QuarterFinalTeams [3];
			teams [3] = QuarterFinalTeams [4];
			teams [4] = QuarterFinalTeams [5];
			teams [5] = QuarterFinalTeams [6];
		}

        string winner2 = teams[UnityEngine.Random.Range(0, 1+1)];
        string winner3 = teams[UnityEngine.Random.Range(2, 3+1)];
        string winner4 = teams[UnityEngine.Random.Range(4, 5+1)];

        return new string[] { winner2, winner3, winner4 };
    }
    string DecideSemiFinalWinner()
    {
        string[] quarterFinalWinners = DeserializeStringArray(PlayerPrefs.GetString("QuarterFinalWinners"));
        return quarterFinalWinners[UnityEngine.Random.Range(1, 2 + 1)];
    }

    void DiscolourNonWinners(GameObject[] labels, string[] winners)
    {
        foreach (GameObject team in labels)
        {
            string teamName = team.GetComponent<Text>().text;
            if (System.Array.IndexOf<string>(winners, teamName) == -1)
            {
                DiscolorLabel(team);
            }
        }
    }
    void DiscolorLabel(GameObject label)
    {
        label.GetComponent<Text>().color = new Color(0, 0, 0, 0.2f);
    }
    void SetLabelVisibility(GameObject[] labels, bool visible)
    {
        foreach(GameObject obj in labels)
        {
            obj.SetActive(visible);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OpenShop ()
    {
        if(ShopOpen)
        {
            return;
        }
        GameObject itemBeingCreated = GameObject.Instantiate(Shop, GameObject.Find("Canvas").transform);
        itemBeingCreated.name = "Shop";
        ShopOpen = true;
    }

    public void CloseShop()
    {
        if (!ShopOpen)
        {
            return;
        }
        ShopOpen = false;
        GameObject.Destroy(GameObject.Find("Shop"));
    }
}
