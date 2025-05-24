using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static Action CheckIfShapeCanBePlaced;
    public static event Action OnPuzzleSolved;
    public static event Action<int> OnPuzzleFailed;

    public static void InvokeCheckIfShapeCanBePlaced()
    {
        CheckIfShapeCanBePlaced?.Invoke();
    }
}
