using UnityEngine;

public class SoundsData : MonoBehaviour
{
    [Space]
    [Header("Ёффекты")]
    [SerializeField] private AudioClip _pickBottleClip;
    [SerializeField] private AudioClip _createPotionClip;
    [SerializeField] private AudioClip _waterDrop;
    [SerializeField] private AudioClip[] _doorOpenClips;
    [SerializeField] private AudioClip _closed;
    [SerializeField] private AudioClip _coinDrop;
    [SerializeField] private AudioClip[] _cancelTasks;
    [SerializeField] private AudioClip _winWindowSound;
    [SerializeField] private AudioClip _loseWindowSound;

    [Space]
    [Header("ћузыка")]
    [SerializeField] private AudioClip _claudronRoomTheme;

    public AudioClip PickBottleClip => _pickBottleClip;
    public AudioClip CreatePotionClip => _createPotionClip;
    public AudioClip WaterDrop => _waterDrop;
    public AudioClip[] DoorOpenClips => _doorOpenClips;
    public AudioClip[] CancelClips => _cancelTasks;
    public AudioClip Closed => _closed;
    public AudioClip CoinDrop => _coinDrop;
    public AudioClip WinWindowSound => _winWindowSound;
    public AudioClip LoseWindowSound => _loseWindowSound;
    public AudioClip ClaudronRoomTheme => _claudronRoomTheme;

}
