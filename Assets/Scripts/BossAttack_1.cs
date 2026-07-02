using UnityEngine;

public class BossAttack_1 : MonoBehaviour
{
    float speed=3f;
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
}
