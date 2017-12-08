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

			string[] c0 = new string[] {
				latitude.ToString(),
				longitude.ToString()
			};

			/* Open cities database */

			QuarterFinalTeams = new string[7];

			GameObject.Find ("Team1").GetComponent<Text>().text = "IM IN HERE";

			string[] c1;
			string[] c2;
			string[] c3;
			string[] c4;
			string[] c5;
			string[] c6;

			string[] cities = new string[7];

			int count = 0;
			int loop = 0;
			int multiplier = -9;

			string line;

			while (count < 7) {

				multiplier = multiplier + 10;

				c1 = newCoordinates (latitude, longitude, 0, multiplier * 30);
				c2 = newCoordinates (latitude, longitude, multiplier * 30, 0);
				c3 = newCoordinates (latitude, longitude, multiplier * 30, multiplier * 30);
				c4 = newCoordinates (latitude, longitude, 0, multiplier * -30);
				c5 = newCoordinates (latitude, longitude, multiplier * -30, 0);
				c6 = newCoordinates (latitude, longitude, multiplier * -30, multiplier * -30);

				while ((loop < 5520) && (count < 7)) {
					line = PlayerLocation.Locations.locations [loop];
					if (line.Substring(line.IndexOf(","), line.LastIndexOf(",")).Contains (c0 [0]) 
						|| line.Substring(line.LastIndexOf(",")).Contains (c0 [1])) {
						cities [count] = line.Substring (0, line.IndexOf (","));
						count++;
					} else if (line.Substring(line.IndexOf(","), line.LastIndexOf(",")).Contains (c1 [0]) 
						|| line.Substring(line.LastIndexOf(",")).Contains (c1 [1])) {
						cities [count] = line.Substring (0, line.IndexOf (","));
						count++;
					} else if (line.Substring(line.IndexOf(","), line.LastIndexOf(",")).Contains (c2 [0]) 
						|| line.Substring(line.LastIndexOf(",")).Contains (c2 [1])) {
						cities [count] = line.Substring (0, line.IndexOf (","));
						count++;
					} else if (line.Substring(line.IndexOf(","), line.LastIndexOf(",")).Contains (c3 [0]) 
						|| line.Substring(line.LastIndexOf(",")).Contains (c3 [1])) {
						cities [count] = line.Substring (0, line.IndexOf (","));
						count++;
					} else if (line.Substring(line.IndexOf(","), line.LastIndexOf(",")).Contains (c4 [0]) 
						|| line.Substring(line.LastIndexOf(",")).Contains (c4 [1])) {
						cities [count] = line.Substring (0, line.IndexOf (","));
						count++;
					} else if (line.Substring(line.IndexOf(","), line.LastIndexOf(",")).Contains (c5 [0]) 
						|| line.Substring(line.LastIndexOf(",")).Contains (c5 [1])) {
						cities [count] = line.Substring (0, line.IndexOf (","));
						count++;
					} else if (line.Substring(line.IndexOf(","), line.LastIndexOf(",")).Contains (c6 [0]) 
						|| line.Substring(line.LastIndexOf(",")).Contains (c6 [1])) {
						cities [count] = line.Substring (0, line.IndexOf (","));
						count++;
					}

					loop++;
				}
			}

			QuarterFinalTeams [0] = (cities[0] + " " + animals [char.ToUpper ((cities[0]) [0]) - 65]);
			GameObject.Find ("Team1").GetComponent<Text>().text = QuarterFinalTeams[0];

			QuarterFinalTeams [1] = (cities[1] + " " + animals [char.ToUpper ((cities[1]) [0]) - 65]);
			GameObject.Find ("Team2").GetComponent<Text>().text = QuarterFinalTeams[1];

			QuarterFinalTeams [2] = (cities[2] + " " + animals [char.ToUpper ((cities[2]) [0]) - 65]);
			GameObject.Find ("Team3").GetComponent<Text>().text = QuarterFinalTeams[2];

			QuarterFinalTeams [3] = (cities[3] + " " + animals [char.ToUpper ((cities[3]) [0]) - 65]);
			GameObject.Find ("Team4").GetComponent<Text>().text = QuarterFinalTeams[3];

			QuarterFinalTeams [4] = (cities[4] + " " + animals [char.ToUpper ((cities[4]) [0]) - 65]);
			GameObject.Find ("Team5").GetComponent<Text>().text = QuarterFinalTeams[4];

			QuarterFinalTeams [5] = (cities[5] + " " + animals [char.ToUpper ((cities[5]) [0]) - 65]);
			GameObject.Find ("Team6").GetComponent<Text>().text = QuarterFinalTeams[5];

			QuarterFinalTeams [6] = (cities[6] + " " + animals [char.ToUpper ((cities[6]) [0]) - 65]);
			GameObject.Find ("Team7").GetComponent<Text>().text = QuarterFinalTeams[6];

//			PlayerLocation.Locations.locate (0, 0);
//			string curr_city = PlayerLocation.Locations.city;
//			QuarterFinalTeams [0] = (curr_city + " " + animals [char.ToUpper (curr_city [0]) - 65]);
//
//			GameObject.Find ("Team1").GetComponent<Text>().text = (curr_city + " " + animals [char.ToUpper (curr_city [0]) - 65]);

//			Transform team1 = canvas.Find ("Team1");
//			Text setTeam1 = team1.GetComponent<Text> ();
//			setTeam1.text = (curr_city + " " + animals [char.ToUpper (curr_city [0]) - 65]);

//			string other_city1 = PlayerLocation.Locations.surrounding (curr_city, 0, 10, 10, false, true);
//			QuarterFinalTeams [1] = (other_city1 + " " + animals [char.ToUpper (other_city1 [0]) - 65]);

//			Transform team2 = canvas.Find ("Team2");
//			Text setTeam2 = team2.GetComponent<Text> ();
//			setTeam2.text = (other_city1 + " " + animals [char.ToUpper (other_city1 [0]) - 65]);

//			GameObject.Find ("Team2").GetComponent<Text> ().text = (other_city1 + " " + animals [char.ToUpper (other_city1 [0]) - 65]);
//
//			string other_city2 = PlayerLocation.Locations.surrounding (curr_city, 10, 10, 10, true, true);
//			if (other_city2.Equals (other_city1)) {
//				other_city2 = PlayerLocation.Locations.surrounding (curr_city, 100, 100, 100, true, true);
//			}
//			QuarterFinalTeams [2] = (other_city2 + " " + animals [char.ToUpper (other_city2 [0]) - 65]);
//
//			GameObject.Find ("Team3").GetComponent<Text> ().text = (other_city2 + " " + animals [char.ToUpper (other_city2 [0]) - 65]);

//			Transform team3 = canvas.Find ("Team3");
//			Text setTeam3 = team3.GetComponent<Text> ();
//			setTeam3.text = (other_city2 + " " + animals [char.ToUpper (other_city2 [0]) - 65]);

//			string other_city3 = PlayerLocation.Locations.surrounding (curr_city, 10, 0, 10, true, false);
//			if (other_city3.Equals (other_city1) || other_city3.Equals(other_city2)) {
//				other_city3 = PlayerLocation.Locations.surrounding (curr_city, 100, 0, 100, true, false);
//			}
//			QuarterFinalTeams [3] = (other_city3 + " " + animals [char.ToUpper (other_city3 [0]) - 65]);
//
//			GameObject.Find ("Team4").GetComponent<Text> ().text = (other_city3 + " " + animals [char.ToUpper (other_city3 [0]) - 65]);

//			Transform team4 = canvas.Find ("Team4");
//			Text setTeam4 = team4.GetComponent<Text> ();
//			setTeam4.text = (other_city3 + " " + animals [char.ToUpper (other_city3 [0]) - 65]);

//			string other_city4 = PlayerLocation.Locations.surrounding (curr_city, 0, -10, -10, false, true);
//			if (other_city4.Equals (other_city1) || other_city4.Equals(other_city2) || other_city4.Equals(other_city3)) {
//				other_city4 = PlayerLocation.Locations.surrounding (curr_city, 0, -100, -100, false, true);
//			}
//			QuarterFinalTeams [4] = (other_city4 + " " + animals [char.ToUpper (other_city4 [0]) - 65]);
//
//			GameObject.Find ("Team5").GetComponent<Text> ().text = (other_city4 + " " + animals [char.ToUpper (other_city4 [0]) - 65]);

//			Transform team5 = canvas.Find ("Team5");
//			Text setTeam5 = team5.GetComponent<Text> ();
//			setTeam5.text = (other_city4 + " " + animals [char.ToUpper (other_city4 [0]) - 65]);

//			string other_city5 = PlayerLocation.Locations.surrounding (curr_city, -10, 0, -10, true, false);
//			if (other_city5.Equals (other_city1) || other_city5.Equals(other_city2) || other_city5.Equals(other_city3)
//				|| other_city5.Equals(other_city4)) {
//				other_city5 = PlayerLocation.Locations.surrounding (curr_city, -100, 0, -100, true, false);
//			}
//			QuarterFinalTeams [5] = (other_city5 + " " + animals [char.ToUpper (other_city5 [0]) - 65]);
//
//			GameObject.Find ("Team6").GetComponent<Text> ().text = (other_city5 + " " + animals [char.ToUpper (other_city5 [0]) - 65]);

//			Transform team6 = canvas.Find ("Team6");
//			Text setTeam6 = team6.GetComponent<Text> ();
//			setTeam6.text = (other_city5 + " " + animals [char.ToUpper (other_city5 [0]) - 65]);

//			string other_city6 = PlayerLocation.Locations.surrounding (curr_city, -10, -10, -10, true, true);
//			if (other_city6.Equals (other_city1) || other_city6.Equals(other_city2) || other_city6.Equals(other_city3)
//				|| other_city6.Equals(other_city4) || other_city6.Equals(other_city5)) {
//				other_city5 = PlayerLocation.Locations.surrounding (curr_city, -100, -100, -100, true, true);
//			}
//			QuarterFinalTeams [6] = (other_city6 + " " + animals [char.ToUpper (other_city6 [0]) - 65]);
//
//			GameObject.Find ("Team7").GetComponent<Text> ().text = (other_city6 + " " + animals [char.ToUpper (other_city6 [0]) - 65]);

//			Transform team7 = canvas.Find ("Team7");
//			Text setTeam7 = team7.GetComponent<Text> ();
//			setTeam7.text = (other_city6 + " " + animals [char.ToUpper (other_city6 [0]) - 65]);
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

	string[] newCoordinates(float lat, float longi, int km_offset1, int km_offset2) {

		string[] new_coords = new string[2];

		/* Convert offset (in km) to degrees. 
					1 degree in Google maps = 111.32 km */
		double degree1 = km_offset1 * (1 / 111.32);
		double degree2 = km_offset2 * (1 / 111.32);

		double lat2 = lat + degree1;
		/* longitude */
		new_coords[1] = (longi + degree2 / Mathf.Cos ((float) (lat2 * Mathf.PI / 180))).ToString ();
		/* latitude */
		new_coords[0] = lat2.ToString ();

		return new_coords;
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

        string winner2 = teams[Random.Range(0, 1+1)];
        string winner3 = teams[Random.Range(2, 3+1)];
        string winner4 = teams[Random.Range(4, 5+1)];

        return new string[] { winner2, winner3, winner4 };
    }
    string DecideSemiFinalWinner()
    {
        string[] quarterFinalWinners = DeserializeStringArray(PlayerPrefs.GetString("QuarterFinalWinners"));
        return quarterFinalWinners[Random.Range(1, 2 + 1)];
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
