using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public sealed class ButtonEventCatcher : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Action OnButtonPressed;
    public Action OnButtonHold;
    public Action OnButtonRelease;
    public Button Button => _button;
    private Button _button;
    private UnityAction _handleButtonPressed;


    private void OnEnable()
    {
        _button = GetComponent<Button>();
        _handleButtonPressed = () => {OnButtonPressed?.Invoke();};
        _button.onClick.AddListener(_handleButtonPressed);
        _button.interactable = false;
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(_handleButtonPressed);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_button.interactable)
        {
            OnButtonHold?.Invoke();
        }       
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        OnButtonRelease?.Invoke();
    }
}
