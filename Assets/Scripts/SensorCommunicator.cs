using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class SensorCommunicator : MonoBehaviour
{
    public string StationID1, StationID2, StationID3, StationID4, StationID5, StationID6;
    // NOTE: PAT authentication is temprorary! This should
    // be updated to OAuth when we have the basic functionality
    // in place.
    public string PersonalAccessToken1, PersonalAccessToken2, PersonalAccessToken3, PersonalAccessToken4, PersonalAccessToken5, PersonalAccessToken6;
    private string URL;
    private Observation CurrentObservation;
    private int count = 0;
    private string StationID;
    private string PersonalAccessToken;
    public Text textElement;
    private List<Station> stations = new List<Station>();

    // Start is called before the first frame update
    void Start()
    {
        //Taking inputed ID's and PAT's and storing them in a list
        stations.Add(new Station() {id = StationID1, pat = PersonalAccessToken1});
        stations.Add(new Station() {id = StationID2, pat = PersonalAccessToken2});
        stations.Add(new Station() {id = StationID3, pat = PersonalAccessToken3});
        stations.Add(new Station() {id = StationID4, pat = PersonalAccessToken4});
        stations.Add(new Station() {id = StationID5, pat = PersonalAccessToken5});
        stations.Add(new Station() {id = StationID6, pat = PersonalAccessToken6});
        //Setting the first station's ID and PAT
        StationID = stations[0].id;
        PersonalAccessToken = stations[0].pat;


        InvokeRepeating("changeUI", 0.0f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if(Input.GetButton("space"))
        {
            changeUI();
        }
        */
           
    }

    //changes UI once per action function called
    void changeUI()
    {
        if(Input.GetButton("space"))
        {
            if(count>=stations.Count)
            {
                textElement.text = "";
                count = 0;
            }
            else
            {
                textElement.text = "";
                StationID = stations[count].id;
                PersonalAccessToken = stations[count].pat;
                URL = $"https://swd.weatherflow.com/swd/rest/observations/station/{StationID}?token={PersonalAccessToken}";
                GetNewObservation();
                count++;
            }
        }
    }

    public struct Station
    {
        //Station ID
        public string id;
        //Personal Access Token
        public string pat;
    }
    public Observation GetCurrentObservation()
    {
        return CurrentObservation;
    }

    private void GetNewObservation()
    {
        StartCoroutine(PollStation());
    }

    private IEnumerator PollStation()
    {
        UnityWebRequest request = UnityWebRequest.Get(URL);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success) 
        {
            Debug.Log(request.error);
        } 
        else 
        {
            string jsonData = request.downloadHandler.text;
            StationData currentData = JsonUtility.FromJson<StationData>(jsonData);
            CurrentObservation = currentData.obs[0];
            Debug.Log(currentData.station_name);
            textElement.text += $"Station: {currentData.station_name}\n";   
            textElement.text += $"Time Stamp: {CurrentObservation.timestamp}\n";
            textElement.text += $"Temperature: {CurrentObservation.air_temperature}\n";
            textElement.text += $"UV: {CurrentObservation.uv}\n";
            textElement.text += $"Wind Direction: {CurrentObservation.wind_direction}\n";
            foreach (var field in typeof(Observation).GetFields()) 
            {
                Debug.Log($"{field.Name}: {field.GetValue(CurrentObservation)}");
            }
        }
    }
    
    [System.Serializable]
    private class StationData
    {
        public string station_name;
        public List<Observation> obs;
    }

    [System.Serializable]
    public struct Observation
    {
        public float air_density;
        public float air_temperature;
        public float barometric_pressure;
        public float brightness;
        public float delta_t;
        public float dew_point;
        public float feels_like;
        public float lightning_strike_count;
        public float lightning_strike_count_last_1hr;
        public float lightning_strike_count_last_3hr;
        public float lightning_strike_last_distance;
        public float lightning_strike_last_epoch;
        public float precip;
        public float precip_accum_last_1hr;
        public float precip_accum_local_day;
        public float precip_accum_local_day_final;
        public float precip_accum_local_yesterday;
        public float precip_accum_local_yesterday_final;
        public float precip_analysis_type_yesterday;
        public float precip_minutes_local_day;
        public float precip_minutes_local_yesterday;
        public float precip_minutes_local_yesterday_final;
        public float pressure_trend;
        public float relative_humidity;
        public float sea_level_pressure;
        public float solar_radiation;
        public float station_pressure;
        public float timestamp;
        public float uv;
        public float wet_bulb_globe_temperature;
        public float wet_bulb_temperature;
        public float wind_avg;
        public float wind_chill;
        public float wind_direction;
        public float wind_gust;
        public float wind_lull;
    }
}
