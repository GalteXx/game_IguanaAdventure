using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Buttons : MonoBehaviour
{ 
    public void Play()
    {
        SceneManager.LoadScene(0);
    }
    public void User()
    {
        Application.OpenURL("https://yandex.ru/images/touch/search?text=я%20ничего%20не%20сделал&img_url=http%3A%2F%2Frisovach.ru%2Fupload%2F2013%2F04%2Fmem%2Fkrasavchik_16630398_orig_.jpg&pos=0&rpt=simage&source=wiz");
    }
}
