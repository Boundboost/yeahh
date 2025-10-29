using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pausemenu;
    public void Return()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("main menu");
    }

    public void Pause()
    {
        pausemenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void play()
    {
        pausemenu.SetActive(false);
        Time.timeScale = 1;
    }
}
