using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GridUtility {


    public static bool IsWithinMoveRange(Unit unit, Tile target)
    {
        return GetDistanceBetweenTiles(unit.OccupyingTile, target) <= unit.GetBehavior<Movement>().RemainingMovement;
    }

    public static int GetDistanceBetweenTiles(Tile start, Tile end)
    {
        var unitPos = new Vector2(start.RowNumber, start.ColumnNumber);
        var targetPos = new Vector2(end.RowNumber, end.ColumnNumber);
        int dx = Mathf.Abs((int)targetPos.x - (int)unitPos.x);
        int dy = Mathf.Abs((int)targetPos.y - (int)unitPos.y);

        int min = Mathf.Min(dx, dy);
        int max = Mathf.Max(dx, dy);

        int diagonalSteps = min;
        int straightSteps = max - min;

        return (int)Mathf.Sqrt(2) * diagonalSteps + straightSteps;
    }

    public static string GetUniqueUnitName()
    {
        var grid = ApplicationFacade.instance.GetModel<GridModel>();
        return string.Format("Unit{0}", grid.Units.Count);
    }
}
