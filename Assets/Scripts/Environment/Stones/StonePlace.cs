using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StonePlace : MonoBehaviour
{
    public static StonePlace Instance { get; private set; }


    [SerializeField] private string stoneElementCorrect; // The correct element for this stone place
    [SerializeField] private Transform fixedStonePosition; // Fixed position to place the stone
    private SpriteRenderer placeSpriteRenderer;

    public Stone assignedStone; // Reference to the currently assigned stone;
    public bool isStonePlaced = false; // Flag to check if a stone is already placed
    public bool isComplete = false;

    private void Awake()
    {
        Instance = this;
        placeSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        StonePlacingCheck();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (isStonePlaced)
        {
            return; // Exit if a stone is already placed
        }

        if (other.CompareTag("Stone"))
        {
            assignedStone = other.GetComponent<Stone>();
            Vector3 position = fixedStonePosition.position;
            other.transform.position = position; // Move stone to fixed position

            if (assignedStone != null)
            {
                CheckAssignStone();
            }
        }
    }

    public void CheckAssignStone()
    {
        isStonePlaced = true;

        if (stoneElementCorrect != assignedStone.stoneElement)
        {
            Debug.Log("Stone placed in incorrect position.");
        }
        else
        {
            Debug.Log("Stone placed in correct position.");
            isComplete = true;
            CheckElementAnswer();
            StartCoroutine(WaitBeforeDeactivateScript());
        }
    }

    public void StonePlacingCheck()
    {
        if (isStonePlaced && assignedStone != null)
        {
            assignedStone = null;
            isStonePlaced = false;
        }
    }

    IEnumerator WaitBeforeDeactivateScript()
    {
        yield return new WaitForSeconds(5f);
        this.enabled = false;

    }

    public void CheckElementAnswer()
    {
        if (stoneElementCorrect == "SunStone")
        {
            placeSpriteRenderer.color = new Color32(255, 215, 0, 255);
        }
        else if (stoneElementCorrect == "WaterStone")
        {
            placeSpriteRenderer.color = new Color32(30, 144, 255, 255);
        }
        else if (stoneElementCorrect == "FireStone")
        {
            placeSpriteRenderer.color = new Color32(255, 69, 0, 255);
        }
        else if (stoneElementCorrect == "LifeStone")
        {
            placeSpriteRenderer.color = new Color32(50, 205, 50, 255);
        }
        else if (stoneElementCorrect == "EarthStone")
        {
            placeSpriteRenderer.color = new Color32(139, 69, 19, 255);
        }
        else if (stoneElementCorrect == "WindStone")
        {
            placeSpriteRenderer.color = new Color32(135, 206, 235, 255);
        }
        else if (stoneElementCorrect == "MountainStone")
        {
            placeSpriteRenderer.color = new Color32(128, 128, 128, 255);
        }
        else
        {
            placeSpriteRenderer.color = new Color32(138, 43, 226, 255);
        }

    }

    public bool CheckIfComplete()
    {
        return isComplete;
    }
}