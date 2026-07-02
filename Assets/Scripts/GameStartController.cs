using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStartController : MonoBehaviour
{


    float timer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject SceneManager = GameObject.Find("GameSceneManager");
        Destroy(gameObject, SceneManager.GetComponent<GameSceneController>().startDirectionTime);
    }

    // Update is called once per frame
    void Update()
    {



    }
}
