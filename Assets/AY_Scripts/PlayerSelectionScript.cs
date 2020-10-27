using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerSelectionScript : MonoBehaviour
{
    private Button button;
    private Text text;
    private void Awake()
    {
        text = GetComponentInChildren<Text>();
        button = GetComponent<Button>();
        button.onClick.AddListener(LoadLobby);
    }
    private void LoadLobby()
    {
        SavePlayerChoice();
        SceneManager.LoadScene(1);
    }
    private void SavePlayerChoice()
    {
        foreach (TankType type in Enum.GetValues(typeof(TankType)))
        {
            if(type.ToString().Equals(RemoveSpaces(text.text)))
            {
                PlayerPrefs.SetInt("Player's Choice", (int)type);
                return;
            }
        }
    }
    private string RemoveSpaces(string input)
    {
        input = input.Replace(" ", "");
        return input;
    }
}