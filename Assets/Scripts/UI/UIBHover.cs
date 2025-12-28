using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIBHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    //change Color
    public Image BGImage;
    public Color normalColor;
    public Color hoverColor;

    //glow
    public Outline outline;
    public Color glowColor = Color.cyan;
    public float pulseSpeed = 1f;

    //scale
    public float ScaleRate = 1.5f;


    private Color originalGlowColor;
    private bool isHovered = false;
    private Vector3 originalScale;

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovered = true;

        //change color
        if (BGImage != null) BGImage.color = hoverColor;

        transform.localScale = originalScale * ScaleRate;

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovered = false;

        //change color
        if (BGImage != null) BGImage.color = normalColor;

        transform.localScale = originalScale;
    }

    // Start is called before the first frame update
    void Start()
    {
        if(outline != null)
        {
            originalGlowColor = outline.effectColor;
        }

        originalScale = transform.localScale;

        if (BGImage != null)
        {
            BGImage.color = normalColor;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (outline != null)
        {
            if (isHovered)
            {
                float pulse = Mathf.PingPong(Time.time * pulseSpeed, 1f);
                outline.effectColor = Color.Lerp(originalGlowColor, glowColor, pulse);
            }
            else
            {
                //return to original color
                outline.effectColor = Color.Lerp(outline.effectColor, originalGlowColor, Time.deltaTime * 5f);
            }
        }
    }
}
