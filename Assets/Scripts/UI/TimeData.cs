using UnityEngine;
using TMPro;

[System.Serializable]
public struct TimeData
{
    public TextMeshProUGUI objectName;
    public GameObject linkedObject; // Reference ke GameObject asal
    public float timeValue;

}
