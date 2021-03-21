using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Buttons : MonoBehaviour
{
    bool boo = false;
    public void Play()
    {
        if (!boo)
        {
            FindObjectOfType<MenuIguana>().menuWalk = true;
            boo = true;
        }
        else if (!FindObjectOfType<MenuIguana>().menuWalk)
        {
            SceneManager.LoadScene(0);
        }
    }
    public void User()
    {
        Application.OpenURL("https://vk.com/mgoryachev1");
    }
    public void Share()
    {
        Application.OpenURL("https://vk.com/amphibian_kuro");
    }
    public void Star()
    {
        Application.OpenURL("https://vk.com/m4ksikk");
    }
    public void Info()
    {
        Application.OpenURL("https://vk.com/dsisakov");
    }
}
