using UnityEngine;

public class MapBuilder : MonoBehaviour
{
    public enum DrawMode { NosieMap, ColorMap };
    public DrawMode drawMode;
    public int mapWidth;
    public int mapHeight;
    public int seed;
    public Vector2 offset;
    public float noiseScale;
    public int octaves;
    [Range(0, 1)]
    public float persistance;
    public float lacunarity;
    public bool autoUpdate;
    public TerrainType[] regions;
    public void GenerateMap()
    {
        float[,] noiseMap = MapNoise.GenerateNoise(mapWidth, mapHeight, seed, offset, noiseScale, octaves, persistance, lacunarity);
        Color[] colorMap = new Color[mapWidth * mapHeight];

        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                float currentNoise = noiseMap[x, y];
                for (int i = 0; i < regions.Length; i++)
                {
                    if (currentNoise <= regions[i].noise)
                    {
                        colorMap[y * mapWidth + x] = regions[i].color;
                        break;
                    }
                }
            }

        }
        MapRenderer renderer = FindObjectOfType<MapRenderer>();
        if (drawMode == DrawMode.NosieMap) renderer.DrawSprite(MapSprite.TextureFromNoiseMap(noiseMap));
        else if (drawMode == DrawMode.ColorMap) renderer.DrawSprite(MapSprite.TextureFromColorMap(colorMap, mapWidth, mapHeight));
    }
    private void OnValidate()
    {
        if (mapWidth < 1) mapWidth = 1;
        if (mapHeight < 1) mapHeight = 1;
        if (lacunarity < 1) lacunarity = 1;
        if (octaves < 0) octaves = 0;
    }
    [System.Serializable]
    public struct TerrainType
    {
        public string name;
        public float noise;
        public Color color;
    }
}
