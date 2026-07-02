using UnityEngine;

public class BossAttack_3 : MonoBehaviour
{
    public Transform player;
    float speed = 15f;
    bool target=false;
    float timer=0;

    Vector3 direction;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // プレイヤー方向のベクトル
        Vector2 dir = (player.position - transform.position).normalized;

        // 角度を求める
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        // Z軸だけ回転させる
        transform.rotation = Quaternion.Euler(0, 0, angle+180);
    }

    // Update is called once per frame
    void Update()
    {

        if (target == false) {
            direction = player.transform.position - transform.position;
            direction.z = 0;
            direction = direction.normalized;
            
            target = true;
                }
        transform.position += direction * speed * Time.deltaTime;
        timer += Time.deltaTime;
        if (timer >= 8f)
        {
            Destroy(gameObject);
        }
    }
 
}
