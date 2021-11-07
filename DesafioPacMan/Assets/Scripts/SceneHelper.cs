using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneHelper : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Gameplay");
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void ResetGame()
    {
        PlayerPrefs.SetInt("PointsRecord", 0);
    }
}
