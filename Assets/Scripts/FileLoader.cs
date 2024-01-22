using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class FileLoader
{
    public Dictionary<string, string[]> LoadAllGamesFromPath(string myFolder)
    {
        string myPath = Application.streamingAssetsPath;
        string[] images = Directory.GetFileSystemEntries(myPath + "/" + myFolder, "*.jpg", SearchOption.AllDirectories);
        string[] executables = Directory.GetFileSystemEntries(myPath + "/" + myFolder, "*.exe", SearchOption.AllDirectories);
        string[] texts = Directory.GetFileSystemEntries(myPath + "/" + myFolder, "*.txt", SearchOption.AllDirectories);

        Dictionary<string, string[]> gameList = new();

        foreach (string filePath in executables)
        {
            Debug.Log(filePath);

            string name = Path.GetFileName(filePath);
            name = name.Substring(0, name.Length - 4);

            if (gameList.ContainsKey(name)) { continue; }

            string path = filePath.Substring(0, filePath.Length - 4);
            string hasImage = null;
            string hasText = null;
            if (images.Contains(path + ".jpg")) hasImage = "hasImage";
            if (texts.Contains(path + ".txt")) hasText = "hasText";

            string[] info = { path, hasImage, hasText };
            gameList.Add(name, info);

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

    private Texture2D LoadTexture(string FilePath) // Load JPG or PNG file from file path
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

    public string LoadNewTextFile(string textFilePath)
    {
        string text = File.ReadAllText(textFilePath);
        return text;
    }
}