using UnityEngine;

public class EfectController : MonoBehaviour
{
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //このオブジェクトを生成し、1秒後に破壊
        Destroy(gameObject,1.0f);
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
