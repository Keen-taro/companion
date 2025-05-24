using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Piece : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public PuzzleManager manager;
    public Color pieceColor = Color.green;
    private Vector3 startPos;
    private Vector2 startAnchoredPos;
    private RectTransform rect;
    private RectTransform canvasRect;

    public Slot[] slotChildren;

    void Awake()
    {
        rect = GetComponent<RectTransform>();
        // Ambil Canvas RectTransform (asumsi parent teratas adalah Canvas)
        Canvas cv = GetComponentInParent<Canvas>();
        canvasRect = cv.transform as RectTransform;
        startAnchoredPos = rect.anchoredPosition;
        slotChildren = GetComponentsInChildren<Slot>();

        startPos = transform.position;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        slotChildren = GetComponentsInChildren<Slot>();
        manager.AssignSlotsFromChildren();
        manager.StartingPuzzle();
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Pindahkan RectTransform piece secara UI local
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRect,
            eventData.position,
            eventData.pressEventCamera,
            out Vector2 localPoint
        );
        rect.anchoredPosition = localPoint;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Hitung snapping di UI local space
        // 1. Dapatkan posisi center Piece dalam local space
        Vector2 center = Vector2.zero;
        foreach (Slot s in slotChildren)
            center += s.GetComponent<RectTransform>().anchoredPosition;
        center /= slotChildren.Length;

        // 2. Cari slot grid terdekat (konversi gridSlot world?local)
        Slot[] allGridSlots = manager.slotContainer.GetComponentsInChildren<Slot>();
        Slot bestCenter = null;
        float bestDist = float.MaxValue;
        foreach (Slot grid in allGridSlots)
        {
            Vector2 gridPos = grid.GetComponent<RectTransform>().anchoredPosition;
            float d = Vector2.Distance(gridPos, center);
            if (d < bestDist)
            {
                bestDist = d;
                bestCenter = grid;
            }
        }
        if (bestCenter == null || bestDist > 50f)
        { // threshold px
            rect.anchoredPosition = startAnchoredPos;
            return;
        }

        // 3. Hitung offset UI local
        Vector2 offset = bestCenter.GetComponent<RectTransform>().anchoredPosition - center;

        // 4. Cek tiap slot child
        List<Slot> targets = new List<Slot>();
        foreach (Slot child in slotChildren)
        {
            Vector2 childLocal = child.GetComponent<RectTransform>().anchoredPosition;
            Vector2 targetPos = childLocal + offset;

            // cari grid slot terdekat
            Slot nearest = null; float md = float.MaxValue;
            foreach (Slot grid in allGridSlots)
            {
                Vector2 gp = grid.GetComponent<RectTransform>().anchoredPosition;
                float d = Vector2.Distance(gp, targetPos);
                if (d < 30f && d < md) { md = d; nearest = grid; }
            }
            if (nearest != null && (nearest.state == SlotState.Empty || nearest.state == SlotState.Occupied))
                targets.Add(nearest);
            else
            {
                rect.anchoredPosition = startAnchoredPos;
                return;
            }
        }

        // 5. Snap dan occupy
        rect.anchoredPosition += offset;
        foreach (Slot t in targets)
            t.SetOccupied(pieceColor);

        startAnchoredPos = rect.anchoredPosition;
    }

    public void ResetToStart()
    {
        transform.position = startPos;
    }
}
