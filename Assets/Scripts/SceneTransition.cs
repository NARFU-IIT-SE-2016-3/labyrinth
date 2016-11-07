using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public string SceneName;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && SceneName != null)
        {
            SceneManager.LoadScene(SceneName);
        }
    }
}
