using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneButton : MonoBehaviour
{
    [SerializeField] private string _sceneName = "Scene2";

    public void Load()
    {
        SceneManager.LoadScene(_sceneName);
    }
}
