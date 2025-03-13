using UnityEngine;
using System.Collections.Generic; // For List<T>

public class ColorChanger : MonoBehaviour
{
    [SerializeField] private Renderer objectRenderer; // Assign in Inspector
    [SerializeField] private List<Color> colors = new List<Color>(); // Editable in Inspector
    private int currentIndex = 0; // Tracks current color index
    [SerializeField] private float changeInterval = 5f; // Time in seconds

    void Start()
    {
        // Auto-assign Renderer if not set
        if (objectRenderer == null)
        {
            objectRenderer = GetComponent<Renderer>();
        }

        // Start changing colors every few seconds
        InvokeRepeating(nameof(ChangeColor), changeInterval, changeInterval);
    }

    void ChangeColor()
    {
        if (colors.Count == 0 || objectRenderer == null) return; // Avoid errors

        currentIndex = (currentIndex + 1) % colors.Count; // Cycle through colors
        objectRenderer.material.color = colors[currentIndex]; // Apply color change

        Debug.Log("Changed color to: " + colors[currentIndex]);
    }
}
