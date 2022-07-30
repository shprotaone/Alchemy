using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TutorialSystem : MonoBehaviour
{
    public static Action<bool> OnEndedTutorial;
    public static Action<bool> OnUIInterract;   //UI прожимаетс€
    public static Action<bool> OnShopSlotDisabled;

    private const string interactiveLayerName = "Interractive";
    private const string dialogLayerName = "Dialog";

    [SerializeField] private EventCounter _eventCounter;
    [SerializeField] private DialogManager _dialogManager;
    [SerializeField] private VisitorController _visitorController;
    [SerializeField] private InGameTimeController _pause;
    [SerializeField] private Canvas _tutorialCanvas;   

    [Header("–амки дл€ обозначени€ где нажать")]
    [SerializeField] private GameObject _otherWindowFrame;
    [SerializeField] private GameObject _UpOtherWindowFrame;
    [SerializeField] private GameObject _shopClickFrame;
    [SerializeField] private GameObject _inShopClickFrame;
    [SerializeField] private GameObject _guildClickFrame;

    [SerializeField] private List<GameObject> _brightObjectInRoom;
      
    private Visitor _firstVisitor;


    /// <summary>
    /// ¬се действи€ в прив€зке к номеру эвента
    /// </summary>
    /// <param name="eventNumber">предоставл€ет текущий count</param>
    public void CheckEvent(int eventNumber)
    {
        if (eventNumber == _eventCounter.EventCount[0])        //инициализаци€ первого гост€
        {
            _dialogManager.NextDialog();
            SetFirstVisitor();

            _firstVisitor.GetComponent<SpriteRenderer>().sortingLayerName = dialogLayerName;
            _firstVisitor.GetComponentInChildren<Canvas>().sortingLayerName = dialogLayerName;

        }
        else if (eventNumber == _eventCounter.EventCount[1])   //шаг с переходом на нижнее окно
        {
            _dialogManager.NextDialog();

            _firstVisitor.GetComponent<SpriteRenderer>().sortingLayerName = interactiveLayerName;
            _firstVisitor.GetComponentInChildren<Canvas>().sortingLayerName = interactiveLayerName;

            _dialogManager.PlateMovement(1);
            _otherWindowFrame.SetActive(true);

            _dialogManager.PanelIsActive(false);
            _dialogManager.ButtonIsActive(false);
        }
        else if (eventNumber == _eventCounter.EventCount[2])   //шаг с подсветкой рабочего стола
        {

            _dialogManager.NextDialog();
            _otherWindowFrame.SetActive(false);

            _dialogManager.PanelIsActive(true);
            _dialogManager.ButtonIsActive(true);

            BrightObjects(true);
            OnShopSlotDisabled?.Invoke(true);
        }
        else if (eventNumber == _eventCounter.EventCount[3]) // шаг перед покупкой ингредиентов
        {
            _pause.PauseGame();
            _dialogManager.NextDialog();
            _dialogManager.DialogIsActive(false);
            _dialogManager.ButtonIsActive(false);

            BrightObjects(false);

            _shopClickFrame.SetActive(true);

            OnUIInterract?.Invoke(true);            
        }
        else if (eventNumber == _eventCounter.EventCount[4])
        {
            
            _tutorialCanvas.sortingLayerName = dialogLayerName;
            _inShopClickFrame.SetActive(true);

        }
        else if (eventNumber == _eventCounter.EventCount[5]) // шаг после покупки ингредиентов
        {
            _dialogManager.DialogIsActive(true);
            _dialogManager.PanelIsActive(false);
            _dialogManager.NextDialog();

            DragController.instance.ObjectsInterractable(true);
            _tutorialCanvas.sortingLayerName = interactiveLayerName;
        }
        else if (eventNumber == _eventCounter.EventCount[6]) // шаг перед варкой зель€
        {
            _pause.ResumeGame();
            _dialogManager.NextDialog();
        }
        else if (eventNumber == _eventCounter.EventCount[7]) // шаг после варки зель€
        {
            _dialogManager.NextDialog();
        }
        else if (eventNumber == _eventCounter.EventCount[8]) // перед отдачей зель€
        {
            _dialogManager.NextDialog();
            _dialogManager.ButtonIsActive(false);
            _dialogManager.PlateMovement(2);

            _UpOtherWindowFrame.SetActive(true);
        }
        else if (eventNumber == _eventCounter.EventCount[9])
        {
            _UpOtherWindowFrame.SetActive(false);
            _dialogManager.NextDialog();
            _dialogManager.PlateMovement(1);
        }
        else if (eventNumber == _eventCounter.EventCount[10])
        {
            _dialogManager.ButtonIsActive(false);
            _guildClickFrame.SetActive(true);
            _dialogManager.NextDialog();
        }
        else if (eventNumber == _eventCounter.EventCount[11])
        {            
            _guildClickFrame.SetActive(false);
            _dialogManager.DialogIsActive(false);
        }
        else if (eventNumber == _eventCounter.EventCount[12])
        {
            _dialogManager.ButtonIsActive(true);
            _dialogManager.DialogIsActive(true);
            _dialogManager.NextDialog();
        }
        else if( eventNumber == _eventCounter.EventCount[13]) //выдача ресурсов
        {
            _dialogManager.NextDialog();           
        }
        else if(eventNumber == _eventCounter.EventCount[14])  //выдача задани€
        {
            _dialogManager.DialogIsActive(false);
            _dialogManager.PanelIsActive(false);
            _dialogManager.ButtonIsActive(false);
           
            OnEndedTutorial?.Invoke(true);
            OnShopSlotDisabled?.Invoke(false);
        }
    }

    public IEnumerator StartTutorialDelay(bool value)
    {
        if (value)
        {
            EventCounter.OnIncreasedEventCount += CheckEvent;
            OnEndedTutorial?.Invoke(false);

            yield return new WaitForSeconds(1);
            OnUIInterract?.Invoke(false);
            _dialogManager.StartDialogSystem();
            yield return null;
        }       
    }

    private void SetFirstVisitor()
    {
        _firstVisitor = _visitorController.CurrentVisitor;      
    }

    public void BrightObjects(bool flag)
    {
        List<SpriteRenderer> brightObject = new List<SpriteRenderer>();

        string nameLayer;
        if (flag)
        {
            nameLayer = dialogLayerName;
        }
        else
        {
            nameLayer = interactiveLayerName;
        }

        foreach (var item in _brightObjectInRoom)
        {
            brightObject.Add(item.GetComponentInChildren<SpriteRenderer>());
        }

        foreach (var item in brightObject)
        {
            item.sortingLayerName = nameLayer;
        }
    }

    private void OnDisable()
    {
        EventCounter.OnIncreasedEventCount -= CheckEvent;
    }
}
