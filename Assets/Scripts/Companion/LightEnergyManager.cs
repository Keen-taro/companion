using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightEnergyManager : MonoBehaviour
{
    [Header("Settings")]
    public float maxEnergy = 100f;
    public float currentEnergy;
    public float drainRate = 5f; // Berapa cepat energi habis per detik
    public float rechargeRate = 20f; // Kecepatan isi ulang

    [Header("UI (Optional)")]
    public Slider energySlider;

    private void Start()
    {
        currentEnergy = maxEnergy;
    }

    private void Update()
    {
        // Update UI jika ada
        if (energySlider != null)
        {
            energySlider.value = currentEnergy / maxEnergy;
        }
    }

    // Fungsi untuk mengurangi energi (dipanggil saat nyala terang)
    public void DrainEnergy()
    {
        currentEnergy -= drainRate * Time.deltaTime;
        currentEnergy = Mathf.Clamp(currentEnergy, 0, maxEnergy);
    }

    // Fungsi isi ulang (dipanggil saat dekat Cawan)
    public void Recharge()
    {
        currentEnergy += rechargeRate * Time.deltaTime;
        currentEnergy = Mathf.Clamp(currentEnergy, 0, maxEnergy);
    }

    // Mengembalikan persentase energi (0.0 sampai 1.0)
    public float GetEnergyPercent()
    {
        return currentEnergy / maxEnergy;
    }
}
