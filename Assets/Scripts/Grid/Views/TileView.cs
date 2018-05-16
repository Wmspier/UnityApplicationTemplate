//  This particular View is not constrained to be just a UI object
public class TileView : View
{
    public int RowNumber { get; private set; }
    public int ColumnNumber { get; private set; }

    public void InitializeTile(int r, int c)
    {
        RowNumber = r;
        ColumnNumber = c;
    }
}
