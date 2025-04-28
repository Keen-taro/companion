using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Color standardColor; // Assign default color
    public Color hoverColor; // Assign hover color
    private Image imageColor;
    private Button button;

    private AudioSource cameraAudioSource;

    public AudioClip hoverAudio;
    public AudioClip clickAudio;

    private void Awake()
    {
        cameraAudioSource = GameObject.Find("ButtonAudioSource").GetComponent<AudioSource>();
    }

    void Start()
    {
        imageColor = GetComponent<Image>();
        button = GetComponent<Button>();
        imageColor.color = standardColor; // Initialize with the standard color
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (button != null && button.interactable)
        {
            imageColor.color = hoverColor;
            cameraAudioSource.PlayOneShot(hoverAudio);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (button != null && button.interactable)
        {
            imageColor.color = standardColor;
        }
    }

    public void SoundOnClick()
    {
        cameraAudioSource.PlayOneShot(clickAudio);
    }
}
