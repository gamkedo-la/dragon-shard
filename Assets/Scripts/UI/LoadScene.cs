using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    [Header("For use with buttons enter the string on the button")]
    public string Scene;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadSceneButton(string sceneName)
    {
        SceneManager.LoadScene(sceneName);

    }

    public void NoButtonLoadScene()
    {
        SceneManager.LoadScene(Scene);

    }

}
