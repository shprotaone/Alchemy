using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.U2D;

public class LabelToSprite : MonoBehaviour
{
    [SerializeField] private string _atlasPath = "Assets/Sprites/Atlas/Labels.spriteatlas";

    private Sprite _fireSprite;
    private Sprite _rockSprite;
    private Sprite _waterSprite;

    private Sprite _fireSpriteNB;
    private Sprite _rockSpriteNB;
    private Sprite _waterSpriteNB;

    private AsyncOperationHandle<SpriteAtlas> _spriteOperation;

    private void Awake()
    {
        _spriteOperation = Addressables.LoadAssetAsync<SpriteAtlas>(_atlasPath);
        _spriteOperation.Completed += SpriteLoaded;
    }

    private void SpriteLoaded(AsyncOperationHandle<SpriteAtlas> obj)
    {
        switch (obj.Status)
        {
            case AsyncOperationStatus.Succeeded:
                LoadSprites(obj.Result);
                break;
            case AsyncOperationStatus.Failed:
                Debug.LogError("SpriteNotLoaded");
                break;
            default:
                break;
        }
    }

    private void LoadSprites(SpriteAtlas atlas)
    {
        _fireSprite = atlas.GetSprite("FireB");
        _rockSprite = atlas.GetSprite("RockB");
        _waterSprite = atlas.GetSprite("WaterB");

        _fireSpriteNB = atlas.GetSprite("FireNB");
        _rockSpriteNB = atlas.GetSprite("RockNB");
        _waterSpriteNB = atlas.GetSprite("WaterNB");
    }

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
