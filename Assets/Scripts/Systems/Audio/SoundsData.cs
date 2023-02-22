using FMODUnity;
using UnityEngine;

public class SoundsData : MonoBehaviour
{
    [Space]
    [Header("Эффекты")]
    [SerializeField] private EventReference _pickBottleClip;
    [SerializeField] private EventReference _createPotionClip;
    [SerializeField] private EventReference _waterDrop;
    [SerializeField] private EventReference _bottleOnShelf;
    [SerializeField] private EventReference _scales;
    [SerializeField] private EventReference _closed;
    [SerializeField] private EventReference _coinDrop;
    [SerializeField] private EventReference[] _cancelTasks;
    [SerializeField] private EventReference _winWindowSound;
    [SerializeField] private EventReference _loseWindowSound;
    [SerializeField] private EventReference _boilSound;

    [Space] 
    [Header("Шаги")] 
    [SerializeField] private EventReference _stepsBandit;
    [SerializeField] private EventReference _stepsKnight;
    [SerializeField] private EventReference _stepsSaint;
    [SerializeField] private EventReference _stepsWizzard;

    [Space] 
    [Header("Голоса")] 
    [SerializeField] private EventReference _voiceGood;
    [SerializeField] private EventReference _voiceSad;
    [SerializeField] private EventReference _voiceWaiting;

    [Space] 
    [Header("Музыка и UI")] 
    [SerializeField] private EventReference _mainMenuMusic;
    [SerializeField] private EventReference _claudronRoomTheme;
    [SerializeField] private EventReference _okSoundButton;
    [SerializeField] private EventReference _soundButton;
    [SerializeField] private EventReference _closeButton;

    public EventReference MainMenuMusic => _mainMenuMusic;
    public EventReference PickBottleClip => _pickBottleClip;
    public EventReference CreatePotionClip => _createPotionClip;
    public EventReference WaterDrop => _waterDrop;
    public EventReference StepsBandit => _stepsBandit;
    public EventReference StepsKnight => _stepsKnight;
    public EventReference StepsSaint => _stepsSaint;
    public EventReference StepsWizzard => _stepsWizzard;
    public EventReference VoiceGood => _voiceGood;
    public EventReference VoiceSad => _voiceSad;
    public EventReference VoiceWaiting => _voiceWaiting;
    public EventReference BottleOnShelf => _bottleOnShelf;
    public EventReference Scales => _scales;
    public EventReference Closed => _closed;
    public EventReference CoinDrop => _coinDrop;
    public EventReference WinWindowSound => _winWindowSound;
    public EventReference LoseWindowSound => _loseWindowSound;
    public EventReference ClaudronRoomTheme => _claudronRoomTheme;
    public EventReference BoilSound => _boilSound;
    public EventReference OkSoundButton => _okSoundButton;
    public EventReference SoundButton => _soundButton;
    public EventReference CloseButton => _closeButton;

}
