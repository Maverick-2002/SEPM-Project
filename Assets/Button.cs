using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    public void ScreenLoader(int SceneIndex)
    {
        SceneManager.LoadScene(SceneIndex);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Exit()
    {
        Application.Quit();
    }
}
