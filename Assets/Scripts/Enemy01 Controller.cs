using System.Threading;
using Unity.Hierarchy;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Enemy01Controller : MonoBehaviour
{
    //外部からエフェクトプレハブを読み込む
    public GameObject Effectprefab = null;
  
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() { 
        
    
        //transform.position.x=>現在アタッチされているオブジェクトのX座標を見る
        //transform.position.y=>""""""""""""""""""""""""""""""""""""Y""""""""""
        //左に動かす(FPSに作用されないよう時間を掛ける)
        transform.Translate(-5f*Time.deltaTime, 0, 0);
        //今の位置が-10以下で実行
        if (transform.position.x <= -10)
        {
            //このオブジェクトを破壊
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        //PlayerBulletタグかRobotタグを持つオブジェクトに触れると実行
        if (collision.tag == "PlayerBullet"|| collision.tag == "Robot")
        {
            //このオブジェクトを破壊
            Destroy(gameObject);
            //エフェクトを生成
            Instantiate(Effectprefab, transform.position, Quaternion.identity);

        }
       
    }
}
