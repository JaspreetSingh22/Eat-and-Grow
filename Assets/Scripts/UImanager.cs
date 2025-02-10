using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UImanager : MonoBehaviour
{
   public void SceneChange(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
   public void DonePlaying()
    {
        Application.Quit();
    }
}
