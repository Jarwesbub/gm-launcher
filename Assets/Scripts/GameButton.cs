using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameButton : MonoBehaviour
{
    public TMP_Text myText;
    GameObject Controller;
    string gameName;

    public void SetGameButton(string gameName)
    {
        this.gameName= gameName;
        myText.text = gameName;
    }

    private void Start()
    {
        Controller = GameObject.FindWithTag("GameController");

        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        Controller.GetComponent<UIControl>().ButtonSetNextGame(gameName);
    }


}
