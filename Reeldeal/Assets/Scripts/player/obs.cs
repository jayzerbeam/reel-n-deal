using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerWaterCollision : MonoBehaviour
{
    public GameObject player;
    public GameObject water;
    public string mainMenuSceneName;

    private void Update()
    {
        if (player != null && water != null)
        {
            if (player.GetComponent<Collider>().bounds.Intersects(water.GetComponent<Collider>().bounds))
            {
                Destroy(player);

                SceneManager.LoadScene(mainMenuSceneName);
            }
        }
    }
}