using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;


public class Manager : MonoBehaviour
{
    public GameObject prefab; // Prefab mit den Segmenten
    public Button myButton; // UI-Button für Rotation
    private bool displayActive = false; // Anzeige an/aus
    public Canvas myCanvas;
    public GameObject myPanel;

    private Dictionary<string, GameObject> segments = new Dictionary<string, GameObject>();

    void Start()
    {
        // Button-Listener hinzufügen
        if (myButton != null)
        {
            myButton.onClick.AddListener(ActivateDisplay);
        }
        else
        {
            Debug.LogError("Button nicht zugewiesen!");
        }

        // Segmente aus dem Prefab sammeln (A-G)
        foreach (Transform child in prefab.transform)
        {
            segments[child.name] = child.gameObject;
        }

        // Standardmäßig 0 anzeigen
        DrawDigit(0);
    }

    void Update()
    {
        myButton.gameObject.SetActive(true); // Fehler: .SetActive gibt es nur für GameObjects
        myCanvas.gameObject.SetActive(true);
        myPanel.SetActive(true); // myPanel muss im Inspector zugewiesen sein!

        // Prüfen, ob eine Zahlentaste gedrückt wurde (0-9)
        for (int i = 0; i <= 9; i++)
        {
            if (Input.GetKeyDown((KeyCode)(i + 48))) // 48 = KeyCode.Alpha0
            {
                DrawDigit(i);
            }
        }
    }


    void ActivateDisplay()
    {
        Debug.Log("Button wurde gedrückt!"); // Debugging
        displayActive = !displayActive;
        RotateDisplay();
    }

    void RotateDisplay()
    {
        if (prefab != null)
        {
            prefab.transform.Rotate(0, 180, 0);
        }
        else
        {
            Debug.LogError("Prefab nicht zugewiesen!");
        }
    }

    void DrawDigit(int digit)
    {
        // Alle Segmente deaktivieren
        foreach (var segment in segments.Values)
        {
            segment.SetActive(false);
        }

        // Segment-Mappings für jede Zahl (A-G)
        Dictionary<int, string[]> digitSegments = new Dictionary<int, string[]>
        {
            { 0, new string[] { "A", "B", "C", "D", "E", "F" } },      // 0
            { 1, new string[] { "B", "C" } },                         // 1
            { 2, new string[] { "A", "B", "G", "E", "D" } },          // 2
            { 3, new string[] { "A", "B", "G", "C", "D" } },          // 3
            { 4, new string[] { "F", "G", "B", "C" } },               // 4
            { 5, new string[] { "A", "F", "G", "C", "D" } },          // 5
            { 6, new string[] { "A", "F", "E", "D", "C", "G" } },     // 6
            { 7, new string[] { "A", "B", "C" } },                    // 7
            { 8, new string[] { "A", "B", "C", "D", "E", "F", "G" } },// 8
            { 9, new string[] { "A", "B", "C", "D", "F", "G" } }      // 9
        };

        // Aktiviert die Segmente für die gewählte Zahl
        if (digitSegments.ContainsKey(digit))
        {
            foreach (string segment in digitSegments[digit])
            {
                if (segments.ContainsKey(segment))
                {
                    segments[segment].SetActive(true);
                }
            }
        }
    }
}
