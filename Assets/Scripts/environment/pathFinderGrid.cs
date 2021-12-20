using System;
using UnityEngine;


public class pathFinderGrid<TGridObj>
{
    public event EventHandler<OnGridObjectChangedEventArgs> OnGridObjectChanged;
    public class OnGridObjectChangedEventArgs
    {
        public int x;
        public int y;
    }
    private int width, height;
    private TGridObj[,] gridArray;
    public float cellSize;
    public Vector3 originPos;
    private TextMesh[,] debugTextArray;
    private bool debug = true;
    private PathNode pathNode;
    public pathFinderGrid(int width, int height, float cellSize, Vector3 originPos, Func<pathFinderGrid<TGridObj>, int, int, TGridObj> createGridObj, Transform parent)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.originPos = originPos;
        gridArray = new TGridObj[width, height];
        debugTextArray = new TextMesh[width, height];
        for (int i = 0; i < gridArray.GetLength(0); i++)
        {
            for (int j = 0; j < gridArray.GetLength(1); j++)
            {
                gridArray[i, j] = createGridObj(this, i, j);
            }
        }
        if (debug)
        {
            for (int i = 0; i < gridArray.GetLength(0); i++)
            {
                for (int j = 0; j < gridArray.GetLength(1); j++)
                {
                    debugTextArray[i, j] = CreateWordText(parent, gridArray[i, j]?.ToString(), GetPosition(i, j) + new Vector3(cellSize, cellSize) * .5f, 10, Color.red, TextAnchor.MiddleCenter);
                }
            }
        }
    }

    private Vector3 GetPosition(int x, int y)
    {
        Vector3 Position = new Vector3(x, y) * cellSize + originPos;
        Position = new Vector3(Position.x, Position.y, -2);
        return Position;
    }

    public void GetXY(Vector3 Position, out int x, out int y)
    {
        x = Mathf.FloorToInt((Position - originPos).x / cellSize);
        y = Mathf.FloorToInt((Position - originPos).y / cellSize);
    }
    public void SetGridObject(int x, int y, TGridObj value)
    {
        if(x >=0 && y >= 0 && x < width && y < height)
        {
            gridArray[x, y] = value;
            if (OnGridObjectChanged != null) OnGridObjectChanged(this, new OnGridObjectChangedEventArgs { x = x, y = y });
            if (debug)
            {
                debugTextArray[x, y].text = gridArray[x, y].ToString();
            }
        }
    }
    public void TriggerGridObjectChanged(int x, int y)
    {
        if (OnGridObjectChanged != null) OnGridObjectChanged(this, new OnGridObjectChangedEventArgs { x = x, y = y });
    }

    public void SetGridObject(Vector3 position, TGridObj value)
    {
        int x, y;
        GetXY(position, out x, out y);
        SetGridObject(x, y, value);
    }

    public TGridObj GetGridObject(int x, int y)
    {
        if(x >= 0 && y >= 0 && x < width && y < height)
        {
            return gridArray[x, y];
        }
        else
        {
            return default(TGridObj);
        }
    }
    public TGridObj GetGridObject(Vector3 position)
    {
        int x, y;
        GetXY(position, out x, out y);
        return GetGridObject(x, y);
    }

    public int GetWidth()
    {
        return gridArray.GetLength(0);
    }

    public int GetHeight()
    {
        return gridArray.GetLength(1);
    }
    public TextMesh CreateWordText(Transform parent, string text, Vector3 position, int fontSize, Color color, TextAnchor anchor)
    {
        GameObject gameObject = new GameObject("World_text", typeof(TextMesh));
        gameObject.transform.SetParent(parent.transform);
        Transform transform = gameObject.transform;
        transform.localPosition = position;
        TextMesh textMesh = gameObject.GetComponent<TextMesh>();
        textMesh.anchor = anchor;
        textMesh.text = text;
        textMesh.fontSize = fontSize;
        textMesh.color = color;
        return textMesh;
    }

}
