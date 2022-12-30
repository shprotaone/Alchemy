using System;

public class VisitorCountSystem
{
    public event Action OnVisitorEnded;

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
        if(VisitorLeft == 0)
        {
            OnVisitorEnded?.Invoke();
        }
    }
}
