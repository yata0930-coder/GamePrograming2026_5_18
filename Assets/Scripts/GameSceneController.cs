using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneController : MonoBehaviour
{
    public GameObject StartPrefab;
    public GameObject OverPrefab;
    public GameObject ClearPrefab;
    //開始演出の時間
    //敵の生成は演出の間行わない
    public float startDirectionTime = 5.0f;
    public float GameOverTimer = 0f;
    public float GameClearTimer = 0f;

    bool isFinished = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //プレハブだけ指定すると座標などはプレハブの情報がそのまま使われる
        GameObject obj_Start=Instantiate(StartPrefab);
       
    }

    // Update is called once per frame
    void Update()
    {
        if (isFinished == true)
        {
            SceneManager.LoadScene("TitleScene");
        }
    }

    public void ClearedGame()
    {
        GameObject obj_Clear = Instantiate(ClearPrefab);
        GameClearTimer += Time.deltaTime;
        Debug.Log("お前の勝ち");
        if(GameClearTimer>= startDirectionTime)
        isFinished = true;
        //SceneManager.LoadScene("TitleScene");
    }
    public void FailedGame()
    {
        GameObject obj_Over = Instantiate(OverPrefab);
       GameOverTimer += Time.deltaTime;
        Debug.Log("お前の負け");
        if (GameOverTimer >= startDirectionTime)
        isFinished = true;
        //SceneManager.LoadScene("TitleScene");
    }
}
