using UnityEngine;

public class Enemy02Controller : MonoBehaviour
{
    //外部からエフェクトプレハブを読み込む
    public GameObject Effectprefab = null;
    //時間を測る変数
    float timer = 0.0f;
    //上に行く速度を管理する変数
    float speed_x=0.01f;
    //下に行く速度を管理する変数
    float speed_y = 0.02f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       //時間を測る
        timer += Time.deltaTime;
        
        //上下に移動させつつ、左に動かす
        transform.Translate(-speed_x, speed_y, 0);
        //timerが1以上で実行
        if (timer >= 1)
        {
            //－1を掛けて今とは逆の方向に進ませる
            speed_y *= -1f;
            //timerをリセット
            timer = 0;
        }
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

