using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.XR.Haptics;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using static UnityEditor.PlayerSettings;

public class BossController : MonoBehaviour
{
    public GameObject[] BossAttackPrefabs = null;

   /*enumとは→文字に値を入れることができる
      何も値を入れていない場合は一番最初から0が入力される
      次に入っている文字にも何も書かれていない場合は前に入っている値に＋１された値が入る
   */

   //ボスの行動の設定
    enum State
    {
        //出現状態
        Apear,
        //バトル状態
        Battle,
        //死亡状態
        Dead,
    }
    //ボスのHP 
    int BossLife = 10;
    //ボスの攻撃の選択の変数
    int attack;
    //ボスの攻撃時間
    float attacktimer;
    //ボスの攻撃が重複しないようにするための変数
    bool cooltime = false;
    //stateにApearを設定
    State state = State.Apear;
    //外部からエフェクトのプレハブを読み込む
    public GameObject EffectPrefab = null;
    //ランダムを扱う変数（今回はエフェクトのランダム生成に使う）
    int rand;
    //時間を測る変数
    float timer = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //switch文で状態を切り替える
        switch (state)
        {
            //stateが出現状態の時
            case State.Apear:
                //関数Apearを呼び出す
                Apear();
                break;
            //stateがバトル状態のとき
            case State.Battle:
                //関数Battleを呼び出す
                Battle();
                break;
            //stateが死亡状態のとき
            case State.Dead:
                //関数Deadを呼び出す
                Dead();
                break;

        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        //ボスがプレイヤーの攻撃に当たったかつ、stateがBattleのときに実行
        if (collision.tag == "PlayerBullet" && state == State.Battle)
        {
            //ボスのHPを１減らす
            BossLife -= 1;
            //ボスのHPが０になったら実行
            if (BossLife <= 0)
            {
                //stateをDeadに切り替える
                state=State.Dead;
            }

        }
    }
    //stateがApear(出現状態)時の処理
    void Apear()
    {
        //ボスが出現するときのスピード
        float bossSpeed = 0.5f;
        //ボスがどこまで出てくるかを制限する処理
        float stop = 7.0f;
        //今のボスのx座標がstopの値以上だったら実行
        if (transform.position.x >= stop)
        {
            //ボスを左に動かす
            transform.Translate(-bossSpeed * Time.deltaTime, 0, 0);
        }
        //ボスのx座標がstopの値より小さかったら
        else
        {
            //登場が終わったのでBattleに変更
            state = State.Battle;
        }
    }
    //stateがBattle(バトル状態)時の処理
    void Battle()
    {
        if (cooltime != true)
        {
            attack = Random.Range(1, 5);
            cooltime = true;
            timer = 0;
            attacktimer = 0;
        }
        attacktimer += Time.deltaTime;
        timer += Time.deltaTime;
       
            switch (attack)
            {
            case 1:
                if (attacktimer >= 1f)
                {
                    Instantiate(BossAttackPrefabs[0], transform.position, Quaternion.identity);
                    attacktimer = 0;
                }
                if (timer >= 3)
                {
                    cooltime = false;
                }
                    break;
            case 2:
                if (attacktimer >= 1f)
                {
                    Vector2 pos = transform.position;
                    pos.y = Random.Range(-5, 5);
                    Instantiate(BossAttackPrefabs[1], pos, Quaternion.identity);
                    attacktimer = 0;
                }
                if (timer >= 7)
                {
                    cooltime = false;
                }
                break;
            case 3:
                if (attacktimer >= 1f)
                {
                    float ran=Random.Range(1,3);
                    Vector2 pos = transform.position;
                   
                    pos.x = Random.Range(-10,10);
                    if (ran >= 2) {
                        pos.y = Random.Range(6, 8);
                    }else
                    {
                        pos.y = Random.Range(-8, -6);
                    }
                       

                    GameObject atk_3 = Instantiate(BossAttackPrefabs[2], pos, Quaternion.identity);
                    atk_3.GetComponent<BossAttack_3>().player = GameObject.FindGameObjectWithTag("Robot").transform;
                    attacktimer = 0;
                }
                if (timer >= 6f)
                {
                    cooltime = false;
                }
                break;
            case 4:
                GameObject atk_4=null;
                if (attacktimer >= 2f)
                {
                    if (attacktimer >= 1f)
                    {
                        atk_4 = Instantiate(BossAttackPrefabs[3], transform.position, Quaternion.identity);
                       
                        attacktimer = 0;
                    }
                    atk_4.GetComponent<BossAttack_4>().player = GameObject.FindGameObjectWithTag("Robot").transform;
                    if (timer >= 3)
                    {
                        cooltime = false;
                    }
                    attacktimer = 0;
                }
                if (timer >= 5)
                {
                    cooltime = false;
                }
                break;
               
            }
            
    }
    //stateがDead(死亡状態)時の処理
    void Dead()
    {
        //時間を測る
        timer += Time.deltaTime;
        //timerの値が10以下で実行
        if(timer<=10) {
            //ボスを回転させる
            transform.Rotate(0, 0, 3000* Time.deltaTime);
            //ボスを右下に移動させる
            transform.Translate(1f*Time.deltaTime, -1f * Time.deltaTime, 0,Space.World);
            //死亡時のエフェクトの生成位置を決める変数
            Vector3 pos = transform.position;
                //横方向のどこに生成するかをランダムで決める
                pos.x += Random.Range(-2f, 2f);
            　　//縦方向のどこに生成するかをランダムで決める
            pos.y += Random.Range(-2f, 2f);
            　　//エフェクトを生成
                GameObject efect = Instantiate(EffectPrefab, pos, Quaternion.identity);
            //timerの値が10以上だったら
            } else
        {
            //ボスを破壊
            Destroy(gameObject);
            //GameSceneManagerをゲームシーン内から探し、scene_managerに入れる
            GameObject scene_manager = GameObject.Find("GameSceneManager");
            //GameSceneManagerの中のGameSceneControllerを見つけ、その中のClearGame関数を呼び出す
            scene_manager.GetComponent<GameSceneController>().ClearedGame();
        }

       
        }
}
