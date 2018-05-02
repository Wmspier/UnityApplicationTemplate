using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileModel : Model
{
    public int RowNumber { get; private set; }
    public int ColumnNumber { get; private set; }

    public bool IsTraversable { get; private set; }

    public TileModel(int r, int c)
    {
        RowNumber = r;
        ColumnNumber = c;
        IsTraversable = true;
    }
}
