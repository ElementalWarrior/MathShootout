using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using System;
using System.Reflection;

public class Log {

    [Serializable]
    private class PostData
    {
        [SerializeField]
        public string session_id;
        [SerializeField]
        public string player_name;

        [SerializeField]
        private string date_session_start;

        public DateTime? date_session_start_public
        {
            set{
                date_session_start = value.ToString();
            }
        }
        [SerializeField]
        public string tag;

        [SerializeField]
        private string data;

        public object data_public
        {
            set
            {
                data = value.ToString();
            }
        }

        [SerializeField]
        private string date_created;

        public DateTime? date_created_public
        {
            set
            {
                date_created = value.ToString();
            }
        }

        public override string ToString()
        {
            string data = "{";
            foreach (PropertyInfo propertyInfo in this.GetType().GetProperties())
            {
                if (propertyInfo.CanRead)
                {
                    data += propertyInfo.Name + ": " + propertyInfo.GetValue(propertyInfo, null).ToString();
                }
            }
            return data + "}";
        }
    }
    public static void Submit(string tag, object value)
    {
        string sessionID = PlayerPrefs.GetString("session_id");
        string player_name = PlayerPrefs.GetString("TeamName", null);
        DateTime date_session_start = DateTime.Parse(PlayerPrefs.GetString("session_start")); 
        string postData = JsonUtility.ToJson( new PostData {

            session_id = sessionID
            , player_name = player_name ?? ""
            , date_session_start_public = date_session_start
            , tag = tag
            , data_public = value.ToString()
            ,
            date_created_public = DateTime.Now
        });
        Debug.Log("Logging: " + tag + ", " + postData);
        UnityWebRequest wr = UnityWebRequest.Put("localhost:8080/submit", postData);
        wr.method = "POST";
        wr.SetRequestHeader("Content-Type", "application/json");
        wr.Send();
    }
}
