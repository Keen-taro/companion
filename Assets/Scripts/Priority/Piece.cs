using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Piece : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public PuzzleManager manager;
    public Color pieceColor = Color.green;
    private Vector3 startPos;

    public Slot[] slotChildren;

    public void Awake()
    {
        slotChildren = GetComponentsInChildren<Slot>();
        startPos = transform.position;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            transform.Rotate(0, 0, -90);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        manager.AssignSlotsFromChildren(); // Refresh status slot
        manager.StartingPuzzle();
    }

    public void OnDrag(PointerEventData eventData)
    {
        RectTransform rt = GetComponent<RectTransform>();
        Vector3 globalMousePos;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(rt, eventData.position, eventData.pressEventCamera, out globalMousePos))
        {
            transform.position = globalMousePos;
        }
    }


    public void OnEndDrag(PointerEventData eventData)
    {
        if (slotChildren == null || slotChildren.Length == 0)
        {
            Debug.LogError("Piece has no slot children assigned!");
            ResetToStart();
            return;
        }

        List<Slot> targetSlots = new List<Slot>();
        bool allValid = true;

        // Get all grid slots once for performance
        Slot[] allGridSlots = manager.GetComponentsInChildren<Slot>();

        // Calculate piece center in world space
        Vector3 pieceCenter = Vector3.zero;
        foreach (Slot child in slotChildren)
        {
            pieceCenter += child.transform.position;
        }
        pieceCenter /= slotChildren.Length;

        // Find nearest grid slot to piece center
        Slot nearestCenterSlot = null;
        float minCenterDist = float.MaxValue;

        foreach (Slot gridSlot in allGridSlots)
        {
            float dist = Vector3.Distance(pieceCenter, gridSlot.transform.position);
            if (dist < minCenterDist)
            {
                minCenterDist = dist;
                nearestCenterSlot = gridSlot;
            }
        }

        if (nearestCenterSlot == null)
        {
            ResetToStart();
            return;
        }

        // Calculate how much to move the piece to align centers
        Vector3 centerOffset = nearestCenterSlot.transform.position - pieceCenter;

        // Verify all child slots would align with grid slots
        foreach (Slot childSlot in slotChildren)
        {
            Vector3 targetPos = childSlot.transform.position + centerOffset;
            Slot nearestSlot = null;
            float minDist = float.MaxValue;

            foreach (Slot gridSlot in allGridSlots)
            {
                float dist = Vector3.Distance(targetPos, gridSlot.transform.position);
                if (dist < 0.5f && dist < minDist) // 0.5f is world unit threshold
                {
                    minDist = dist;
                    nearestSlot = gridSlot;
                }
            }

            if (nearestSlot != null &&
               (nearestSlot.state == SlotState.Empty || nearestSlot.state == SlotState.Occupied))
            {
                targetSlots.Add(nearestSlot);
            }
            else
            {
                allValid = false;
                break;
            }
        }

        if (allValid && targetSlots.Count == slotChildren.Length)
        {
            // Move piece to align centers
            transform.position += centerOffset;

            // Occupy all target slots
            foreach (Slot slot in targetSlots)
            {
                slot.SetOccupied(pieceColor);
            }
        }
        else
        {
            ResetToStart();
        }
    }

    public void ResetToStart()
    {
        transform.position = startPos;
    }
}
