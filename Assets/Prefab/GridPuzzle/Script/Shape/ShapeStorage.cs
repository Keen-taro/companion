using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeStorage : MonoBehaviour
{
    public List<ShapeData> shapeData;  // List data bentuk (ShapeData)
    public List<Shape> shapeList;      // List objek Shape di scene

    public Vector3[] defaultPositions;

    void Start()
    {
        defaultPositions = new Vector3[shapeList.Count];
        // Pastikan jumlah Shape dan ShapeData sama
        if (shapeList.Count != shapeData.Count)
        {
            Debug.LogError("Jumlah Shape dan ShapeData tidak sama!");
            return;
        }

        // Pasangkan Shape[0] dengan ShapeData[0], Shape[1] dengan ShapeData[1], dst.
        for (int i = 0; i < shapeList.Count; i++)
        {
            shapeList[i].CreateShape(shapeData[i]);
            defaultPositions[i] = shapeList[i].transform.localPosition;
        }
    }

    public Shape GetCurrentSelectedShape()
    {
        foreach (var shape in shapeList)
        {
            if (!shape.IsOnStartPosition() && shape.isAnyOfShapeSquareActive())
                return shape;
        }

        Debug.LogError("Tidak ada Shape yang dipilih!");
        return null;
    }

    public void ResetAllShapes()
    {
        for (int i = 0; i < shapeList.Count; i++)
        {
            if (shapeList[i] != null) // Pastikan shape belum di-destroy
            {
                shapeList[i].ResetShape(defaultPositions[i]);
                shapeList[i].gameObject.SetActive(true); // Force activate
            }
        }
    }
}
