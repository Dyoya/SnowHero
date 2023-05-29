using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySnowball : MonoBehaviour
{
    private LayerMask destroyLayer;  // ���� �ı���ų ���̾�

    public void SetDestroyLayer(LayerMask layer)
    {
        destroyLayer = layer;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (destroyLayer == (destroyLayer | (1 << collision.gameObject.layer)))
        {
            Destroy(gameObject);
        }
    }
}
