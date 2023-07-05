using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SensorTest : MonoBehaviour
{
    public string StationID;
    // NOTE: PAT authentication is temprorary! This should
    // be updated to OAuth when we have the basic functionality
    // in place.
    public string PersonalAccessToken;
    private string URL;

    // Start is called before the first frame update
    void Start()
    {
        URL = $"https://swd.weatherflow.com/swd/rest/observations/station/{StationID}?token={PersonalAccessToken}";
        Debug.Log(URL);
        StartCoroutine(GetText());
    }

    IEnumerator GetText()
    {
        UnityWebRequest request = UnityWebRequest.Get(URL);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success) {
            Debug.Log(request.error);
        } else {
            Debug.Log(request.downloadHandler.text);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
