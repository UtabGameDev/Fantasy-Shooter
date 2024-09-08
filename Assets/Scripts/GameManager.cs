using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private float waitAfterDying = 2f;
    [FormerlySerializedAs("evelEnding")] [FormerlySerializedAs("LevelEnding")] [FormerlySerializedAs("ending")] [HideInInspector] public bool levelEnding;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseUnpause();
        }
    }

    public void PlayerDied()
    {
        StartCoroutine(PlayerDiedCo());
    }

    private IEnumerator PlayerDiedCo()
    {
        yield return new WaitForSeconds(waitAfterDying);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

    public void PauseUnpause()
    {
        if (UIController.Instance.pauseScreen.activeInHierarchy)
        {
           UIController.Instance.pauseScreen.SetActive(false);
           Cursor.lockState = CursorLockMode.Locked;

           Time.timeScale = 1;
        }
        else
        {
            UIController.Instance.pauseScreen.SetActive(true);
            Cursor.lockState = CursorLockMode.None;

            Time.timeScale = 0;
        }
    }
}
