using UnityEngine;

public class GlyphClickSender : MonoBehaviour
{
    // Drag Wisp (LightBehaviour) ke sini di Inspector
    public LightBehaviour wispScript;

    private void OnMouseDown()
    {
        if (wispScript != null)
        {
            // "Hei Wisp, targetnya adalah SAYA (transform ini)"
            wispScript.GoToPosition(transform.position);
        }
    }
}