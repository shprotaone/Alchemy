public class VisitorCountSystem
{
    private VisitorCountSystemView _view;
    public int VisitorLeft { get; private set; }

    public VisitorCountSystem(VisitorCountSystemView view, int count)
    {
        _view = view;
        VisitorLeft = count;
        _view.SetStartCountText(VisitorLeft);
        _view.RefreshText(VisitorLeft);
    }

    public void DecreaseVisitorCount()
    {
        if(VisitorLeft > 0)
        {
            VisitorLeft--;
            _view.RefreshText(VisitorLeft);
        }
    }
}
