using UnityEngine;
using System.Collections;

public class GridModel : Model
{
    public int Rows { get; set; }
    public int Columns { get; set; }

    public Tile[,] Grid { get; private set; }

    public Tile SelectedTile { get; set; }
    public Hero Hero { get; set; }

    public Unit SelectedUnit { get; set; }
}
