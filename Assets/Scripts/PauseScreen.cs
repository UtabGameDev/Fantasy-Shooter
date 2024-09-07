using UnityEngine.SceneManagement;
using UnityEngine;

public class PauseScreen : MonoBehaviour
{
    [SerializeField] private string mainMenuScene;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void Resume()
    {
        GameManager.Instance.PauseUnpause();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(mainMenuScene);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
 