using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    [SerializeField] float nextLevelTime;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(nameof(NextLevel));
        }
    }

    IEnumerator NextLevel()
    {
        yield return new WaitForSeconds(nextLevelTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
