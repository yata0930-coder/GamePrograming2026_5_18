using UnityEngine;

public class GameClearController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //このオブジェクトを生成し、5秒後に破壊する
        Destroy(gameObject, 5.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
