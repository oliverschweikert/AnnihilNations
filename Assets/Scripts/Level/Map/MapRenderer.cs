using UnityEngine;

public class MapRenderer : MonoBehaviour
{
    public SpriteRenderer textureRenderer;
    public void DrawSprite(Sprite map)
    {
        textureRenderer.sprite = map;
        textureRenderer.transform.localScale = new Vector3(map.rect.width, map.rect.height, 0);
    }
}
