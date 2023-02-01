using UnityEngine;
using UnityEngine.EventSystems;

public class MiniHelpPanel : MonoBehaviour,IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Transform _panel;

    private void Behaviour(bool flag)
    {
        _panel.gameObject.SetActive(flag);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Behaviour(true);     
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Behaviour(false);
    }
}
