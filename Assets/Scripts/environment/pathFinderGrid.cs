using UnityEngine;

public class pathFinderGrid
{
    private int width, height;
    private int[,] gridArray;
    private float cellSize;
    private TextMesh[,] debugTextArray;
    private PathNode pathNode;
    public pathFinderGrid(int width, int height, float cellSize, Transform parent)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        gridArray = new int[width, height];
        debugTextArray = new TextMesh[width, height];
        for(int i = 0; i < gridArray.GetLength(0); i++)
        {
            for(int j = 0; j < gridArray.GetLength(1); j++)
            {
                debugTextArray[i, j] = CreateWordText(parent, gridArray[i, j].ToString(), GetPosition(i, j), 10, Color.red, TextAnchor.MiddleCenter);
            }
        }
    }

    private Vector3 GetPosition(int x, int y)
    {
        Vector3 Position = new Vector3(x, y) * cellSize;
        Position = new Vector3(Position.x - 9, Position.y - 9, -2);
        return Position;
    }

    private void GetXY(Vector3 Position, out int x, out int y)
    {
        x = Mathf.FloorToInt(Position.x / cellSize);
        y = Mathf.FloorToInt(Position.y / cellSize);
    }
    public void SetValue(int x, int y, int value)
    {
        if(x >=0 && y >= 0 && x < width && y < height)
        {
            gridArray[x, y] = value;
            debugTextArray[x, y].text = gridArray[x, y].ToString();
        }
    }

    public void SetValue(Vector3 position, int value)
    {
        int x, y;
        GetXY(position, out x, out y);
        SetValue(x + 5, y + 5, value);
    }

    public PathNode GetGridObject(int x, int y)
    {
        pathNode = new PathNode(this, 0, 0);
        return pathNode;
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
