using System.Collections;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public static MovingPlatform singleton;

    public float moveSpeed = 5f;
    public bool moveHorizontal = true;
    public bool moveVertical = false;
    public float maxDistance = 5f;

    private Vector3 startPosition;
    private bool movingForward = true;
    private Transform playerTransform;
    private Vector3 lastPosition;

    void Start()
    {
        startPosition = transform.position;
        lastPosition = transform.position;
        singleton = this;
    }

    void FixedUpdate()
    {
        // Simpan posisi sebelum bergerak
        Vector3 previousPosition = transform.position;

        // Hitung pergerakan
        Vector3 movement = CalculateMovement();

        // Terapkan pergerakan
        transform.position += movement;

        // Pastikan tidak melebihi batas
        EnsureWithinBounds();

        // Pindahkan player jika ada
        MovePlayerWithPlatform(previousPosition);

        // Simpan posisi terakhir
        lastPosition = transform.position;
    }

    Vector3 CalculateMovement()
    {
        Vector3 movement = Vector3.zero;
        float direction = movingForward ? 1 : -1;

        if (moveHorizontal)
        {
            movement.x = direction * moveSpeed * Time.fixedDeltaTime;
        }
        else if (moveVertical)
        {
            movement.y = direction * moveSpeed * Time.fixedDeltaTime;
        }

        return movement;
    }

    void EnsureWithinBounds()
    {
        Vector3 currentPosition = transform.position;
        float distanceFromStart = Vector3.Distance(currentPosition, startPosition);

        // Jika melebihi batas, koreksi posisi
        if (distanceFromStart > maxDistance)
        {
            Vector3 direction = (currentPosition - startPosition).normalized;
            transform.position = startPosition + direction * maxDistance;
            movingForward = !movingForward;
        }

        // Periksa apakah perlu membalik arah
        float currentAxisPosition = moveHorizontal ? transform.position.x : transform.position.y;
        float startAxisPosition = moveHorizontal ? startPosition.x : startPosition.y;

        if (Mathf.Abs(currentAxisPosition - startAxisPosition) >= maxDistance - 0.1f)
        {
            movingForward = !movingForward;
        }
    }

    void MovePlayerWithPlatform(Vector3 previousPosition)
    {
        if (playerTransform != null)
        {
            // Hitung pergerakan aktual platform
            Vector3 actualMovement = transform.position - previousPosition;

            // Terapkan ke player
            playerTransform.position += actualMovement;
        }
    }

    public void ResetPlatform()
    {
        // Koreksi posisi platform
        transform.position = startPosition;
        movingForward = true;

        // Koreksi posisi player relatif
        if (playerTransform != null)
        {
            // Hitung offset terakhir
            Vector3 offset = playerTransform.position - lastPosition;
            playerTransform.position = startPosition + offset;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerTransform = collision.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerTransform = null;
        }
    }
}