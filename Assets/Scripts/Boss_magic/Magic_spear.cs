using UnityEngine;

public class Magic_spear : MonoBehaviour
{
    public int damage;
    Transform player;
    bool isDestroyed = false;
    float speed = 30.0f; // �߻� �ӵ� ���� ����
    public Vector3 direction;


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
        if (player != null)
        {
            // �÷��̾� �������� �̵�
            direction = (player.position - transform.position).normalized;
        }
        Destroy(gameObject, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            // �÷��̾� �������� �̵�
            transform.position += direction * speed * Time.deltaTime;
        }
    }
}
