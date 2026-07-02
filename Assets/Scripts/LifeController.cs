using System.Threading;
using UnityEngine;

public class LifeController : MonoBehaviour
{
    //外部からプレイヤーライフを読み込む
    public GameObject[] lifes=null;
    //外部からプレイヤーオブジェクトを読み込む
    public GameObject playerObject;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        /*
           for(カウンター変数の宣言と初期化；繰り返し条件チェック；カウンター変数の増減）
        {
         繰り返し処理
        }

        流れ
        　　①カウンター変数の宣言と初期化が行われる
        　　②繰り返し条件チェックを行う
        　　　　条件が成立していたら繰り返し処理実行
        　　③繰り返し処理を実行する
        　　④カウンター変数の増減処理を行う
        　　⑤②に戻る
         */
        //三回繰り返す
        for (int i = 0; i < 3; i++)
        {
  　　　　　//ライフを生成する         
            lifes[i].SetActive(true);
            Debug.Log("ライフ" + i + "生成");
        }
        //lifes[0].SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        RobotController robot_controller = null;
        robot_controller = playerObject.GetComponent<RobotController>();
        int life = robot_controller.life;
        int max_life = robot_controller.maxLife;

        for(int i = 0; i < max_life; i++)
        {
            if (life > i)
            {
                lifes[i].SetActive(true);
            }
            else
            {
                lifes[i].SetActive(false);
            }
        }
        /*if (life > 0)
        {
            lifes[0].SetActive(true);
        }
        else
        {
            lifes[0].SetActive(false);
        }
        if (life > 1)
        {
            lifes[1].SetActive(true);
        }
        else
        {
            lifes[1].SetActive(false);
        }
        if (life > 2)
        {
            lifes[2].SetActive(true);
        }
        else
        {
            lifes[2].SetActive(false);
        }*/
    }
}
