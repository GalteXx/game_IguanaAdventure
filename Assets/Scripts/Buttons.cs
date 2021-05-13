using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Buttons : MonoBehaviour
{
    bool flag;
    public GameObject pauseMenu;
    public void Start()
    {
        flag = false;
    }
    // func to switch scene
    public void Play()
    {
        // preventing multiple clicks on the Play button
        if (!flag)
        {
            // switching flags to true
            FindObjectOfType<MenuIguana>().menuWalk = true;
            flag = true;
        }
        else if (!FindObjectOfType<MenuIguana>().menuWalk)
        {
            // loading another scene
            SceneManager.LoadScene(0);
        }
    }
    // some funcs to see player's profile, to share the game, to like the game and look for information about the game. Until our game appears on the Steam there will be links to our team members' VK profiles
    public void User()
    {

    }
    public void Share()
    {

    }
    public void Star()
    {
        Application.OpenURL("https://vk.com/m4ksikk");
        Application.OpenURL("https://vk.com/dsisakov");
        Application.OpenURL("https://vk.com/mgoryachev1");
        Application.OpenURL("https://vk.com/amphibian_kuro");

    }
    public void Info()
    {
        
    }
    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }
    
    public void Exit()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }
}
