public class VisitorCountSystem
{
    private VisitorCountSystemView _view;
    public int VisitorLeft { get; private set; }
    public int CurrentVisitorCount { get; private set; }

    public VisitorCountSystem(VisitorCountSystemView view, int count)
    {
        _view = view;
        VisitorLeft = count;

        _view.RefreshText(VisitorLeft);
    }

    public void DecreaseVisitorCount()
    {
        VisitorLeft--;
        _view.RefreshText(VisitorLeft);
    }
}
