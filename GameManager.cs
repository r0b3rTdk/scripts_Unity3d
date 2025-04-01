using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool isPaused = false;
    public GameObject img;
    private void Start()
    {
        if(Time.timeScale == 0f)
        {
            Resume();
        }
    }
    void Update()
    {
        ReturnGame();
        CallPauseResume();
    }
    public void ReturnGame()
    {
        if(Input.GetKeyDown(KeyCode.M))
        {
            SceneManager.LoadScene(0);
        }
    }
    public void CallPauseResume()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
    public void Pause()
    {
        Time.timeScale = 0f;
        isPaused = true;
        //Cursor.lockState = CursorLockMode.None;
        //Cursor.visible = true;
        img.SetActive(true);
    }
    public void Resume()
    {
        Time.timeScale = 1f;
        isPaused = false;
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
        img.SetActive(false);

    }
}
