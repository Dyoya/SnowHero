using UnityEngine;

public class Magic_spear : MonoBehaviour
{
    public int damage;
    public Transform target;
    Transform player;
    bool isDestroyed = false;
    float speed = 30.0f; // �߻� �ӵ� ���� ����
    public Vector3 direction;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Terrain")
        {
            Destroy(gameObject, 2f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isDestroyed && other.gameObject.tag == "Player")
        {
            player.GetComponent<Player>().Damage(damage);
            Destroy(gameObject);
            isDestroyed = true;
        }
    }

    void Awake()
    {
        player = GameObject.Find("Player").transform;
        if (player == null)
        {
            Debug.LogError("Player object not found!");
        }
        if (target != null)
        {
            // �÷��̾� �������� �̵�
            direction = (target.position - transform.position).normalized;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            // �÷��̾� �������� �̵�
            transform.position += direction * speed * Time.deltaTime;
        }
    }
}
