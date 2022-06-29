using UnityEngine;

public static class MapSprite
{
    public static Sprite TextureFromColorMap(Color[] colorMap, int width, int height)
    {
        Texture2D texture = new Texture2D(width, height);
        texture.filterMode = FilterMode.Point;
        texture.wrapMode = TextureWrapMode.Clamp;
        texture.SetPixels(colorMap);
        texture.Apply();
        Sprite map = Sprite.Create(texture, new Rect(0, 0, width, height), new Vector2(0.5f, 0.5f));
        return map;
    }
    public static Sprite TextureFromNoiseMap(float[,] noiseMap)
    {

        int width = noiseMap.GetLength(0);
        int height = noiseMap.GetLength(1);
        Color[] colorMap = new Color[width * height];
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                colorMap[y * width + x] = Color.Lerp(Color.black, Color.white, noiseMap[x, y]);
            }
        }
        return TextureFromColorMap(colorMap, width, height);
    }
}
