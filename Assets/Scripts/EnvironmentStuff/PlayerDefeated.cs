using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerDefeated : MonoBehaviour {

    TypeWriter twman;
    public float timeBeforeChanging = 15f;
    void Start()
    {
        twman = GetComponent<TypeWriter>();
    }

	void Update()
    {
        if (CheckpointManager.Instance.startCinematicStuff && !twman.startDialogue)
        {
            PlayerDisableUtility.Instance.PlayerAbility(false);
            twman.startDialogue = true;
        }
        if(CheckpointManager.Instance.isDead)
        {
            if(twman.hasFinished)
            {
                SceneManager.LoadScene("Level1_1");
            }
        }
        
    }

    IEnumerator ChangeSceneToOne()
    {
        yield return new WaitForSeconds(timeBeforeChanging);
        SceneManager.LoadScene("Level1_1");
    }
}
