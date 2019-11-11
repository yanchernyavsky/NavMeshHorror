using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{

    public Animator animator;

    private int levelToLoad;

   
    public void FadeToNextLevel()
    {
        FadeToLevel(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void FadeToLevel(int levelIndex)
    {
        levelToLoad = levelIndex;
        animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(levelToLoad);
    }


    IEnumerator Restart()
    {
        yield return new WaitForSeconds(5f);
        FadeToLevel(SceneManager.GetActiveScene().buildIndex);
        
    }

    IEnumerator EndGame()
    {
        yield return new WaitForSeconds(5f);
        FadeToLevel(0);

    }

    private void Update()
    {
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().PlayerIsDead == true)
        {
            StartCoroutine("Restart");
        }
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().Win == true)
        {
            StartCoroutine("EndGame");
        }
    }

}