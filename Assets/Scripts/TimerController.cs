using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEditor.Build.Content;
public class TimerController : MonoBehaviour
{
    
    float Timer = 40f;
    public GameObject boss;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        EnemyFactoryController Boss = boss.GetComponent<EnemyFactoryController>();
        
        Timer -= Time.deltaTime;
       
        if (Timer <0.0f)
        {
           
                Boss.GetComponent<EnemyFactoryController>().CreateBoss();
                
        
            //SceneManager.LoadScene("TitleScene");
            //Hierarchy上にあるGameObjectから該当するGameObjectを探してくれる
            GameObject scene_manager = GameObject.Find("GameSceneManager");
            if (scene_manager != null)
            {
               // scene_manager.GetComponent<GameSceneController>().FailedGame();
            }
            
        }
        //Clamp(調整値、下限値、上限値）
        //調整値が下限値から上限値までの間で調整される
        Timer = Mathf.Clamp(Timer, 0.0f, 10.0f);
        TextMeshProUGUI tmpro = GetComponent<TextMeshProUGUI>();
        //ToString 変数情報を文字列に帰る
        tmpro.text = Timer.ToString("f2");
    }
}
