using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
  public void StartGame()
    {
        SceneManager.LoadScene("MainGame"); // loads the level
    }
    public void QuitGame()
    {
        Application.Quit(); // closes the game
        Debug.Log("Raged Quit!!!");
    }

    public void Settings()
    {
        SceneManager.LoadScene("SettingsMenu"); // loads settings
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu"); // loads mainmenu
    }
}
