using UnityEngine;
using System.Collections.Generic;

public class SevenSegmentDisplay : MonoBehaviour
{
    private Dictionary<string, GameObject> segments = new Dictionary<string, GameObject>();

    // Segment-Muster für Zahlen 0-9
    private Dictionary<int, bool[]> digitPatterns = new Dictionary<int, bool[]>()
    {
        { 0, new bool[] { true, true, true, true, true, true, false } },   // 0 → ABCDEF
        { 1, new bool[] { false, true, true, false, false, false, false } }, // 1 → BC
        { 2, new bool[] { true, true, false, true, true, false, true } },   // 2 → ABDEG
        { 3, new bool[] { true, true, true, true, false, false, true } },   // 3 → ABCDG
        { 4, new bool[] { false, true, true, false, false, true, true } },  // 4 → BCFG
        { 5, new bool[] { true, false, true, true, false, true, true } },   // 5 → ACDFG
        { 6, new bool[] { true, false, true, true, true, true, true } },    // 6 → ACDEFG
        { 7, new bool[] { true, true, true, false, false, false, false } }, // 7 → ABC
        { 8, new bool[] { true, true, true, true, true, true, true } },     // 8 → ABCDEFG
        { 9, new bool[] { true, true, true, true, false, true, true } }     // 9 → ABCFG
    };

    void Start()
    {
        // Finde alle Segmente in den Kindern und speichere sie
        foreach (Transform child in transform)
        {
            segments[child.name] = child.gameObject;
        }

        // Standardmäßig eine Zahl anzeigen
        SetNumber(0);
    }

    public void SetNumber(int number)
    {
        if (!digitPatterns.ContainsKey(number)) return;

        bool[] pattern = digitPatterns[number];

        string[] segmentNames = { "A", "B", "C", "D", "E", "F", "G" };

        for (int i = 0; i < segmentNames.Length; i++)
        {
            if (segments.ContainsKey(segmentNames[i]))
            {
                segments[segmentNames[i]].SetActive(pattern[i]);
            }
        }
    }
}
