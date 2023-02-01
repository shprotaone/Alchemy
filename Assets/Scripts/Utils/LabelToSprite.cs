using UnityEngine;

public class LabelToSprite : MonoBehaviour
{
    [SerializeField] private string _atlasPath = "Assets/Sprites/Atlas/Labels.spriteatlas";

    [SerializeField] private Sprite _fireSprite;
    [SerializeField] private Sprite _rockSprite;
    [SerializeField] private Sprite _waterSprite;

    [SerializeField] private Sprite _fireSpriteNB;
    [SerializeField] private Sprite _rockSpriteNB;
    [SerializeField] private Sprite _waterSpriteNB;

    //private AsyncOperationHandle<SpriteAtlas> _spriteOperation;

    //private void Awake()
    //{
    //    _spriteOperation = Addressables.LoadAssetAsync<SpriteAtlas>(_atlasPath);
    //    _spriteOperation.Completed += SpriteLoaded;
    //}

    //private void SpriteLoaded(AsyncOperationHandle<SpriteAtlas> obj)
    //{
    //    switch (obj.Status)
    //    {
    //        case AsyncOperationStatus.Succeeded:
    //            LoadSprites(obj.Result);
    //            break;
    //        case AsyncOperationStatus.Failed:
    //            Debug.LogError("SpriteNotLoaded");
    //            break;
    //        default:
    //            break;
    //    }
    //}

    //private void LoadSprites(SpriteAtlas atlas)
    //{
    //    _fireSprite = atlas.GetSprite("FireB");
    //    _rockSprite = atlas.GetSprite("RockB");
    //    _waterSprite = atlas.GetSprite("WaterB");

    //    _fireSpriteNB = atlas.GetSprite("FireNB");
    //    _rockSpriteNB = atlas.GetSprite("RockNB");
    //    _waterSpriteNB = atlas.GetSprite("WaterNB");
    //}

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
