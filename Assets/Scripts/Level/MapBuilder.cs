using UnityEngine;

public class MapBuilder : MonoBehaviour
{
    public int mapWidth;
    public int mapHeight;
    public float noiseScale;
    public bool autoUpdate;
    public void GenerateMap()
    {
        float[,] noiseMap = MapNoise.GenerateNoise(mapWidth, mapHeight, noiseScale);
        MapRenderer renderer = FindObjectOfType<MapRenderer>();
        renderer.DrawNoiseMap(noiseMap);
    }
}
