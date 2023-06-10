using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Magic_ball : MonoBehaviour
{
    public int damage;
    Rigidbody rb;
    float startScale = 1f;
    float targetScale = 7f;
    float scaleDuration = 2f;
    float angularPower = 2;
    bool isShoot;
    Transform player;
    bool isTriggered = false;
    float scaleValue;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Terrain")
        {
            Destroy(gameObject, 4f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isTriggered && other.gameObject.tag == "Player")
        {
            player.GetComponent<Player>().Damage(damage);
            Destroy(gameObject);
            isTriggered = true;
        }
    }

    void Awake()
    {
        player = GameObject.Find("Player").transform;
        if (player == null)
        {
            Debug.LogError("Player object not found!");
        }
        rb = GetComponent<Rigidbody>();
        StartCoroutine(GrowBeforeRoll());
    }

    IEnumerator GrowBeforeRoll()
    {
        float currentTime = 0f;
        float startScaleValue = startScale;

        while (currentTime < scaleDuration)
        {
            currentTime += Time.deltaTime;
            float t = currentTime / scaleDuration;
            scaleValue = Mathf.Lerp(startScaleValue, targetScale, t);
            transform.localScale = Vector3.one * scaleValue;
            yield return null;
        }
        StartCoroutine(GainPower());
    }

    IEnumerator GainPower()
    {
        float elapsedTime = 0f;
        while (elapsedTime < 2f)
        {
            angularPower += 0.4f;

            // 회전력을 가하기 위해 Quaternion.Euler을 사용하여 회전값을 계산합니다.
            Quaternion rotation = Quaternion.Euler(angularPower, 0f, 0f);
            rb.MoveRotation(rb.rotation * rotation);

            Vector3 playerDirection = (player.position - transform.position).normalized;
            rb.AddForce(playerDirection.normalized * angularPower, ForceMode.Acceleration);

            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

}
