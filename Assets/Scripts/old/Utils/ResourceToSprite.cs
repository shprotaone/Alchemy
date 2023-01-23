using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.U2D;
using UnityEngine;

public class ResourceToSprite
{
    private string _atlasPath = "Assets/Sprites/Atlas/Gameplay1.spriteatlas";

    [SerializeField] private Sprite _redSprite;
    [SerializeField] private Sprite _yellowSprite;
    [SerializeField] private Sprite _blueSprite;
    [SerializeField] private Sprite _whiteSprite;

    private AsyncOperationHandle<SpriteAtlas> _spriteOperation;

    private void Start()
    {
        _spriteOperation = Addressables.LoadAssetAsync<SpriteAtlas>(_atlasPath);
        _spriteOperation.Completed += SpriteLoaded;
    }

    public Sprite Convert(ResourceType type)
    {
        if (type == ResourceType.White) return _whiteSprite;
        else if (type == ResourceType.Red) return _redSprite;
        else if (type == ResourceType.Yellow) return _yellowSprite;
        else return _blueSprite;
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
        _redSprite = atlas.GetSprite("Red");
        _blueSprite = atlas.GetSprite("Blue");
        _yellowSprite = atlas.GetSprite("Yellow");
        _whiteSprite = atlas.GetSprite("White");
    }

    private void OnDestroy()
    {
        if (_spriteOperation.IsValid())
        {
            Addressables.Release(_spriteOperation);
            Debug.Log("Load Complete");

        }
    }
}
