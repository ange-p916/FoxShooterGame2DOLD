using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Rewired;

public class TypeWriter : MonoBehaviour {

    Player input;
    public Image bgImg;
    public Text nameText;
    public Text theText;
    public TextAsset textToReadFrom;
    public List<string> textLines = new List<string>();
    string lineOfText;

    public bool shouldHaveUtility = true;

    public bool startDialogue = false;
    public bool hasFinished = false;
    public int lineToStartFrom;
    public int lineToEndAt;
    int lineCounter;
    int letterCounter;
    public float timeBetweenLetters = 0.08f;
    public float newTimeBetweenLetters = 0.08f;
    public float slowDownTimeAtCommas = 0.14f;

    void Start()
    {
        input = ReInput.players.GetPlayer(0);

        foreach (var line in textToReadFrom.text.Split('\n'))
        {
            textLines.Add(line);
        }
        theText.text = "";
        lineCounter = lineToStartFrom;
    }

    void Update()
    {
        HoldUpDialogue();
    }

    void HoldUpDialogue()
    {
        if (startDialogue)
        {
            hasFinished = false;
            bgImg.gameObject.SetActive(true);
            theText.enabled = true;
            nameText.enabled = true;

            if(shouldHaveUtility)
            {
                PlayerDisableUtility.Instance.PlayerAbility(false);
            }            

            lineOfText = textLines[lineCounter];
            nameText.text = HaveNameInText(lineOfText);
            //print(HaveNameInText(lineOfText));
            lineOfText = OmitNameInText(lineOfText);
            timeBetweenLetters -= Time.deltaTime;

            if (timeBetweenLetters <= 0)
            {
                if (letterCounter <= lineOfText.Length - 1)
                {
                    theText.text += lineOfText[letterCounter];

                    if (ContainsComma(lineOfText[letterCounter]))
                    {
                        timeBetweenLetters = slowDownTimeAtCommas;
                    }
                    else
                    {
                        timeBetweenLetters = newTimeBetweenLetters;
                    }
                    letterCounter++;
                }
            }
            if (input.GetButtonDown("Jump")
                && timeBetweenLetters > 0
                && letterCounter <= lineOfText.Length - 1)
            {
                letterCounter = lineOfText.Length - 1;
                lineOfText = OmitNameInText(lineOfText);
                theText.text = OmitNameInText(lineOfText);
            }

            if (letterCounter >= lineOfText.Length)
            {
                if (input.GetButtonDown("Jump"))
                {
                    letterCounter = 0;
                    theText.text = "";

                    //nameText.text = HaveNameInText(lineOfText);
                    lineCounter++;
                }
            }

            if (lineCounter >= lineToEndAt)
            {
                nameText.text = "";
                startDialogue = false;
                if (!startDialogue)
                {
                    StartCoroutine(DisableGUI(0.5f));
                    if (shouldHaveUtility)
                    {
                        PlayerDisableUtility.Instance.PlayerAbility(true);
                    }
                    hasFinished = true;
                }
            }
        }
    }

    IEnumerator DisableGUI(float mTime)
    {
        yield return new WaitForSeconds(mTime);
        theText.enabled = false;
        nameText.enabled = false;
        bgImg.gameObject.SetActive(false);
        
    }

    string HaveNameInText(string s)
    {
        return s.Substring(0, s.LastIndexOf('|'));
    }

    string OmitNameInText(string s)
    {
        return s.Substring(s.IndexOf('|') + 1);
    }

    string OmitNameInText(char s)
    {
        return s.ToString().Substring(s.ToString().IndexOf('|') + 1);
    }

    bool ContainsComma(char c)
    {
        if (c.Equals(','))
            return true;

        return false;
    }

}