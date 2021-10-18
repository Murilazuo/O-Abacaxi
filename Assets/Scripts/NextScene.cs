using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    [SerializeField] float nextLevelTime;
    [SerializeField] bool lastLevel = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ShowNewText.showNewText.NewText("Passou de Fase!");
            collision.GetComponentInChildren<Animator>().SetTrigger("Win");
            StartCoroutine(nameof(NextLevel));
            Player player = collision.GetComponent<Player>();
            player.canMove = false;
            player.Stop();
        }
    }

    IEnumerator NextLevel()
    {
        yield return new WaitForSeconds(nextLevelTime);
        if (lastLevel)
            SceneManager.LoadScene("End");
        else SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        
    }
}
