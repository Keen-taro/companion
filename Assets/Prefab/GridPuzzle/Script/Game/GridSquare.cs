using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridSquare : MonoBehaviour
{
    public Image hoverImage;
    public Image activeImage;
    public Image normalImage;
    public List<Sprite> normalImages;
    [SerializeField] private Image feedbackImage; // Referensi ke UI Image

    public bool Selected { get; set; }
    public int SquareIndex { get; set; }
    public bool SquareOccupied;

    // Start is called before the first frame update
    void Start()
    {
        Selected = false;
        SquareOccupied = false;
    }

    //temp function. REMOVE
    public bool CanWeUseThisSquare()
    {
        return hoverImage.gameObject.activeSelf;
    }

    public void ActivateSquare()
    {
        hoverImage.gameObject.SetActive(false);
        activeImage.gameObject.SetActive(true);
        Selected = true;
        SquareOccupied = true;
    }

    public void ResetGridSquare()
    {
        SquareOccupied = false;
        Selected = false;
        normalImage.gameObject.SetActive(true);
        hoverImage.gameObject.SetActive(false);
        activeImage.gameObject.SetActive(false);
    }

    public void SetImage(bool setFirstImage)
    {
        normalImage.GetComponent<Image>().sprite = setFirstImage ? normalImages[1] : normalImages[0];
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        hoverImage.gameObject.SetActive(true);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        hoverImage.gameObject.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        hoverImage.gameObject.SetActive(false);
    }

    public void ShowCorrectFeedback()
    {
        feedbackImage.color = Color.green;
        StartCoroutine(ResetFeedback());
    }

    public void ShowIncorrectFeedback()
    {
        feedbackImage.color = Color.red;
        StartCoroutine(ResetFeedback());
    }

    private IEnumerator ResetFeedback()
    {
        yield return new WaitForSeconds(0.5f);
        feedbackImage.color = Color.white;
    }
}
