using UnityEngine;
using UnityEngine.SceneManagement;

public class playerDeath : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Lake") || other.CompareTag("Waterfall") || other.CompareTag("River") || other.CompareTag("Spring") || other.CompareTag("Ocean"))
        {
            Destroy(gameObject);
            SceneManager.LoadScene("MainMenu");
        }
    }
}