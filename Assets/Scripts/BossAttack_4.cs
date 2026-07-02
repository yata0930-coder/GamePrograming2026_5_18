using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BossAttack_4 : MonoBehaviour
{
    public Transform player;
    public GameObject EffectPrefab;
    int HP = 3;
    float timer = 0;
    float speed = 2f;
    bool target = false;
    Vector3 direction = Vector3.zero;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      
            direction = player.transform.position - transform.position;
            direction.z = 0;
            direction = direction.normalized;

           
  
        transform.position += direction * speed * Time.deltaTime;
        if (timer >= 15)
        {
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerBullet")
        {
            HP--;
            if (HP <= 0)
            {
                Destroy(gameObject);
                Instantiate(EffectPrefab, transform.position, Quaternion.identity);
            }
        }
    }
}
