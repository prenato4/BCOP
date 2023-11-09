using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public GameObject Pause;

    public GameObject Over;

    private bool IsPaused;

    public static GameController instance;
    
    public Player player;
    
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        PauseGame();
    }

    public void PauseGame()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            IsPaused = !IsPaused;
            Pause.SetActive(IsPaused);
            
        }

        if (IsPaused)
        {
            Time.timeScale = 0f;
            //player.SomP.Pause();
            
            
        }
        else
        {
            Time.timeScale = 1f;
            //player.SomP.UnPause();
        }
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        Over.SetActive(true);

    }
    
}
