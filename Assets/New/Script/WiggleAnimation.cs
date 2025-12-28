using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WiggleAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Wiggle());
    }

    IEnumerator Wiggle()
    {
        while (true)
        {
            // Set Z ke 1
            transform.eulerAngles = new Vector3(
                transform.eulerAngles.x,
                transform.eulerAngles.y,
                1
            );

            // Tunggu 1 detik
            yield return new WaitForSeconds(1f);

            // Set Z ke -1
            transform.eulerAngles = new Vector3(
                transform.eulerAngles.x,
                transform.eulerAngles.y,
                -1
            );

            // Tunggu 1 detik lagi
            yield return new WaitForSeconds(1f);

        }
    }
}
