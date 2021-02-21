using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class DefeatLastBoss : MonoBehaviour {

    public string levelToLoad = "";
    TypeWriter twman;

    void Start()
    {
        twman = GetComponent<TypeWriter>();
    }

    void Update()
    {
        if(twman.hasFinished)
        {
            SceneManager.LoadScene(levelToLoad);
        }
    }
	
}
