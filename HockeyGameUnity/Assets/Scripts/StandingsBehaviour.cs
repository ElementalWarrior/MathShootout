using System.Collections;
using System.Collections.Generic;
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
    GameObject[] SemiFinalLabels;
    GameObject[] FinalistLabels;

    public Round CurrentRound;

	public bool location_on;

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

	// Use this for initialization
	IEnumerator Start () {
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

		QuarterFinalLabels = new GameObject[7];

		/* User does have location service on */
		location_on = true;
		Input.location.Start();

		int start_time = 0;

		/* Wait up to 20 seconds for location services to initialize */
		while ((Input.location.status == LocationServiceStatus.Initializing) && (start_time <= 20)) {
			yield return new WaitForSeconds(1);
			start_time++;
		}

		if (Input.location.status != LocationServiceStatus.Running) {
			location_on = false;
		}

		/* Location services are not enabled */
		if (!location_on) {
			QuarterFinalLabels [0] = canvas.Find ("Team1").gameObject;
			QuarterFinalLabels [1] = canvas.Find ("Team2").gameObject;
			QuarterFinalLabels [2] = canvas.Find ("Team3").gameObject;
			QuarterFinalLabels [3] = canvas.Find ("Team4").gameObject;
			QuarterFinalLabels [4] = canvas.Find ("Team5").gameObject;
			QuarterFinalLabels [5] = canvas.Find ("Team6").gameObject;
			QuarterFinalLabels [6] = canvas.Find ("Team7").gameObject;
		} 

		/* Location services enabled */
		else {
			PlayerLocation.Locations.locate (location_on, 0, 0);
			string curr_city = PlayerLocation.Locations.city;
			GameObject label = QuarterFinalLabels[0];
			label.GetComponent<Text>().text = (curr_city + " " + animals [char.ToUpper (curr_city [0]) - 65]);

			string other_city = PlayerLocation.Locations.surrounding (location_on, curr_city, 0, 10, 10, false, true);
			label = QuarterFinalLabels[1];
			label.GetComponent<Text>().text = other_city + " " + animals [char.ToUpper (other_city [0]) - 65];

			other_city = PlayerLocation.Locations.surrounding (location_on, curr_city, 10, 10, 10, true, true);
			label = QuarterFinalLabels[2];
			label.GetComponent<Text>().text = other_city + " " + animals [char.ToUpper (other_city [0]) - 65];

			other_city = PlayerLocation.Locations.surrounding (location_on, curr_city, 10, 0, 10, true, false);
			label = QuarterFinalLabels[3];
			label.GetComponent<Text>().text = other_city + " " + animals [char.ToUpper (other_city [0]) - 65];

			other_city = PlayerLocation.Locations.surrounding (location_on, curr_city, 0, -10, -10, false, true);
			label = QuarterFinalLabels[4];
			label.GetComponent<Text>().text = other_city + " " + animals [char.ToUpper (other_city [0]) - 65];

			other_city = PlayerLocation.Locations.surrounding (location_on, curr_city, -10, 0, -10, true, false);
			label = QuarterFinalLabels[5];
			label.GetComponent<Text>().text = other_city + " " + animals [char.ToUpper (other_city [0]) - 65];

			other_city = PlayerLocation.Locations.surrounding (location_on, curr_city, -10, -10, -10, true, true);
			label = QuarterFinalLabels[6];
			label.GetComponent<Text>().text = other_city + " " + animals [char.ToUpper (other_city [0]) - 65];
		}

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

		/* User does not have location services on */
		if (!Input.location.isEnabledByUser) {
//			location_on = false;
//			yield break;
		}

		/* User does have location service on */
		Input.location.Start();

		start_time = 0;

		/* Wait up to 20 seconds for location services to initialize */
		while ((Input.location.status == LocationServiceStatus.Initializing) && (start_time <= 20)) {
			yield return new WaitForSeconds(1);
			start_time++;
		}

		/* Initialization failed or timed-out */
		if (Input.location.status != LocationServiceStatus.Running) {
			location_on = false;
			yield break;
		} 

		/* Location service initialization was successful */
		else {
			location_on = true;
		}
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
		if (!location_on) {
			teams [0] = "East Coast Eagles";
			teams [1] = "Northern Owls";
			teams [2] = "Southern Snakes";
			teams [3] = "Midwest Minstrels";
			teams [4] = "Cape Camels";
			teams [5] = "Gulf Gophers";
		}

		/* Else player has location services on */
		else {
			PlayerLocation.Locations.locate (location_on, 0, 0);
			string curr_city = PlayerLocation.Locations.city;
			teams [0] = curr_city + " " + animals [char.ToUpper (curr_city [0]) - 65];

			string other_city = PlayerLocation.Locations.surrounding (location_on, curr_city, 0, 10, 10, false, true);
			teams [1] = other_city + " " + animals [char.ToUpper (other_city [0]) - 65];
		
			other_city = PlayerLocation.Locations.surrounding (location_on, curr_city, 10, 10, 10, true, true);
			teams [2] = other_city + " " + animals [char.ToUpper (other_city [0]) - 65];

			other_city = PlayerLocation.Locations.surrounding (location_on, curr_city, 10, 0, 10, true, false);
			teams [3] = other_city + " " + animals [char.ToUpper (other_city [0]) - 65];

			other_city = PlayerLocation.Locations.surrounding (location_on, curr_city, 0, -10, -10, false, true);
			teams [4] = other_city + " " + animals [char.ToUpper (other_city [0]) - 65];

			other_city = PlayerLocation.Locations.surrounding (location_on, curr_city, -10, 0, -10, true, false);
			teams [5] = other_city + " " + animals [char.ToUpper (other_city [0]) - 65];

			other_city = PlayerLocation.Locations.surrounding (location_on, curr_city, -10, -10, -10, true, true);
			teams [6] = other_city + " " + animals [char.ToUpper (other_city [0]) - 65];
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
}
