/* 
    ------------------- Code Monkey -------------------

    Thank you for downloading this package
    I hope you find it useful in your projects
    If you have any questions let me know
    Cheers!

               unitycodemonkey.com
    --------------------------------------------------
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode {

    private pathFinderGrid<PathNode> grid;
    public int x;
    public int y;

    public int gCost;
    public int hCost;
    public int fCost;

    public bool isWalkable;
    public bool isTrap;
    public bool isWall;
    public bool isTrapWalkable;
    public PathNode cameFromNode;

    public PathNode(pathFinderGrid<PathNode> grid, int x, int y) {
        this.grid = grid;
        this.x = x;
        this.y = y;
        isWalkable = false;
        isTrap = false;
        isWall = false;
    }

    public void CalculateFCost() {
        fCost = gCost + hCost;
    }

    public void SetIsWalkable(bool isWalkable) {
        this.isWalkable = isWalkable;
        grid.TriggerGridObjectChanged(x, y);
    }

    public void SetIsTrap(bool isTrap)
    {
        this.isTrap = isTrap;
        grid.TriggerGridObjectChanged(x, y);
    }

    public override string ToString() {
        if (isWalkable)
            return 1.ToString();
        else
            return 0.ToString();
    }

}
