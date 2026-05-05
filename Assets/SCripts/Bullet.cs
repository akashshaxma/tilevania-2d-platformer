//using UnityEngine;

//public class Bullet : MonoBehaviour
//{
//    [SerializeField] float bulletSpeed = 20f;
//    Rigidbody2D myRigidbody;
//    PlayerMovement player;
//    float xSpeed;
//    void Start()
//    {
//        myRigidbody = GetComponent<Rigidbody2D>();
//        player = FindAnyObjectByType<PlayerMovement>();
//        xSpeed = player.transform.localScale.x * bulletSpeed;
//    }


//    void Update()
//    {
//        myRigidbody.linearVelocity = new Vector2 ( xSpeed, 0f);
//    }

//    void OnTriggerEnter2D(Collider2D other) 
//    {
//        if (other.tag == "Enemy")
//        {
//            Destroy(other.gameObject);
//        }
//        GetComponent<PersistentObject>().DestroyPersistentObject();
//    }
//    private void OnCollisionEnter2D(Collision2D other)
//    {
//        Destroy(gameObject,1f);
//    }
//}
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float bulletSpeed = 20f;

    Rigidbody2D myRigidbody;
    PlayerMovement player;
    float xSpeed;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        player = FindAnyObjectByType<PlayerMovement>();
        xSpeed = player.transform.localScale.x * bulletSpeed;
    }

    void Update()
    {
        myRigidbody.linearVelocity = new Vector2(xSpeed, 0f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyMovement enemy = other.GetComponent<EnemyMovement>();

            if (enemy != null)
            {
                enemy.KillEnemy();
            }
        }

        Destroy(gameObject);
    }
}