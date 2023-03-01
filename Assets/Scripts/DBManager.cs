using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Firebase.Extensions;
using System.Globalization;
using Button = UnityEngine.UIElements.Button;

public class DBManager : MonoBehaviour
{
    public DatabaseReference reference;

    [Header("Data Text")] 
    public Text nameText;
    public Text modeText;
    public Text movementText;
    public Text incotermText;
    public Text countriesText;
    public Text packageTypeText;
    public Text unit1Text;
    public Text unit2Text;
    public Text currencyText;
    public InputField datausernameInput;

    [Header("Panels")] 
    public GameObject usernamePanel;
    public GameObject ListPanel;
    public GameObject UserListPanel;
    public GameObject MenuPanel;

    User user = new User();
    

    public Transform UserListTransform;
    public Font mFont;
    public GameObject[] userText;
    private void Start()
    {
        reference = FirebaseDatabase.DefaultInstance.RootReference;
        GetComponent<Text>();

    }
    
    public IEnumerator GetUserData()
    {
        var data = reference.Child("User").GetValueAsync();
        int a = 0;
        int x = -800, y=300;
        while (!data.IsCompleted)
        {
            yield return null;
        }

        if (data.IsCanceled || data.IsFaulted)
        {
            Debug.LogError("Database Error: " + data.Exception);
            yield break;
        }
        DataSnapshot snapshot = data.Result;
            foreach (DataSnapshot user in snapshot.Children)
            {
                a++;
                y -=100;
                CreateText(UserListTransform,x,y,a+". "+user.Key,30,Color.white); 

                if (a%6==0)
                {
                    y = 300;
                    x += 600;
                }

            }
    }
   
   public GameObject CreateText(Transform panel_transform, float x, float y, string text_to_print, int font_size, Color text_color)
    {
        GameObject UItextGO = new GameObject("Text2");
        UItextGO.transform.SetParent(panel_transform);

        RectTransform trans = UItextGO.AddComponent<RectTransform>();
        trans.anchoredPosition = new Vector2(x, y);
        
        RectTransform rt = UItextGO.GetComponent (typeof (RectTransform)) as RectTransform;
        rt.sizeDelta = new Vector2 (200, 100);
        UItextGO.tag = "TextTag";
        
        Text text = UItextGO.AddComponent<Text>();
        text.font = mFont;
        text.text = text_to_print;
        text.fontSize = font_size;
        text.color = text_color;
        text.fontStyle = FontStyle.Normal;
       
        
        return UItextGO;
    }
   public void ShowUsersButtonClick()
    {
        UserListPanel.SetActive(true);
        MenuPanel.SetActive(false);
        StartCoroutine(GetUserData());
        
    }
    public void SearchButtonClick()
    {
        usernamePanel.SetActive(true);
        UserListPanel.SetActive(false);
        MenuPanel.SetActive(false);
        ListPanel.SetActive(false);
        datausernameInput.text = "";
    }

    public void NewUserButtonClick()
    {
        SceneManager.LoadScene("LoginScene");
    }

    public void MenuButtonClick()
    {
        MenuPanel.SetActive(true); 
        DestroyAll("TextTag");
    }
    void DestroyAll(string tag)
    {
        GameObject[] Texts = GameObject.FindGameObjectsWithTag(tag);
        for(int i=0; i< Texts.Length; i++)
        {
            GameObject.Destroy(Texts[i]);
        }
    }

   
   

    public void LCLButtonClick()
    {
        user.mode = GameObject.Find("LCLButton").GetComponentInChildren<Text>().text;
    }

    public void FCLButtonClick()
    {
        user.mode = GameObject.Find("FCLButton").GetComponentInChildren<Text>().text;
    }

    public void AirButtonClick()
    {
        user.mode = GameObject.Find("AirButton").GetComponentInChildren<Text>().text;
    }

    public void DoorToDoorButtonClick()
    {
        user.movementType = GameObject.Find("DoorToDoor").GetComponentInChildren<Text>().text;
    }

    public void PortToDoorButtonClick()
    {
        user.movementType = GameObject.Find("PortToDoorButton").GetComponentInChildren<Text>().text;
    }

    public void DoorToPortButtonClick()
    {
        user.movementType = GameObject.Find("DoorToPortButton").GetComponentInChildren<Text>().text;
    }

    public void PortToPortButtonClick()
    {
        user.movementType = GameObject.Find("PortToPortButton").GetComponentInChildren<Text>().text;
    }

    public void DutyPaidButtonClick()
    {
        user.incoterm = GameObject.Find("DutyPaidButton").GetComponentInChildren<Text>().text;
    }

    public void AtPlaceButtonClick()
    {
        user.incoterm = GameObject.Find("AtPlaceButton").GetComponentInChildren<Text>().text;
    }

    public void USAButtonClick()
    {
        user.countries = GameObject.Find("USAButton").GetComponentInChildren<Text>().text;
    }

    public void ChinaButtonClick()
    {
        user.countries = GameObject.Find("ChinaButton").GetComponentInChildren<Text>().text;
    }

    public void TurkeyButtonClick()
    {
        user.countries = GameObject.Find("TurkeyButton").GetComponentInChildren<Text>().text;
    }

    public void PalletsButtonClick()
    {
        user.packageType = GameObject.Find("PalletsButton").GetComponentInChildren<Text>().text;
    }

    public void BoxesButtonClick()
    {
        user.packageType = GameObject.Find("BoxesButton").GetComponentInChildren<Text>().text;
    }

    public void CartonsButtonClick()
    {
        user.packageType = GameObject.Find("CartonsButton").GetComponentInChildren<Text>().text;
    }

    public void CMButtonClick()
    {
        user.unit1 = GameObject.Find("CMButton").GetComponentInChildren<Text>().text;
    }

    public void INButtonClick()
    {
        user.unit1 = GameObject.Find("INButton").GetComponentInChildren<Text>().text;
    }

    public void KGButtonClick()
    {
        user.unit2 = GameObject.Find("KGButton").GetComponentInChildren<Text>().text;
    }

    public void LBButtonClick()
    {
        user.unit2 = GameObject.Find("LBButton").GetComponentInChildren<Text>().text;
    }

    public void USDButtonClick()
    {
        user.currency = GameObject.Find("USDButton").GetComponentInChildren<Text>().text;
    }

    public void CNYButtonClick()
    {
        user.currency = GameObject.Find("CNYButton").GetComponentInChildren<Text>().text;
    }

    public void TRYButtonClick()
    {
        user.currency = GameObject.Find("TRYButton").GetComponentInChildren<Text>().text;
    }

    public void SaveData()
    {
        user.UserName = PlayerPrefs.GetString("userName");
        user.Email = PlayerPrefs.GetString("email");
        string json = JsonUtility.ToJson(user);

        reference.Child("User").Child(user.UserName).SetRawJsonValueAsync(json).ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log("Successfully added data to firebase.");
            }
            else
            {
                Debug.Log("Not successfull.");
            }
        });
        MenuPanel.SetActive(true);
    }
    
    public void GetData()
    {
        ReadDataName();
        ReadDataMode();
        ReadDataMovement();
        ReadDataIncoterm();
        ReadDataCountries();
        ReadDataPackageType();
        ReadDataUnit1();
        ReadDataUnit2();
        ReadDataCurrency();
        ListPanel.SetActive(true);

    }
    public void ReadDataName()
    {
        datausernameInput.text=CultureInfo.CurrentCulture.TextInfo.ToTitleCase(datausernameInput.text);
        reference.Child("User").Child(datausernameInput.text).GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;

                nameText.text = snapshot.Child("UserName").Value.ToString() + " Offer List";
            }
            else
            {
                Debug.Log("Not successfull");
            }
        });

    }
   
    public void ReadDataMode()
    {
        reference.Child("User").Child(datausernameInput.text).GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                modeText.text = "Mode : " + snapshot.Child("mode").Value.ToString();
            }
            else
            {
                Debug.Log("Not successfull");
            }
        });
    }

    public void ReadDataMovement()
    {
        reference.Child("User").Child(datausernameInput.text).GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                movementText.text = "Movement Type : " + snapshot.Child("movementType").Value.ToString();
            }
            else
            {
                Debug.Log("Not successfull");
            }
        });
    }

    public void ReadDataIncoterm()
    {
        reference.Child("User").Child(datausernameInput.text).GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                incotermText.text = "Incoterm : " + snapshot.Child("incoterm").Value.ToString();
            }
            else
            {
                Debug.Log("Not successfull");
            }
        });
    }

    public void ReadDataCountries()
    {
        reference.Child("User").Child(datausernameInput.text).GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                countriesText.text = "Countries : " + snapshot.Child("countries").Value.ToString();
            }
            else
            {
                Debug.Log("Not successfull");
            }
        });
    }

    public void ReadDataPackageType()
    {
        reference.Child("User").Child(datausernameInput.text).GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                packageTypeText.text = "Package Type : " + snapshot.Child("packageType").Value.ToString();
            }
            else
            {
                Debug.Log("Not successfull");
            }
        });
    }

    public void ReadDataUnit1()
    {
        reference.Child("User").Child(datausernameInput.text).GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                unit1Text.text = "Unit1 : " + snapshot.Child("unit1").Value.ToString();
            }
            else
            {
                Debug.Log("Not successfull");
            }
        });
    }

    public void ReadDataUnit2()
    {
        reference.Child("User").Child(datausernameInput.text).GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                unit2Text.text = "Unit2 : " + snapshot.Child("unit2").Value.ToString();
            }
            else
            {
                Debug.Log("Not successfull");
            }
        });
    }

    public void ReadDataCurrency()
    {
        reference.Child("User").Child(datausernameInput.text).GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                currencyText.text = "Currency : " + snapshot.Child("currency").Value.ToString();
            }
            else
            {
                Debug.Log("Not successfull");
            }
        });
    }
}