using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class FileLoader
{
    public Dictionary<string, string[]> LoadAllGamesFromPath(string myFilesPath)
    {

        string[] images = Directory.GetFileSystemEntries(myFilesPath, "*.jpg", SearchOption.AllDirectories);
        string[] files = Directory.GetFileSystemEntries(myFilesPath, "*.exe", SearchOption.AllDirectories);

        Dictionary<string, string[]> gameList = new();

        foreach (string filePath in files)
        {
            Debug.Log(filePath);

            string name = Path.GetFileName(filePath);
            name = name.Substring(0, name.Length - 4);

            if (gameList.ContainsKey(name)) { continue; }

            string path = filePath.Substring(0, filePath.Length - 4);

            if (images.Contains(path + ".jpg"))
            {
                string[] info = { path, "hasImage" };

                gameList.Add(name, info);
            }
            else
            {
                string[] info = { path, null };
                gameList.Add(name, info);
            }

        }
        
        return gameList;
    }

    public Sprite LoadNewSprite(string FilePath)
    {
        float PixelsPerUnit = 100.0f;
        // Load a PNG or JPG image from disk to a Texture2D

        Texture2D SpriteTexture = LoadTexture(FilePath);
        Sprite NewSprite = Sprite.Create(SpriteTexture, new Rect(0, 0, SpriteTexture.width, SpriteTexture.height), new Vector2(0, 0), PixelsPerUnit);

        return NewSprite;
    }

    public Texture2D LoadTexture(string FilePath) // Load JPG or PNG file from file path
    {
        Texture2D Tex2D;
        byte[] FileData;

        if (File.Exists(FilePath))
        {
            FileData = File.ReadAllBytes(FilePath);
            Tex2D = new Texture2D(2, 2);
            if (Tex2D.LoadImage(FileData))
                return Tex2D;
        }
        return null;
    }

}
