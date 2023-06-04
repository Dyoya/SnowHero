using UnityEngine;

public class Magic_spear : MonoBehaviour
{
    public int damage;
    public Transform target;
    Transform player;
    bool isDestroyed = false;
    float speed = 20.0f; // �߻� �ӵ� ���� ����

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
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            // �÷��̾� �������� �̵�
            Vector3 direction = (target.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
        }
    }
}
