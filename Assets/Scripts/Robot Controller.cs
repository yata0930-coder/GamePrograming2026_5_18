using TMPro;
using Unity.Hierarchy;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RobotController : MonoBehaviour
{
    //外部から弾のプレハブを読み込む
    public GameObject PlayerBulletPrefab = null;
    //public GameObject PlayerBigBulletPrefab = null;
    //外部からプレイヤーのプレハブを読み込む
    public GameObject PlayerPrefab = null;
    //外部からプレイヤーのエフェクトのプレハブを読み込む
    public GameObject PlayerEfectPrefab = null;
    //外部からエンジンファイアのオブジェクトを読み込む
    public GameObject EngineFireobj = null;
   
    //スピードアップしたときに何倍にするかを決める変数
    float speedup = 1.0f;
    //外部から触れるブースト時間をかんりする変数
    public float boostTimer = 0.0f;
    //外部から触れるブーストのON、OFFを管理する変数
    bool boost = false;
    //外部から触れるプレイヤーのHPを管理する変数
    public int life = 3;
    //外部から触れるプレイヤーの最大体力を管理する変数
    public int maxLife = 3;
    //外部から触れるブーストの時間を管理する変数
    public float boostTime=5.0f;
    //外部から触れる敵を倒した数を管理する変数
    public int defeatedCounter=0;
    /*
          public
              メンバ変数に設定することで、以下の効果を得られる
    　　　　　　　　・Inspectorに項目が追加される
    　　　　　　　　・外部（別のスクリプト）からアクセスできる
     */
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //エンジンファイアのオブジェクトを非表示にする
        EngineFireobj.GetComponent<Renderer>().enabled = false;

      
       
    }

    // Update is called once per frame
    void Update()
    {
        //プレイヤーの速度を管理する変数(ここでスピードアップもさせるため最初に1.0fと定義する)
        float speed = 10.0f * speedup;
        //プレイヤーのFPSに作用されない速度を管理する変数
        float move = speed * Time.deltaTime;  
        //プレイヤーの縦方向の移動を管理する変数
        float move_x = 0.0f;
        //プレイヤーの横方向の移動を管理する変数
        float move_y = 0.0f;
       
        
        //○
        //上矢印キーが押されると実行
        if (Input.GetKey(KeyCode.UpArrow))
        {
            //上方向
            move_y += 1.0f;
        }
        //上矢印キーを押されると実行
        if (Input.GetKey(KeyCode.DownArrow))
        {
            //下方向
            move_y -= 1.0f;
        }
        //上矢印キーを押されると実行
        if (Input.GetKey(KeyCode.RightArrow))
        {
            //右方向
            move_x += 1.0f;
        }
        //上矢印キーを押されると実行
        if (Input.GetKey(KeyCode.LeftArrow))
            {
                //左方向
                move_x -= 1.0f;
            }
        //スペースキーを押すと実行
        if (Input.GetKeyDown(KeyCode.Space))
            {
            　　//弾を生成させ、その情報をbulletに入れる
                GameObject bullet=Instantiate(PlayerBulletPrefab, transform.position, Quaternion.identity);
            //bulletの中にPlayerBulletControllerを取得し、プレイヤーが弾を撃ったことを知らせる
            bullet.GetComponent<PlayerBulletController>().robotObject = gameObject;
            }
        
        　　//正規化のために方向ベクトルを計算し、vecに入れる
            float vec = Mathf.Sqrt(move_x * move_x + move_y * move_y);
        //0割り対策
        if (vec > 0.0)
        {
            //プレイヤーのx成分をvecで割る
            move_x /= vec;
            //プレイヤーのy成分をvecで割る
            move_y /= vec;
            //さっき計算したものにそれぞれスピードと時間を掛ける
            move_x *= speed * Time.deltaTime;
            move_y *= speed * Time.deltaTime;
            //プレイヤーを移動させる
            transform.Translate(move_x, move_y, 0.0f);
        }
        
        //boost ON
        /*
            if(条件A && 条件B)
        {
        　　　条件Aと条件Bが両方とも成立している場合に実行する
        }

        if(条件A || 条件B)
        {
           条件Aか条件Bのどちらかが成立している場合に実行する
        }
         */
        //エンターキーを押すと実行
        if (Input.GetKeyDown(KeyCode.Return))
            {
            //boostがfalseで実行
                if (boost == false)
                {
                    //boostをtrueにする
                    boost = true;
                    //boostTimerをリセット
                    boostTimer = 0f;
                    //エンジンファイアを表示させる
                    EngineFireobj.GetComponent<Renderer>().enabled = true;
                }
            }
            //boost チェック
            if (boost == true)
            {
                //時間加算
                boostTimer += Time.deltaTime;
                //スピードは何倍にするか
                speedup = 2.0f;
                //時間とボタンチェック
                if (boostTimer >= boostTime || Input.GetKeyUp(KeyCode.A))
                {
                    //boost OFF
                    boost = false;
                    //倍にしたスピードを戻す
                    speedup = 1;
                    //エンジンファイアを非表示にする
                    EngineFireobj.GetComponent<Renderer>().enabled = false;
                }
            }

        }
        void LateUpdate()
        {
            //画面外に行かないようにするための変数
            Vector3 position = transform.position;
        /*
          なぜ 200 で割るのか？
           Screen.width / Screen.height はピクセル数なので、
           そのまま transform.positionと比較すると値が大きすぎる。

      　　1マスを100ピクセルとして扱っているため、
      　　ピクセルの値を 100 で割ることでワールド座標に近い値に変換している。

      　　さらに左右両端を扱うため 2で割る→ 合計で 200 で割っている
         */
        //画面の横の長さから200を割る
        float half_screen_width = Screen.width / 200.0f;
            //画面の縦の長さから200を割る
            float half_screen_height = Screen.height / 200.0f;
        //ここから下は画面外に出ないようにするためのプログラム
            if (position.x > half_screen_width)
            {
            　　//右に出ないよう
                position.x = half_screen_width;
            }
            else if (position.x < -half_screen_width)
            {
            　　//左に出ないよう
                position.x = -half_screen_width;
            }
            if (position.y > half_screen_height)
            {
            　　//上に出ないよう
                position.y = half_screen_height;
            }
            else if (position.y < -half_screen_height)
            {
            　　//下に出ないよう
                position.y = -half_screen_height;
            }
            //位置を反映する
            transform.position = position;
        //画面外に出ないようにするためのプログラム終了
        }
        void OnTriggerEnter2D(Collider2D collision)
        {
       
        //Enemyタグがついたオブジェクトに触れたら実行
        if (collision.tag == "Enemy"||collision.tag=="Attack")
            {
              //life++;//life+=1と同じ
              //life--;//life-=1と同じ
              //++:インクリメント
              //　　変数を1増やす
              //--:デクリメント
              //　　変数を1減らす

            //プレイヤーのHPを1減らす
            life --;
            //残りHPをデバッグログに出力
            Debug.Log("残りライフ"+life);
            //HPが0以下になったら
            if (life <= 0)
            {
                //Destroy(gameObject);
                //プレイヤーエフェクトを生成
                Instantiate(PlayerEfectPrefab, transform.position, Quaternion.identity);
                //SceneManager.LoadScene("TitleScene");
                //ゲームシーンからGameSceneManagerを探し、scene_managerを入れる
                GameObject scene_manager = GameObject.Find("GameSceneManager");
                //scene_managerに何か入っていたら実行
                if (scene_manager != null)
                {
                    //OnTriggerやOnCollision内でSceneを切り替えない方が良い
                    //GameSceneController内にあるFailedGame関数を呼び出す
                    scene_manager.GetComponent<GameSceneController>().FailedGame();
                }
                /*
                同一スクリプト内の同一タイミングで、DestroyとLoadSceneを行ったら
                フリーズする可能性があるので基本やらない
                */
            }
          }
        }
      }

