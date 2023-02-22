using UnityEngine;
using YG;

public class BeforeBuild : MonoBehaviour
{
    [SerializeField] private AchievementData[] _achievments;
    [SerializeField] private PotionForStock[] _potions;

    public void FindAllAchievments()
    {
        _achievments = new AchievementData[]{};
        _achievments = Resources.FindObjectsOfTypeAll<AchievementData>();

        _potions = new PotionForStock[] { };
        _potions = Resources.FindObjectsOfTypeAll<PotionForStock>();
    }

    public void ClearAllAchievements()
    {
        for (int i = 0; i < _achievments.Length; i++)
        {
            _achievments[i].Complete = false;
            _achievments[i].GoalProgress = 0;
        }

        for (int i = 0; i < _potions.Length; i++)
        {
            _potions[i].isCooked = false;
        }
    }

    public void ResetProgress()
    {
        YandexGame.ResetSaveProgress();
        YandexGame.SaveProgress();
    }
}
