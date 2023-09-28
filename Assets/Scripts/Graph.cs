/*using UnityEngine;

public class LiveGraph : MonoBehaviour
{
    public Transform dotPrefab; // Prefab for the dot you want to display.
    public Transform graphParent; // Parent object to hold the dots.
    public SensorCommunicator sensorCommunicator; // Reference to the SensorCommunicator script.

    // Update is called once per frame.
    void Update()
    {
        if (sensorCommunicator != null)
        {
            float airTemperature = sensorCommunicator.CurrentObservation.air_temperature;
            //string stationName = sensorCommunicator.CurrentStation.id; // Use the station ID as the label.

            // Create or update a dot based on the data.
            Transform dot = Instantiate(dotPrefab, graphParent);
            dot.position = new Vector3(Time.time, airTemperature, 0); // Use time as x-axis, temperature as y-axis.

            TextMesh textMesh = dot.GetComponentInChildren<TextMesh>();
            // textMesh.text = stationName;
        }
    }
}
*/