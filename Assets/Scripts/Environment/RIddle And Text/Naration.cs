using UnityEngine;

[CreateAssetMenu(fileName = "New Thought", menuName = "Data/Thought")]
public class Naration : ScriptableObject
{
    [TextArea]
    public string narationText;
}

