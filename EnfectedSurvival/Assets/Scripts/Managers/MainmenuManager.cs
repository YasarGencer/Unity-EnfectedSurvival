using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainmenuManager : MonoBehaviour
{
    public void WaveMode()
    {
        SceneManager.LoadScene("WaveMode");
    }
    public void Close()
    {
        Application.Quit();
    }
}
