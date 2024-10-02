using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 0.05f; // 敌人的移动速度

    [SerializeField] private GameObject player;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        if (player != null)
        {
            // 向玩家移动
            Vector3 direction = (player.transform.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime*0.5f;
        }
        else
        {
            Debug.LogError("PlayerNotFound!");
        }
    }

    /// <summary>
    /// 碰到玩家的box collider的时候执行
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collision)
    {

        switch (collision.gameObject.tag)
        {
            case "Player":
                PlayerControl player = collision.GetComponent<PlayerControl>();
                player.ChangeHealth(-10);
                Destroy(gameObject);
                break;
        }
    }
}
