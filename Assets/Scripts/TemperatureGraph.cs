using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemperatureGraph : MonoBehaviour
{
    public LineRenderer temperatureLineRenderer;
    public float graphWidth = 10.0f; // Adjust the width of the graph as needed.

    void Start()
    {
        // You can initialize any required setup here.
    }

    public void DrawTemperatureGraph(List<float> temperatureData)
    {
        if (temperatureData.Count == 0)
        {
            Debug.LogWarning("Temperature data is empty.");
            return;
        }

        // Clear existing points
        temperatureLineRenderer.positionCount = 0;

        // Set the width of the line renderer
        temperatureLineRenderer.startWidth = 0.1f;
        temperatureLineRenderer.endWidth = 0.1f;

        // Calculate the spacing between points
        float pointSpacing = graphWidth / temperatureData.Count;

        // Iterate through the temperature data and draw points
        for (int i = 0; i < temperatureData.Count; i++)
        {
            float x = i * pointSpacing;
            float y = temperatureData[i];

            Vector3 point = new Vector3(x, y, 0f);

            // Add the point to the line renderer
            temperatureLineRenderer.positionCount++;
            temperatureLineRenderer.SetPosition(i, point);
        }
    }
}



