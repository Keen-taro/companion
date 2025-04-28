using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Keycode : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI Answer;
    public string key, secret_key;
    public bool isSolved = false;

    public void Start()
    {
        Answer.text = "";
    }

    public void Number(int number)
    {
        //Debug.Log(number + " was Pressed");
        if (Answer.text.Length <= 4)
        {
            Answer.text += number.ToString();
        }
    }

    public void Submit()
    {
        Debug.Log(Answer.text);
        StartCoroutine(CheckAnswerCoroutine());
    }

    public IEnumerator CheckAnswerCoroutine()
    {
        if(Answer.text == key && !isSolved)
        {
            Debug.Log("Correct Password");
            Answer.text = "VALID";
            isSolved = true;
            yield return new WaitForSeconds(1.0f);
            Answer.text = "";
        }
        else if (Answer.text == secret_key)
        {
            Debug.Log("???");
            Answer.text = "???";
            yield return new WaitForSeconds(1.0f);
            Answer.text = "";
        }
        else if (Answer.text == key && isSolved)
        {
            yield return new WaitForSeconds(1.0f);
            Answer.text = "";
        }
        else
        {
            Answer.text = "X X X";
            yield return new WaitForSeconds(1.0f);
            Answer.text = "";
        }
    }
}
