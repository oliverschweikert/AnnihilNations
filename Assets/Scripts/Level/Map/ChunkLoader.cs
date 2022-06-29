using UnityEngine;
public class ChunkLoader : MonoBehaviour
{
    public Player player;
    public GameObject chunk;
    Vector2 playerChunkCoords;
    Vector2[] loadedChunkCoords;
    GameObject[] loadedChunks;
    private void Start()
    {
        int playerChunkX = (int)(player.transform.position.x / 100);
        int playerChunkY = (int)(player.transform.position.y / 100);
        playerChunkCoords = new Vector2(playerChunkX, playerChunkY);
        loadedChunks = new GameObject[9];
        UpdateLoadedChunkCoords();
        RenderLoadedChunks();
    }
    void Update()
    {
        int playerChunkX = (int)(player.transform.position.x / 100);
        int playerChunkY = (int)(player.transform.position.y / 100);
        Vector2 playerChunkVector = new Vector2(playerChunkX, playerChunkY);
        if (playerChunkCoords != playerChunkVector)
        {
            playerChunkCoords = playerChunkVector;
            UpdateLoadedChunkCoords();
            RenderLoadedChunks();
            Debug.Log($"Chunk is now {playerChunkCoords}");
        }
    }
    private void RenderLoadedChunks()
    {
        for (int i = 0; i < loadedChunkCoords.Length; i++)
            RenderChunk(loadedChunkCoords[i], i);
    }
    private void RenderChunk(Vector2 c, int i)
    {
        var newChunk = Instantiate(chunk, c * 100, Quaternion.identity, gameObject.transform);
        newChunk.gameObject.name = $"Chunk ({c.x} {c.y})";
        MapBuilder builder = newChunk.GetComponent<MapBuilder>();
        builder.offset = c * 10;
        builder.GenerateMap();
        if (loadedChunks[i] != null)
            Destroy(loadedChunks[i]);
        loadedChunks[i] = newChunk;
    }
    private void UpdateLoadedChunkCoords()
    {
        loadedChunkCoords = new Vector2[]{
            new Vector2(playerChunkCoords.x-1,playerChunkCoords.y +1),
            new Vector2(playerChunkCoords.x,playerChunkCoords.y +1),
            new Vector2(playerChunkCoords.x+1,playerChunkCoords.y +1),
            new Vector2(playerChunkCoords.x-1,playerChunkCoords.y),
            new Vector2(playerChunkCoords.x,playerChunkCoords.y),
            new Vector2(playerChunkCoords.x+1,playerChunkCoords.y),
            new Vector2(playerChunkCoords.x-1,playerChunkCoords.y -1),
            new Vector2(playerChunkCoords.x,playerChunkCoords.y -1),
            new Vector2(playerChunkCoords.x+1,playerChunkCoords.y -1),
        };
    }
}
