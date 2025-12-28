using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;

public class TriggerEvent : MonoBehaviour
{
    public UnityEvent EventOnTrigger;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (EventOnTrigger != null) EventOnTrigger.Invoke();
        }
    }
}
