using UnityEngine;

public class LabelToSprite : MonoBehaviour
{
    [SerializeField] private Sprite _fireSprite;
    [SerializeField] private Sprite _rockSprite;
    [SerializeField] private Sprite _waterSprite;

    [SerializeField] private Sprite _fireSpriteNB;
    [SerializeField] private Sprite _rockSpriteNB;
    [SerializeField] private Sprite _waterSpriteNB;

    public Sprite GetSprite(PotionLabelType label, bool withBG)
    {
        if (withBG)
        {
            if (label == PotionLabelType.ROCK) return _rockSprite;
            else if (label == PotionLabelType.FIRE) return _fireSprite;
            else return _waterSprite;
        }
        else
        {
            if (label == PotionLabelType.ROCK) return _rockSpriteNB;
            else if (label == PotionLabelType.FIRE) return _fireSpriteNB;
            else return _waterSpriteNB;
        }
    }
}
