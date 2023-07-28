using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

public class SensorCommunicator : MonoBehaviour
{
    public string StationID;
    // NOTE(raymond): PAT authentication is temprorary! This should
    // be updated to OAuth when we have the basic functionality
    // in place.
    public string PersonalAccessToken;
    private string URL;
    private Observation CurrentObservation;

    [SerializeField]
    private TMP_Text SensorData;

    void Start()
    {
        URL = $"https://swd.weatherflow.com/swd/rest/observations/station/{StationID}?token={PersonalAccessToken}";
        InvokeRepeating("GetNewObservation", 0.0f, 60.0f);
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

        string newText = "";

        if (request.result != UnityWebRequest.Result.Success) {
            Debug.Log(request.error);
            newText += "Error: Could not access sensor data endpoint.";
        } else {
            string jsonData = request.downloadHandler.text;
            StationData currentData = JsonUtility.FromJson<StationData>(jsonData);
            CurrentObservation = currentData.obs[0];
            newText += $"Station: {currentData.station_name}\n";
            newText += $"Time Stamp: {CurrentObservation.timestamp}\n";
            newText += $"Temperature: {CurrentObservation.air_temperature} °C\n";
            newText += $"UV: {CurrentObservation.uv}\n";
            newText += $"Wind Direction: {CurrentObservation.wind_direction} °N\n";
        }
        SensorData.SetText(newText);
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
