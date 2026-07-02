using UnityEngine;

public class EnemyFactoryController : MonoBehaviour
{
    /*
        Random.Range(最小整数、最大整数)
            最小整数～最大整数-1までの範囲
    　　　　Random.Range(0,100)=>0～99
    
       Random.Range(最小実数、再々実数)
        最小実数～最大実数までの範囲
    　　Random.Range(0.0f,100.0f)=>0.0f～100.0f
        */
    //外部からシーンマネージャーを読み込む
    public GameObject SceneManager;

    /*
       配列情報の名称
    　　　　要素：
    　　　　　配列に保存されている1つ1つの値

    　　　　要素数：
    　　　　　配列に保存されている要素の数

    　　　　要素番号（インデックス、そえ字）：
    　　　　　配列のうちの要素を識別するために設定された番号

　　　　　　要素番号の特徴：
    　　　　　0から始まる
    　　　　　連番になっている
     */
    //外部から敵のプレハブを読み込む
    public GameObject[] enemies = null;
    //GameObject enemise[];   ←c++↑c#
    //外部からボスのプレハブを読み込む
    public GameObject BossPrefab;
    //時間を測るための変数(今回は敵の生成時間のクールタイムのために使用する)
    float timer = 0.0f;
    //ゲームが開始するまでの時間を測る変数
    float startTimer;
    //ランダムを扱う変数（今回は敵の生成に使用する）
    int rand;
    //敵とボスの生成を制限するための変数
    bool limit=false;
    
    //ボスを生成するための関数
    public void CreateBoss()
    {
        //limitがfalseで実行
        if (limit == false)
        {
            //ボスを生成
            Instantiate(BossPrefab,transform.position,Quaternion.identity);
            //limitをtrueに変更
            //これによりボスが生成されると2体目以降のボスの生成とほかの敵の生成をとめることができる　
            limit = true;
        }
        
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       
        /*
          プログラム中に配列を作る場合（C#バージョン）
        　　変数名＝new 型[要素名]
         */
        //enemies=new GameObject[10];←これ
    }

    // Update is called once per frame
    void Update()
    {
        //limitがflaseで実行
        if (limit == false)
        {
            //GameSceneControllerを入手し、GameStartTimerに入れる
            GameSceneController GameStartTimer = SceneManager.GetComponent<GameSceneController>();
            //startTimerで時間を測る
        　　startTimer += Time.deltaTime;
        //startTimerがGameSceneController内のstartDIrectionTimeの値以下で実行
        if (startTimer <= GameStartTimer.startDirectionTime)
        {
            //ここより下の命令をスキップする
            return;
        }
        　　//timerで時間を測る
            timer += Time.deltaTime;
            //timerが0.5f以上で実行
            if (timer >= 0.5f)
            {
                //敵を生成する位置を決める変数
                Vector3 pos = transform.position;
                //縦方向のどこに生成するかランダムで決める
                pos.y = Random.Range(-4.0f, 4.0f);
                //randの中に0から100の数字をランダムで入れる
                rand = Random.Range(0, 100);
                //randの値が49以下で実行
                if (rand <= 49)
                {
                    //enemies[0]に入っている敵を生成する
                    Instantiate(enemies[0], pos, Quaternion.identity);
                    //timerをリセット
                    timer = 0;
                }
                //timerの値が49以下ではなく50以上で実行
                else if (rand >= 50)
                {
                    //enemies[1]に入っている敵を生成する
                    Instantiate(enemies[1], pos, Quaternion.identity);
                    //timerをリセット
                    timer = 0;
                }
            }
        }
    }
}
