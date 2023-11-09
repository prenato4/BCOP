using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneM : MonoBehaviour
{
    public void ChangeScene()
    {
        SceneManager.LoadScene(0);
    }
    public void ChangeScene1()
    {
        SceneManager.LoadScene(1);
    }
    public void ChangeScene2()
    {
        SceneManager.LoadScene(2);
    }
    public void ChangeScene3()
    {
        SceneManager.LoadScene(3);
    }
    public void ChangeScene4()
    {
        SceneManager.LoadScene(4);
    }
    public void Close()
    {
        Application.Quit();
    }
}
