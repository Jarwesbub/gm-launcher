using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIControl : MonoBehaviour
{
    public GameObject GameButton, PlayButton;
    public Transform GameButtonsParent;
    public Image Showcase;
    public TMP_Text gameInfo;
    public Dictionary<string, string[]> gameDictionary;
    private FileLoader fileLoader;
    public int gameFilesCount = 0;
    public string[] imagesTest;
    public string currentName;
    public string filePath;

    int currentGameID = 0;

    private void Awake()
    {
        fileLoader = new();
        string myFilesPath = Environment.CurrentDirectory + "/Assets/Files/";
        gameDictionary = fileLoader.LoadAllGamesFromPath(myFilesPath);
        Showcase.transform.gameObject.SetActive(false);
        gameInfo.text = "";
        PlayButton.SetActive(false);
    }

    protected void SetCurrentGameByID(int id)
    {
        currentGameID = id;
        bool hasImage = gameDictionary.ElementAt(id).Value[1] == "hasImage";
        if (hasImage)
        {
            string path = gameDictionary.ElementAt(id).Value[0];
            filePath = path;
            Showcase.sprite = fileLoader.LoadNewSprite(path + ".jpg");
        }
        Showcase.transform.gameObject.SetActive(hasImage);

        if (!PlayButton.activeSelf)
        {
            PlayButton.SetActive(true);
        }
    }

    protected void CreateGameButtons()
    {
        foreach (KeyValuePair<string, string[]> entry in gameDictionary)
        {
            string name = entry.Key;
            string path = entry.Value[0];
            bool hasImage = entry.Value[1] == "hasImage";

            GameObject gameButton = Instantiate(GameButton, GameButtonsParent);
            gameButton.GetComponent<GameButton>().SetGameButton(name);
        }
    }


    public void OnClickPlay()
    {
        if (gameDictionary.Count < currentGameID) { return; }

        string currentGame = gameDictionary.ElementAt(currentGameID).Value[0] + ".exe";

        Process.Start(currentGame);
    }

    public void OnClickQuitGame()
    {
        Application.Quit();
    }

    public void ButtonSetNextGame(string name)
    {
        if (gameDictionary.ContainsKey(name))
        {
            int id = gameDictionary.Keys.ToList().IndexOf(name);
            SetCurrentGameByID(id);
            gameInfo.text = name;
        }
    }
}