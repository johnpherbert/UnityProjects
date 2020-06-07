using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    [SerializeField] float delayInSeconds = 2f;
    // Start is called before the first frame update
    public void LoadStartMenu()
    {
        SceneManager.LoadSceneAsync(0);
    }
    public void LoadMainGame()
    {
        SceneManager.LoadSceneAsync("Game");
        FindObjectOfType<GameSession>().ResetGame();
    }

    public void LoadGameOver()
    {
        StartCoroutine(WaitAndLoad());

    }

    private IEnumerator WaitAndLoad()
    {
        yield return new WaitForSeconds(delayInSeconds);
        SceneManager.LoadSceneAsync("Game Over");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
