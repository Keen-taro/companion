using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;

public class LightBehaviour : MonoBehaviour
{
    public enum WispState
    {
        Idle,       // Diam & Floating
        Moving,     // Sedang bergerak ke target
        Charging    // Sedang di cawan
    }

    [Header("Status")]
    public WispState currentState = WispState.Idle;

    [Header("Floating Settings (Idle Only)")]
    public float floatSpeed = 2f;      // Kecepatan naik turun
    public float floatHeight = 0.2f;   // Seberapa tinggi naik turunnya

    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float stopDistance = 0.1f;

    [Header("References")]
    public Transform chargingStation;
    public Light2D wispLight;
    public Slider energySlider;

    [Header("Energy Stats")]
    public float maxEnergy = 100f;
    public float currentEnergy = 100f;
    public float drainRate = 5f;
    public float chargeRate = 30f;

    // Internal Variables
    private Vector2 targetPosition;
    private Vector2 baseIdlePosition; // Titik pusat saat floating

    void Start()
    {
        // Set posisi awal sebagai base idle
        baseIdlePosition = transform.position;
        currentState = WispState.Idle;
    }

    void Update()
    {
        HandleStateLogic();
        HandleEnergy();
        UpdateVisuals();
    }

    void HandleStateLogic()
    {
        switch (currentState)
        {
            case WispState.Idle:
                // Floating di tempat dia berhenti terakhir
                float idleY = baseIdlePosition.y + Mathf.Sin(Time.time * floatSpeed) * floatHeight;
                transform.position = new Vector2(baseIdlePosition.x, idleY);
                break;

            case WispState.Moving:
                transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

                if (Vector2.Distance(transform.position, targetPosition) < stopDistance)
                {
                    OnReachedDestination();
                }
                break;

            case WispState.Charging:
                // Floating saat charging (Sama persis dengan Idle)
                // Pastikan langkah No 1 di atas sudah dilakukan ya!
                float chargeY = baseIdlePosition.y + Mathf.Sin(Time.time * floatSpeed) * floatHeight;
                transform.position = new Vector2(baseIdlePosition.x, chargeY);
                break;
        }
    }

    void OnReachedDestination()
    {
        // PENTING: Cek dulu apakah Trigger sudah mengubah state jadi Charging?
        if (currentState == WispState.Charging)
        {
            return; // Keluar, jangan lakukan apa-apa (biarkan tetap charging)
        }

        // Kalau belum charging, berarti sampai di target biasa (Button/Glyph)
        Debug.Log("Sampai di Target Biasa -> Idle");
        currentState = WispState.Idle;

        // Update titik pusat floating ke posisi saat ini
        baseIdlePosition = transform.position;
    }

    // ================== PUBLIC COMMANDS ==================

    public void GoToTarget(Transform targetTransform)
    {
        if (targetTransform != null)
        {
            GoToPosition(targetTransform.position);
        }
    }

    public void GoToChargingStation()
    {
        if (chargingStation != null)
        {
            GoToPosition(chargingStation.position);
        }
    }

    public void GoToPosition(Vector3 newPos)
    {
        targetPosition = newPos;
        currentState = WispState.Moving; // Ganti state jadi Moving, otomatis floating berhenti
    }

    // ================== ENERGY & VISUALS ==================
    // (Sama seperti sebelumnya, tidak ada perubahan)
    void HandleEnergy()
    {
        // KONDISI 1: SEDANG CHARGING
        if (currentState == WispState.Charging)
        {
            currentEnergy += chargeRate * Time.deltaTime;

            if (currentEnergy > maxEnergy) currentEnergy = maxEnergy;

        }

        // KONDISI 2: SEDANG BERAKTIVITAS (Idle / Moving)
        else
        {
            currentEnergy -= drainRate * Time.deltaTime;

            // --- LOGIKA AUTO CHARGE (New) ---
            float threshold30 = maxEnergy * 0.3f; // Hitung 30% dari total energi

            if (currentEnergy <= threshold30)
            {
                // Cek: Jangan panggil kalau sudah OTW ke cawan 
                // (Kita cek jarak target tujuan kita dengan posisi cawan)
                bool isGoingToCharger = Vector2.Distance(targetPosition, chargingStation.position) < 1f;

                if (!isGoingToCharger)
                {
                    Debug.Log("Energi Kritis! Pulang ke Cawan...");
                    GoToChargingStation();
                }
            }

            // Batas bawah energi 0
            if (currentEnergy <= 0) currentEnergy = 0;
        }
    }

    void UpdateVisuals()
    {
        if (energySlider != null) energySlider.value = currentEnergy / maxEnergy;
        if (wispLight != null)
        {
            float targetIntensity = (currentEnergy / maxEnergy) * 2f;
            wispLight.intensity = Mathf.Lerp(wispLight.intensity, targetIntensity, Time.deltaTime * 5);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Pastikan yang disentuh adalah Cawan
        if (chargingStation != null && other.transform == chargingStation)
        {
            Debug.Log("Trigger Cawan Tersentuh! Mulai Charging.");

            // 1. Ubah State
            currentState = WispState.Charging;

            // 2. Snap posisi agar pas di tengah (biar rapi)
            transform.position = chargingStation.position;
            targetPosition = chargingStation.position; // Samakan target agar movement berhenti

            // 3. Set titik pusat floating
            baseIdlePosition = transform.position;
        }
    }
}