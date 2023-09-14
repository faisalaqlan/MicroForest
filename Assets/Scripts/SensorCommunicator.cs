using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.InputSystem;


public class SensorCommunicator : MonoBehaviour
{
    private Observation CurrentObservation;
    private int CurrentStationIndex = 0;
    private Station CurrentStation;
    public bool IsToggled;

    XRIDefaultInputActions Actions;

    // TODO(raymond): Either add and orient a TextElement in the scene,
    // or go back to using TextMesh Pro.
    public Text textElement;

    [System.Serializable]
    struct Station
    {
        public string id;
        // NOTE(raymond): PAT authentication is temprorary! This should
        // be updated to OAuth when we have the basic functionality
        // in place.
        public string pat;
    }

    [SerializeField]
    List<Station> Stations = new List<Station>();

    void Awake()
    {
        Actions = new XRIDefaultInputActions();
        Actions.XRILeftHandInteraction.Enable();
        Actions.XRIRightHandInteraction.Enable();
        Actions.XRILeftHandInteraction.UIPress.performed += OnTriggerPress;
        Actions.XRILeftHandInteraction.Toggle.performed += OnTrackpadPress;
        Actions.XRIRightHandInteraction.UIPress.performed += OnTriggerPress;
        Actions.XRIRightHandInteraction.Toggle.performed += OnTrackpadPress;
    }

    // Start is called before the first frame update
    void Start()
    {
        CurrentStation = Stations[0];
        InvokeRepeating("RequestNewObservation", 0.0f, 60.0f);
    }

    // Update is called once per frame
    void Update()
    {
        // TODO(raymond): Bind input to XRI controller inputs.
        // NOTE(raymond): We should consider moving to the modern input system,
        // as the Input module is legacy functionality.
        textElement.enabled = IsToggled;
    }

    // NOTE(raymond): We need to wrap the coroutine in order to InvokeRepeating.
    private void RequestNewObservation()
    {
        StartCoroutine(PollStation());
    }

    public Observation GetCurrentObservation()
    {
        return CurrentObservation;
    }

    // NOTE(raymond): This might not need to be a coroutine, but given
    // that the speed of an HTTP request depends on connection quality,
    // it's a good idea to use one here (and it's also what the docs
    // recommends).
    private IEnumerator PollStation()
    {
        // NOTE(raymond): There is no need to make the URL as class member.
        // It is only ever used in this function.
        string URL = $"https://swd.weatherflow.com/swd/rest/observations/station/{CurrentStation.id}?token={CurrentStation.pat}";
        UnityWebRequest request = UnityWebRequest.Get(URL);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success) 
        {
            Debug.Log(request.error);
        } 
        else 
        {
            /* string text = ""; */
            string jsonData = request.downloadHandler.text;
            StationData currentData = JsonUtility.FromJson<StationData>(jsonData);
            CurrentObservation = currentData.obs[0];
            textElement.text = "Current Data\n";
            textElement.text += $"Station: {currentData.station_name}\n";   
            textElement.text += $"Time Stamp: {CurrentObservation.timestamp}\n";
            textElement.text += $"Temperature: {CurrentObservation.air_temperature}\n";
            textElement.text += $"UV: {CurrentObservation.uv}\n";
            textElement.text += $"Wind Direction: {CurrentObservation.wind_direction}\n";
            /* text += $"Station: {currentData.station_name}\n";    */
            /* text += $"Time Stamp: {CurrentObservation.timestamp}\n"; */
            /* text += $"Temperature: {CurrentObservation.air_temperature}\n"; */
            /* text += $"UV: {CurrentObservation.uv}\n"; */
            /* text += $"Wind Direction: {CurrentObservation.wind_direction}\n"; */
            /* foreach (var field in typeof(Observation).GetFields())  */
            /* { */
            /*     Debug.Log($"{field.Name}: {field.GetValue(CurrentObservation)}"); */
            /* } */
        }
    }

    private void OnTriggerPress(InputAction.CallbackContext context)
    {
        if (IsToggled)
        {
            CurrentStationIndex = (CurrentStationIndex + 1) % Stations.Count;
            CurrentStation = Stations[CurrentStationIndex];
            CancelInvoke("RequestNewObservation");
            InvokeRepeating("RequestNewObservation", 0.0f, 60.0f);
        }
    }
    
    private void OnTrackpadPress(InputAction.CallbackContext context)
    {
        IsToggled = !IsToggled;
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
