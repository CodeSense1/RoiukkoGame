using UnityEngine;

public class PauseMenu : MonoBehaviour
{

    public GameObject pauseMenu;
    private bool paused = false;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            paused = pauseMenu.activeSelf;
            if (paused)
            {
                Unpause();
            } else
            {
                Pause();
            }

        }   
    }

    public void Unpause()
    {
        paused = false;
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }

    public void Pause()
    {
        paused = true;
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
    }
}
