public class Ground
{
    bool flagged;
    bool mine;
    int minesAround;

    public Ground(bool mine, int minesAround)
    {
        flagged = false;
        this.mine = mine;
        this.minesAround = minesAround;
    }

    public int Dig()
    {
        if (mine)
        {
            return -1;
        }
        else
        {
            return minesAround;
        }
    }
}