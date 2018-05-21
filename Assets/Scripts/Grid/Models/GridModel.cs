using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridModel : Model
{
    public int Rows { get; set; }
    public int Columns { get; set; }

    public Tile[,] Grid { get; private set; }

    public Tile SelectedTile { get; set; }
    public Hero Hero { get; set; }

    public Unit SelectedUnit { get; set; }

    public List<Unit> Units { get; set; }

    public GridModel()
    {
        Units = new List<Unit>();
    }

    public List<Unit> GetUnitsInState(Unit.UnitState state){
        return Units.Where(u => u.State == state).ToList();
    }

    public bool AreUnitsInteracting()
    {
        if (Units.Count == 0)
            return false;
        return (GetUnitsInState(Unit.UnitState.Moving).Count != 0 ||
                GetUnitsInState(Unit.UnitState.Targeting).Count != 0 || 
                SelectedUnit);
    }
}
