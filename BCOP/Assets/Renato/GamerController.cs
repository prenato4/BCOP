using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamerController : MonoBehaviour
{
    

    public static GamerController instance;

    public GameObject PAUSEoBJ;
    public GameObject GameOverobj;
    

    private bool IsPause;
    
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        
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
            IsPause = !IsPause;
            PAUSEoBJ.SetActive(IsPause);
        }

        if (IsPause)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    public void GameOver()
    {
        GameOverobj.SetActive(true);
    }

}
