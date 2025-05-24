using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Shape : MonoBehaviour, IPointerClickHandler, IPointerUpHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, 
    IPointerDownHandler

{
    public GameObject squareShapeImage;
    public Vector3 shapeSelectedScale;
    public Vector2 offset = new Vector2(0f, 700f);

    [HideInInspector]
    public ShapeData CurrentShape;

    private List<GameObject> _currentShape = new List<GameObject>();
    private Vector3 _shapeStartScale;
    private RectTransform _transform;
    private bool _shapeDraggable = true;
    private Canvas _canvas;
    private Vector3 _startPosition;
    private bool _shapeActive = true;
    public bool IsBeingReset { get; private set; } // Flag baru

    private void Awake()
    {
        _shapeStartScale = this.GetComponent<RectTransform>().localScale;
        _transform = this.GetComponent<RectTransform>();
        _canvas = GetComponentInParent<Canvas>();
        _shapeDraggable = true;
        _startPosition = transform.localPosition;
        _shapeActive = true;
    }

    void Start()
    {

    }

    public bool IsOnStartPosition()
    {
        return _transform.localPosition == _startPosition;
    }


    public bool isAnyOfShapeSquareActive()
    {
        foreach (var square in _currentShape)
        {
            if (square.gameObject.activeSelf)
                return true;
        }

        return false;
    }

    public bool IsShapeValidPosition()
    {
        foreach (var square in _currentShape)
        {
            if (!square.activeSelf) continue;

            bool isSquareOnAnyGrid = false;
            Collider2D[] colliders = Physics2D.OverlapPointAll(square.transform.position);

            foreach (var collider in colliders)
            {
                GridSquare gridSquare = collider.GetComponent<GridSquare>();
                if (gridSquare != null)
                {
                    isSquareOnAnyGrid = true;
                    break; // Cukup menemukan 1 grid yang valid
                }
            }

            // Jika ada square yang tidak menempel grid sama sekali, shape tidak valid
            if (!isSquareOnAnyGrid) return false;
        }
        return true; // Semua square menempel di suatu grid (occupied atau tidak)
    }

    public void DeactivateShape()
    {
        if (_shapeActive)
        {
            foreach (var square in _currentShape)
            {
                square?.GetComponent<ShapeSquare>().DeactivateShape();
            }
        }
        _shapeActive = false;
    }

    public void ActivateShape()
    {
        if (!_shapeActive)
        {
            foreach (var square in _currentShape)
            {
                square?.GetComponent<ShapeSquare>().ActivateShape();
            }
        }
        _shapeActive = true;
    }

    public void RequestNewShape(ShapeData shapeData)
    {
        transform.localPosition = _startPosition;
        CreateShape(shapeData);
    }

    /*
    public void CreateShape(ShapeData shapeData)
    {
        CurrentShape = shapeData;
        var totalSquareNumber = GetNumberOfSquares(shapeData);

        while (_currentShape.Count <= totalSquareNumber)
        {
            _currentShape.Add(Instantiate(squareShapeImage, transform) as GameObject);
        }

        foreach (var square in _currentShape)
        {
            square.gameObject.transform.position = Vector3.zero;
            square.gameObject.SetActive(false);
        }

        var squareRect = squareShapeImage.GetComponent<RectTransform>();
        var moveDistance = new Vector2(squareRect.rect.width * squareRect.localScale.x,
            squareRect.rect.height * squareRect.localScale.y);

        int currentIndexInList = 0;

        // Set pos to from final shape
        for (var row = 0; row < shapeData.rows; row++)
        {
            for (var column = 0; column < shapeData.columns; column++)
            {
                if (shapeData.board[row].column[column])
                {
                    _currentShape[currentIndexInList].SetActive(true);
                    _currentShape[currentIndexInList].GetComponent<RectTransform>().localPosition = 
                        new Vector2(GetXPositionForShapeSquare(shapeData, column, moveDistance), 
                            GetYPositionForShapeSquare(shapeData, row, moveDistance)) ;

                    currentIndexInList++;
                }
            }
        }
    }

    
    private float GetYPositionForShapeSquare(ShapeData shapeData, int row, Vector2 moveDistance)
    {
        float shiftOnY = 0f;

        if (shapeData.rows > 1)
        {
            if (shapeData.rows % 2 != 0)
            {
                var middleSquareIndex = (shapeData.rows - 1) / 2;
                var multiplier = (shapeData.rows - 1) / 2;

                if (row < middleSquareIndex) //move to minus
                {
                    shiftOnY = moveDistance.y * 1;
                    shiftOnY *= multiplier;
                }
                else if (row > middleSquareIndex) // move it to plus
                {
                    shiftOnY = moveDistance.y * -1;
                    shiftOnY *= multiplier;
                }
            }
            else
            {
                var middleSquareIndex2 = (shapeData.rows == 2) ? 1 : (shapeData.rows / 2);
                var middleSquareIndex1 = (shapeData.rows == 2) ? 0 : (shapeData.rows - 2);
                var multiplier = shapeData.rows / 2;

                if (row == middleSquareIndex1 || row == middleSquareIndex2)
                {
                    if (row == middleSquareIndex2)
                        shiftOnY = (moveDistance.y / 2) * -1;
                    if (row == middleSquareIndex1)
                        shiftOnY = (moveDistance.y / 2);
                }

                if (row < middleSquareIndex1 && row < middleSquareIndex2) // move it on minus
                {
                    shiftOnY = moveDistance.y * 1;
                    shiftOnY *= multiplier;
                }
                else if (row > middleSquareIndex1 && row > middleSquareIndex2) // move it on plus
                {
                    shiftOnY = moveDistance.y * -1;
                    shiftOnY *= multiplier;
                }
            }
        }

        return shiftOnY;
    }

    
    private float GetXPositionForShapeSquare(ShapeData shapeData, int column, Vector2 moveDistance)
    {
        float shiftOnX = 0f;

        if (shapeData.columns > 1)
        {
            if (shapeData.columns % 2 != 0)
            {
                var middleSquareIndex = (shapeData.columns - 1) / 2;
                var multiplier = (shapeData.columns - 1) / 2;

                if (column < middleSquareIndex) //Move to negative pos
                {
                    shiftOnX = moveDistance.x * -1;
                    shiftOnX *= multiplier;
                }
                else if (column > middleSquareIndex) // move to positive pos
                {
                    shiftOnX = moveDistance.x * 1;
                    shiftOnX *= multiplier;
                }
            }
            else
            {
                var middleSquareIndex2 = (shapeData.columns == 2) ? 1 : (shapeData.columns / 2);
                var middleSquareIndex1 = (shapeData.columns == 2) ? 0 : (shapeData.columns - 1);
                var multiplier = shapeData.columns / 2;

                if (column == middleSquareIndex1 || column == middleSquareIndex2)
                {
                    if (column == middleSquareIndex2)
                        shiftOnX = moveDistance.x / 2;
                    if (column == middleSquareIndex1)
                        shiftOnX = (moveDistance.x / 2) * -1;
                }

                if (column < middleSquareIndex1 && column < middleSquareIndex2) //move to negative
                {
                    shiftOnX = moveDistance.x * -1;
                    shiftOnX += multiplier;
                }
                else if (column > middleSquareIndex1 && column > middleSquareIndex2)
                {
                    shiftOnX = moveDistance.x * 1;
                    shiftOnX *= multiplier;
                }

            }

        }

        return shiftOnX;
    }
    */

    public void CreateShape(ShapeData shapeData)
    {
        CurrentShape = shapeData;
        int totalActiveSquares = GetNumberOfSquares(shapeData);

        // Pastikan ada cukup GameObject dalam _currentShape
        while (_currentShape.Count < totalActiveSquares)
        {
            _currentShape.Add(Instantiate(squareShapeImage, transform));
        }

        // Matikan semua GameObject sementara
        foreach (var square in _currentShape)
        {
            square.SetActive(false);
        }

        // Hitung jarak antar-kotak berdasarkan ukuran prefab
        RectTransform squareRect = squareShapeImage.GetComponent<RectTransform>();
        Vector2 moveDistance = new Vector2(
            squareRect.rect.width * squareRect.localScale.x,
            squareRect.rect.height * squareRect.localScale.y
        );

        int activeSquareIndex = 0;

        // Loop melalui setiap grid di ShapeData
        for (int row = 0; row < shapeData.rows; row++)
        {
            for (int column = 0; column < shapeData.columns; column++)
            {
                if (shapeData.board[row].column[column])
                {
                    GameObject square = _currentShape[activeSquareIndex];
                    square.SetActive(true);

                    // Hitung posisi X dan Y
                    float xPos = GetXPositionForShapeSquare(shapeData, column, moveDistance);
                    float yPos = GetYPositionForShapeSquare(shapeData, row, moveDistance);

                    // Apply posisi
                    square.GetComponent<RectTransform>().localPosition = new Vector2(xPos, yPos);

                    activeSquareIndex++;
                }
            }
        }
    }

    private float GetXPositionForShapeSquare(ShapeData shapeData, int column, Vector2 moveDistance)
    {
        // Hitung posisi X simetris terhadap tengah
        float middleColumn = (shapeData.columns - 1) / 2f;
        float xOffset = (column - middleColumn) * moveDistance.x;
        return xOffset;
    }

    private float GetYPositionForShapeSquare(ShapeData shapeData, int row, Vector2 moveDistance)
    {
        // Jika hanya 1 baris, Y = 0 (sejajar)
        if (shapeData.rows <= 1) return 0f;

        // Hitung posisi Y simetris terhadap tengah
        float middleRow = (shapeData.rows - 1) / 2f;
        float yOffset = (middleRow - row) * moveDistance.y;
        return yOffset;
    }

    private int GetNumberOfSquares(ShapeData shapeData)
    {
        int number = 0;

        foreach (var rowData in shapeData.board)
        {
            foreach (var active in rowData.column)
            {
                if (active)
                {
                    number++;
                }
            }
        }

        return number;
    }

    public void ResetShape(Vector3 startPosition)
    {
        // Reset posisi
        transform.localPosition = startPosition;

        // Reset rotasi (jika ada)
        transform.rotation = Quaternion.identity;

        // Reset scale
        transform.localScale = Vector3.one;

        // Reset state
        ActivateShape();

        // Reset grid yang pernah dihover
        ResetHoveredGrids();

        // Reset state
        _shapeActive = true;
        _shapeDraggable = true;
    }

    private void ResetHoveredGrids()
    {
        foreach (var square in _currentShape)
        {
            if (!square.activeSelf) continue;

            Collider2D[] colliders = Physics2D.OverlapPointAll(square.transform.position);
            foreach (var collider in colliders)
            {
                GridSquare grid = collider.GetComponent<GridSquare>();
                if (grid != null) grid.ResetGridSquare();
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        
    }

    public void OnPointerUp(PointerEventData eventData)
    {

    }

    public void OnDrag(PointerEventData eventData)
    {
        _transform.anchorMin = new Vector2(0, 0);
        _transform.anchorMax = new Vector2(0, 0);
        _transform.pivot = new Vector2(0, 0);

        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_canvas.transform as RectTransform,
            eventData.position, Camera.main, out pos);
        transform.localPosition = pos + offset;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        try
        {
            if (!IsShapeValidPosition())
            {
                transform.localPosition = _startPosition;
                Debug.Log("Shape dikembalikan ke posisi awal");
            }
            else
            {
                // Cara 1: Langsung panggil fungsi tanpa events
                // FindObjectOfType<Grid>().CheckIfShapeCanBePlaced();

                // Cara 2: Dengan event system (pastikan sudah terdaftar)
                if (GameEvents.CheckIfShapeCanBePlaced != null)
                {
                    GameEvents.CheckIfShapeCanBePlaced.Invoke();
                }
                else
                {
                    Debug.LogError("Event handler belum terdaftar!");
                    transform.localPosition = _startPosition; // Fallback
                }
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Error saat end drag: {e.Message}");
            transform.localPosition = _startPosition;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!_shapeActive || !_shapeDraggable) return;
    }
}
