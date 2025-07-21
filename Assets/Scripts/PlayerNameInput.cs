using System;
using TMPro;
using UnityEngine;

public class PlayerNameInput : MonoBehaviour
{
    [SerializeField] private TMP_InputField nameInputField;
    [SerializeField] private TMP_InputField passwordInputField;
    [SerializeField] private GameObject playerNameInputPanel;
    [SerializeField] private GameControl gameControl;

    private string playerName;

    private void Start()
    {
        playerName = JsonSave.Instance.GetName();
        
        // if (string.IsNullOrEmpty(playerName))
        // {
        //     playerNameInputPanel.SetActive(true);
        // }
        // else
        // {
        //     playerNameInputPanel.SetActive(false);
        //     
        //     gameControl.SetName(playerName);
        // }
    }

    public void SetName()
    {
        if (string.IsNullOrEmpty(nameInputField.text) || string.IsNullOrEmpty(passwordInputField.text))
        {
            Debug.Log("nameInputField.text or passwordInputField.text is empty");
        }
        else if (nameInputField.text == playerName && JsonSave.Instance.CheckPassword(passwordInputField.text))
        {
            Debug.Log("login");
            
            playerNameInputPanel.SetActive(false);
        }
        else
        {
            JsonSave.Instance.SetName(nameInputField.text);
            JsonSave.Instance.SetPassword(passwordInputField.text);
            gameControl.SetName(nameInputField.text);
            playerNameInputPanel.SetActive(false);
            
            Debug.Log("registracija");
        }
    }
}
