using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.PlayerSettings;

public class PlayerBulletController : MonoBehaviour
{
    
    //外部からロボットオブジェクトを読み込む
    public GameObject robotObject = null;
    //弾のスピード
    float BulletSpeed = 20f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //弾のスピードがFPSで差が出ないように時間をかける
        float move = BulletSpeed * Time.deltaTime;
        //弾をx方向に移動させる
        transform.Translate(move, 0, 0);
        //Renderer=>描画関連の機能を持つコンポーネント
        Renderer renderer = GetComponent<Renderer>();
        //弾が画面外に行ったら
        if (renderer.isVisible == false)
        {
            //このオブジェクトを破壊
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        //Enemyタグをもつオブジェクトに触れたら実行
        if (collision.tag == "Enemy")
        {
            //RobotControllerを入手し、bulletに入れる
            RobotController bullet = robotObject.GetComponent<RobotController>();
            //RobotController内のdefeatedCounterを＋1する
            bullet.defeatedCounter++;
            //defeatedCounterが3以上になったら
            if (bullet.defeatedCounter >= 999999)
            {
                //SceneManager.LoadScene("TitleScene");
                //ゲームシーン内からGameSceneManagerを探し、scene_managerに入れる
                GameObject scene_manager = GameObject.Find("GameSceneManager");
                //scene_manager内に何か入っていたら実行
                if (scene_manager != null)
                {
                    //scene_manager.GetComponent<GameSceneController>().ClearedGame();
                }
               
            }
            //3より小さかったら
            else
            {
                //このオブジェクトを削除
                Destroy(gameObject);
            }
        }
        //Bossタグを持つオブジェクトに触れたら実行
        else if (collision.tag == "Boss"||collision.tag=="Attack")
        {
            //このオブジェクトを破壊
            Destroy(gameObject);
        }
    }
}
