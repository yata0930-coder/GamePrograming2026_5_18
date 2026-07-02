using UnityEngine;
using UnityEngine.Rendering.Universal;

public class BossAttack_2 : MonoBehaviour
{
    public GameObject EffectPrefab=null;

    float speed = 3f;
    int AttackHp = 5;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(-speed * Time.deltaTime, 0, 0);
        if (transform.position.x <= -10f)
        {
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        //PlayerBulletタグかRobotタグを持つオブジェクトに触れると実行
        if (collision.tag == "PlayerBullet")
        {
            AttackHp--;
            if (AttackHp == 0)
            {
                //このオブジェクトを破壊
                Destroy(gameObject);
                //エフェクトを生成
                Instantiate(EffectPrefab, transform.position, Quaternion.identity);
            }
        }

    }
}
