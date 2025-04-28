using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OracleManager : MonoBehaviour
{
    private List<Oracle> selectedOracles = new List<Oracle>();  // List to hold selected oracles
    [SerializeField] private List<Oracle> answer = new List<Oracle>();

    [SerializeField] GameObject BubbleMemory;

    [SerializeField] int totalOracle;
    bool isComplete;
    int litOracle;

    private void Awake()
    {
        isComplete = false;
        litOracle = 0;
    }

    /*
    private void Start()
    {
        StartCoroutine(EnableColliderBoxAfterIntro());
    }
    */

    private void Update()
    {
        if (isComplete) return;

        if (litOracle == totalOracle)
        {
            CheckPuzzleCompletion();
        }

        if (isComplete)
        {
            DisabelOracleCollision();
        }
    }

    // Add the oracle to the list when it's turned on
    public void AddOracle(Oracle oracle)
    {
        if (!selectedOracles.Contains(oracle))
        {
            selectedOracles.Add(oracle);  // Add to the end of the list (last position)
            litOracle++;
            Debug.Log("Oracle added: " + oracle.oracleID);
        }
    }

    // Remove the oracle from the list when it's turned off
    public void RemoveOracle(int oracleID)
    {
        Oracle oracleToRemove = selectedOracles.Find(oracle => oracle.oracleID == oracleID);
        if (oracleToRemove != null)
        {
            selectedOracles.Remove(oracleToRemove);
            litOracle--;
            Debug.Log("Oracle removed: " + oracleID);
        }
    }

    // This is where you can handle the logic after the puzzle is solved, based on the selectedOracles array
    public void CheckPuzzleCompletion()
    {
        //Debug.Log("Checked");

        // Example: Check if the correct oracles are selected in the correct order
        if (selectedOracles.Count == answer.Count)
        {
            bool isCorrect = true;

            for (int i = 0; i < answer.Count; i++)
            {
                if (selectedOracles[i].oracleID != answer[i].oracleID)
                {
                    isCorrect = false;
                    break; // Exit early if a mismatch is found
                }
            }

            if (isCorrect)
            {
                Debug.Log("Puzzle solved!");
                DisabelOracleCollision();
                BubbleMemory.SetActive(true);
                StartCoroutine(MoveUpCoroutine(2));
                isComplete = true;
                // Add success logic here (e.g., trigger next level, reward player)
            }
            else
            {
                Debug.Log("Didnt match");
                foreach (Oracle oracle in selectedOracles)
                {
                    oracle.Reset();
                }
                StartCoroutine(EnableColliderBoxAfterIntro());
                litOracle = 0;
                selectedOracles.Clear();
            }
        }

        Debug.Log("End of Function");
        // This could be based on the order of oracleIDs in the array, etc.
        //Debug.Log("Checking puzzle with " + selectedOracles.Count + " oracles selected.");
    }

    public void DisabelOracleCollision()
    {
        foreach (Oracle oracle in answer)
        {
            oracle.colliderBox.enabled = false;
        }
    }

    IEnumerator StartIntroAnimation()
    {
        DisabelOracleCollision();

        yield return new WaitForSeconds(3f); // Wait for the delay

        foreach (Oracle oracle in answer)
        {
            oracle.particleEffect.Play();
            oracle.PlayLitSound();
            yield return new WaitForSeconds(1f);
            oracle.particleEffect.Stop();
            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator EnableColliderBoxAfterIntro()
    {
        yield return StartIntroAnimation();

        yield return new WaitForSeconds(1f);

        foreach (Oracle oracle in answer)
        {
            Debug.Log(oracle.oracleID + " Collider has been activated");
            oracle.colliderBox.enabled = true;
        }
    }

    public void StartPuzzle()
    {
        StartCoroutine(EnableColliderBoxAfterIntro());
    }

    public bool CheckIfComplete()
    {
        return isComplete;
    }

    public void MoveBubbleMemoryUp(float duration)
    {
        StartCoroutine(MoveUpCoroutine(duration));
    }

    private IEnumerator MoveUpCoroutine(float duration)
    {
        Vector3 startPosition = BubbleMemory.transform.position;
        Vector3 endPosition = startPosition + new Vector3(0, 2, 0);
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            BubbleMemory.transform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null; // Wait for the next frame
        }

        BubbleMemory.transform.position = endPosition; // Ensure it reaches the final position
    }
}



