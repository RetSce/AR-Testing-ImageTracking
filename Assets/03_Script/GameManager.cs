using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Levels and answers")]
    public GameObject[] Level;
    // 0: Pregame   1: Level 1      2: Level 2      3: Level 3

    public string[] AnswerQ1;
    public string[] AnswerQ2;
    public string[] AnswerQ3;
    public string[] Answer;
    public bool AnswerGet;


    [Header("Level Variable")]
    public int CurLevel;


    [Header("Input Field")]
    public TMP_InputField AnswerInput;
    public TMP_Text TextInput;


    [Header("Error Answer")]
    public GameObject CorrectAnswerPrefab;
    public GameObject WrongAnswerPrefab;
    public Transform CanvasParentObject;


    [Header("Hint")]
    public string AnswerTxt;
    public TMP_Text hinttxt;
    public Transform HintParentItem;
    public bool HintTextChecking;
    public string[] hintlvl1txt;
    public string[] hintlvl2txt;
    public string[] hintlvl3txt;
    private string[] hinttxtarray;


    // Mobile Keyboard 
    private TouchScreenKeyboard MobileInput;







    private void Start()
    {
        AnswerGet = false;


        // Getting the vlue of curlevel
        CurLevel = SceneManager.GetActiveScene().buildIndex;


        hinttxtarray = new string[3];
    }


    private void Update()
    {
        HintTextChecking = true;

        LevelChecker();
        UpdateAnswer();
    }

    public void UpdateText(string text)
    {
        AnswerTxt = text;
        Debug.Log(text);
    }


    // Update Answer and Hint
    public void UpdateAnswer()
    {
        if (CurLevel == 1)
        {
            Answer = new string[AnswerQ1.Length];
            hinttxtarray = new string[hintlvl1txt.Length];

            for (int i = 0; i < AnswerQ1.Length; i++)
            {
                Answer[i] = AnswerQ1[i];
            }

            for (int i = 0; i < hintlvl1txt.Length; i++)
            {
                hinttxtarray[i] = hintlvl1txt[i];
            }
        }

        if (CurLevel == 2)
        {
            Answer = new string[AnswerQ2.Length];
            hinttxtarray = new string[hintlvl2txt.Length];

            for (int i = 0; i < AnswerQ2.Length; i++)
            {
                Answer[i] = AnswerQ2[i];
            }
            
            for (int i = 0; i < hintlvl2txt.Length; i++)
            {
                hinttxtarray[i] = hintlvl2txt[i];
            }
        }

        if (CurLevel == 3)
        {
            Answer = new string[AnswerQ3.Length];
            hinttxtarray = new string[hintlvl3txt.Length];

            for (int i = 0; i < AnswerQ3.Length; i++)
            {
                Answer[i] = AnswerQ3[i];
            }

            for (int i = 0; i < hintlvl3txt.Length; i++)
            {
                hinttxtarray[i] = hintlvl3txt[i];
            }
        }

        ChangeHint();
    }


    // Function for StartButton
    public void StartBtn()
    {
        //GameObject.Find("Tutorial").SetActive(true);
        CurLevel = 1;
    }

    // Open the input field
    public void OpenTyperBtn()
    {
        AnswerInput.gameObject.SetActive(true);
        MobileInput = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default, false, false, false);
    }

    // Checking the answer of player input field
    public void CheckingAnswer()
    {
        for (int i = 0; i < Answer.Length; i++)
        {
            if (AnswerTxt == Answer[i])
            {
                AnswerGet = true;
                CorrectAnswer();
                CurLevel++;
                break;
            }
            Debug.Log(TextInput.text == Answer[i]);
        }

        if (AnswerGet == false)
        {
            ErrorAnswer();
        }
    }

    // if correct answer
    private void CorrectAnswer()
    {
        // Spawning correctAnsprefab for congrats
        var SpawnItem = Instantiate(CorrectAnswerPrefab, CanvasParentObject);
        Destroy(SpawnItem, 2.5f);

        // Restart some variable
        hinttxt.text = "";
        AnswerGet = false;
        HintTextChecking = true;

    }

    // if wrong answer
    private void ErrorAnswer()
    {
        var SpawnItem = Instantiate(WrongAnswerPrefab, CanvasParentObject);
        Destroy(SpawnItem, 2.5f);
    }

    // Close the input field
    public void CloseItems()
    {
        AnswerInput.gameObject.SetActive(false);
    }

    // Checking the current level
    private void LevelChecker()
    {
        if (CurLevel == 4)
        {
            CurLevel = 0;
        }

        if (CurLevel == 0)
        {
            Level[3].SetActive(false);
            Level[0].SetActive(true);
        }

        if (CurLevel >= 1)
        {
            Level[CurLevel - 1].SetActive(false);
            Level[CurLevel].SetActive(true);
        }


    }

    // Changing Hint
    public void ChangeHint()
    {
        for (int i = 0; i <= hinttxtarray.Length; i++)
        {
            if (i == hinttxtarray.Length && HintTextChecking == true)
            {
                HintTextChecking = false;
                break;
            }

            else if (HintTextChecking == true)
            {
                hinttxt.text += hinttxtarray[i] + "\n";
            }
        }
    }
}
