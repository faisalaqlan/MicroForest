
using UnityEngine;
using UnityEngine.UI;

public class TemperatureGraph : MonoBehaviour
{
    public Image temperatureLine;
    public Text yAxisLabel;
    public float graphHeight = 100.0f; // Adjust this value based on your graph's height.

    // Update the graph with temperature data.
    public void UpdateGraph(float normalizedTemperature)
    {
        // Adjust the size and position of the temperature line based on the normalized value.
        RectTransform lineRect = temperatureLine.rectTransform;
        lineRect.sizeDelta = new Vector2(lineRect.sizeDelta.x, normalizedTemperature * graphHeight);
        lineRect.anchoredPosition = new Vector2(lineRect.anchoredPosition.x, normalizedTemperature * graphHeight / 2);

        // Update labels, e.g., on the y-axis, to reflect the temperature range.
    }
}

