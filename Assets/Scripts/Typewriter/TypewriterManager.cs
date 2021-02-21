using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using Rewired;

public class TypewriterManager : MonoBehaviour
{
    public bool startItNow = false;

    public bool isTyping;
    public bool cancelTyping;
    public float textSpeed = 0.08f;
    public float timerBeforeDisable = 5.8f;
    public Text text;
    public Image textImage;

    public TextAsset textFile;
    public string[] textLines;

    public int currentLine;
    public int endAtLine;
    PlayablePlayer player;
    PlayerShotController psc;
    Player input;

    void Start()
    {
        player = FindObjectOfType<PlayablePlayer>();
        psc = player.GetComponent<PlayerShotController>();
        input = ReInput.players.GetPlayer(0);
        if(textFile != null)
        {
            textLines = textFile.text.Split('\n');
        }

        if(endAtLine == 0)
        {
            endAtLine = textLines.Length - 1;
        }

    }
    void Update()
    {
        if(startItNow)
        {
            ShowTextStuff();
        }
    }
    public void ShowTextStuff()
    {
        if(textImage != null)
        {
            textImage.enabled = true;
        }
        text.enabled = true;
        StartCoroutine(WaitWithDisabling(3.5f));
        if (!isTyping)
        {
            currentLine++;
            if (currentLine > endAtLine)
            {
                StartCoroutine(DisableGUI(0.5f));
            }
            else
            {
                StartCoroutine(TextScroll(textLines[currentLine], textSpeed));
                
            }
        }
    }

    IEnumerator DisableGUI(float mTime)
    {
        yield return new WaitForSeconds(mTime);
        text.enabled = false;
        if(textImage != null)
        {
            textImage.enabled = false;
        }
        startItNow = false;
    }


    IEnumerator WaitWithDisabling(float mTime)
    {
        PlayerDisableUtility.Instance.PlayerAbility(false);
        yield return new WaitForSeconds(mTime);
        PlayerDisableUtility.Instance.PlayerAbility(true);
    }

    IEnumerator TextScroll(string lineOfText, float textTypeSpeed)
    {
        int letter = 0;
        text.text = "";
        isTyping = true;
        cancelTyping = false;

        while(isTyping && !cancelTyping && (letter < lineOfText.Length - 1))
        {
            text.text += lineOfText[letter];
            letter++;
            yield return new WaitForSeconds(textTypeSpeed);
        }

        //text.text = lineOfText;
        isTyping = false;
        cancelTyping = false;
    }


}
