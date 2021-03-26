using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class PlayerSelectionScript : MonoBehaviour
{
    private Button button;
    [SerializeField] private Text text;
    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(LoadLobby);
    }
    private void LoadLobby()
    {
        SavePlayerChoice();
        SceneManager.LoadScene((int)SceneIndex.Game);
    }
    private void SavePlayerChoice()
    {
        foreach (TankType type in Enum.GetValues(typeof(TankType)))
        {
            if(type.ToString().Equals(RemoveSpaces(text.text)))
            {
                PlayerPrefs.SetInt("TankSelected", (int)type);
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