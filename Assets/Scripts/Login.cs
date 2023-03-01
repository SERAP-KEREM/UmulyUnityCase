using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Globalization;

public class Login : MonoBehaviour
{
    [Header("Login")] public InputField userNameInputField;
    public InputField emailInputField;
    private string userName;
    private string email;

    private bool EmailValid = false;
    public Text errorText;
    public GameObject ErrorPanel;

    private string[] Characters =
    {
        "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "w",
        "x", "v", "y", "z",
        "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "W",
        "X", "V", "Y", "Z",
        "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "-", "_"
    };
    
    public void LoginButtonClick()
    {
        bool UN = false;
        bool EM = false;
        
        if (userNameInputField.text=="")
        {
            ErrorPanel.SetActive(true);
            errorText.text = "Username Field Empty!";
            Debug.Log("Username Field Empty!");
            
        }
        else
        {
            UN = true;

            if (emailInputField.text != "")
            {
                EmailValidation();
                if (EmailValid)
                {
                    if (emailInputField.text.Contains("@"))
                    {
                        if (emailInputField.text.Contains("."))
                        {
                            EM = true;
                        }
                        else
                        {
                            ErrorPanel.SetActive(true);

                            errorText.text = "Email in Incorrect";
                            Debug.Log("Email in Incorrect");
                        }
                    }
                    else
                    {
                        ErrorPanel.SetActive(true);

                        errorText.text = "Email in Incorrect";
                        Debug.Log("Email in Incorrect");
                    }
                }
                else
                {
                    ErrorPanel.SetActive(true);

                    errorText.text = "Email in Incorrect";
                    Debug.Log("Email in Incorrect");
                }
            }
            else
            {
                ErrorPanel.SetActive(true);

                errorText.text = "Email Field Empty!";
                Debug.Log("Email Field Empty!");
            }

        }

        if (UN ==true && EM ==true)
        {
            userName= CultureInfo.CurrentCulture.TextInfo.ToTitleCase(userNameInputField.text);
            email = emailInputField.text;
            PlayerPrefs.SetString("userName",userName);
            PlayerPrefs.SetString("email",email);
            SceneManager.LoadScene("OfferScene");
        }

    }
    public void EmailValidation()
    {
        bool SW = false;
        bool EW = false;
        for (int i = 0; i < Characters.Length; i++)
        {
            if (emailInputField.text.StartsWith(Characters[i]))
            {
                SW = true;
            }
        }
        for (int i = 0; i < Characters.Length; i++)
        {
            if (emailInputField.text.EndsWith(Characters[i]))
            {
                EW = true;
            }
        }
        if(SW==true&&EW==true)
        {
            EmailValid = true;
        }
        else
        {
            EmailValid = false;
        }
    }

}


