using UnityEngine;

public class PerlinNoiseTerrain : MonoBehaviour
{
    public int width = 256;
    public int depth = 256;
    public int height = 20;

    public NoiseManager noiseManager;

    private void Start()
    {
        Terrain terrain = GetComponent<Terrain>();
        terrain.terrainData = GenerateTerrain(terrain.terrainData);
    }

    void Update()
    {
        Terrain terrain = GetComponent<Terrain>();
        terrain.terrainData = GenerateTerrain(terrain.terrainData);
    }

    TerrainData GenerateTerrain(TerrainData terrainData)
    {
        terrainData.heightmapResolution = width + 1;
        terrainData.size = new Vector3(width, height, depth);

        float[,] heights = noiseManager.GetNoise();

        terrainData.SetHeights(0, 0, heights);
        return terrainData;
    }
}
