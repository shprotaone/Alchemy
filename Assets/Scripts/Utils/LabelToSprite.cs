using UnityEngine;

public class LabelToSprite : MonoBehaviour
{
    [SerializeField] private Sprite _fireSprite;
    [SerializeField] private Sprite _rockSprite;
    [SerializeField] private Sprite _waterSprite;

    public Sprite GetSprite(PotionLabelType label)
    {
        if(label == PotionLabelType.ROCK)
        {
            return _rockSprite;
        }
        else if(label == PotionLabelType.FIRE)
        {
            return _fireSprite;
        }
        else if(label == PotionLabelType.WATER)
        {
            return _waterSprite;
        }
        else
        {
            Debug.LogError("изображение не найдено");
            return null;
        }
    }
}
