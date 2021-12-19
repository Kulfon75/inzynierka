using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode
{
    private pathFinderGrid Grid;
    private int x, y;

    public int gCost, hCost, fCost;

    public PathNode cameFrom;

    public PathNode(pathFinderGrid grid, int x, int y)
    {
        this.Grid = grid;
        this.x = x;
        this.y = y;
    }

    public override string ToString()
    {
        return x + "," + y;
    }
}
