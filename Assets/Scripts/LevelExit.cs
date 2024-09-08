using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    [SerializeField] private string nextLevel;
    [SerializeField] private float waitEndLevel;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameManager.Instance.levelEnding = true;
            
            StartCoroutine(EndLevelCo());
        }
    }

    private IEnumerator EndLevelCo()
    {
        yield return new WaitForSeconds(waitEndLevel);
        SceneManager.LoadScene(nextLevel);
    }
}
