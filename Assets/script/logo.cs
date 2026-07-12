using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class logo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("GoToStartGame", 3);
    }
    public void GoToStartGame()
    {
        SceneManager.LoadScene("loginScene");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
