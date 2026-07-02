using JetBrains.Annotations;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.UI;


public class BoostGaugeController : MonoBehaviour
{
    //外部からプレイヤーオブジェクトを読み込む
    public GameObject PlayerObject;
    //Sliderを入れるためのもの
    Slider slider = null;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Sliderの情報をsliderに入れる
        slider = GetComponent<Slider>();
        //sliderに何か入っていたら実行
        if (slider != null)
        {
            //RobotControllerを入れるためのもの
            RobotController robot_controller = null;
            //RobotControllerの情報をrobot_controllerに入れる
            robot_controller = PlayerObject.GetComponent<RobotController>();
            //スライダーの最小値を管理する変数
            slider.minValue =0.0f;
            //スライダーの最大値を管理する変数
            slider.maxValue = robot_controller.boostTime;
            //スライダーがどれくらい伸びているかを管理する変数
            slider.value = 3.0f;
           
        }
        //何もなかったら
        else
        {
            //デバッグログにスライダーがないよーと出力する
            Debug.Log("Slider is null");
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //RobotControllerを入れるためのもの
        RobotController robot_controller = null;
        //RobotControllerの情報をrobot_controllerに入れる
        robot_controller = PlayerObject.GetComponent<RobotController>();
        //スライダーに何か入っていたら
        if (slider != null)
        {
            //スライダーの長さをRobotController内のboostTimeとboostTimerを
            //参照して残りのブースト時間を変動させる
            slider.value = robot_controller.boostTime - robot_controller.boostTimer;
           
           
        }
       
    }
}
