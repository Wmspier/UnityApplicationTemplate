public class GridModel : Model
{
    public int Rows { get; set; }
    public int Columns { get; set; }

    public TileModel[,] Grid { get; private set; }

    public void ConstructGridMatrix()
    {
        Grid = new TileModel[Rows, Columns];

        for (int r = 0; r < Rows; ++r)
        {
            for (int c = 0; c < Columns; ++c)
            {
                Grid[r, c] = new TileModel(r, c);
            }
        }
    }
}
