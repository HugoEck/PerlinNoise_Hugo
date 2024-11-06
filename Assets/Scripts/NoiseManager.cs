using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoiseManager : MonoBehaviour
{
    public RawImage noiseTextureImage;
    public int width = 256;
    public int height = 256;  
    public float scale = 20f;  

    private float offsetX;
    private float offsetY;

    private float[,] noise;

    private void Start()
    {
        //Random offsets
        offsetX = Random.Range(0f, 10000f);
        offsetY = Random.Range(0f, 10000f);

        noise = GenerateNoise();
        SetNoiseTexture(noise);
    }

    private void Update()
    {
        noise = GenerateNoise();
        SetNoiseTexture(noise);  

        offsetX += Time.deltaTime * 5f;
    }

    public float[,] GetNoise()
    {
        if (noise == null)
        {
            noise = GenerateNoise();
        }
        return noise;
    }

    private float[,] GenerateNoise()
    {
        float[,] noise = new float[width, height];

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                float xCoord = (float)x / width * scale + offsetX;
                float yCoord = (float)y / height * scale + offsetY;

                // Generate Perlin noise value between 0 and 1
                noise[x, y] = Mathf.PerlinNoise(xCoord, yCoord);
            }
        }

        return noise;
    }

    private void SetNoiseTexture(float[,] noise)
    {
        //Adding colors to noise
        Color[] pixels = new Color[width * height];

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                pixels[y * width + x] = Color.Lerp(Color.black, Color.white, noise[x, y]);
            }
        }

        Texture2D texture = new Texture2D(width, height);
        texture.SetPixels(pixels);
        texture.Apply();

        noiseTextureImage.texture = texture;
    }
}
