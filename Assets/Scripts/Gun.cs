using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    // crosshair�� ���� �Ӽ�
    public Transform crosshair;

    public Transform bulletImpact; // �Ѿ� ���� ȿ��
    ParticleSystem bulletEffect;   // �Ѿ� ���� ��ƼŬ �ý���
    AudioSource bulletAudio;       // �Ѿ� �߻� ����
    void Start()
    {
        // �Ѿ� ȿ���� ��ƼŬ �ý��� ������Ʈ�� ��������
        bulletEffect = bulletImpact.GetComponent<ParticleSystem>();
        // �Ѿ� ȿ�� ����� �ҽ� ������Ʈ ��������
        bulletAudio = bulletImpact.GetComponent<AudioSource>();
    }
    void Update()
    {
        ARAVRInput.DrawCrosshair(crosshair);

        // ����ڰ� IndexTrigger ��ư�� ������
        if (ARAVRInput.GetDown(ARAVRInput.Button.IndexTrigger))
        {
            // ��Ʈ�ѷ��� ���� ���
            ARAVRInput.PlayVibration(ARAVRInput.Controller.RTouch);

            // �Ѿ� ����� ���
            bulletAudio.Stop();
            bulletAudio.Play();

            // Ray�� ī�޶��� ��ġ�κ��� �������� �����.
            Ray ray = new Ray(ARAVRInput.RHandPosition, ARAVRInput.RHandDirection);
            // Ray�� �浹 ������ �����ϱ� ���� ���� ����
            RaycastHit hitinfo;
            // �÷��̾� ���̾� ������
            int playerLayer = 1 << LayerMask.NameToLayer("Player");
            // Ÿ�� ���̾� ������
            int towerLayer = 1 << LayerMask.NameToLayer("Tower");
            int layermask = playerLayer | towerLayer;
            // Ray�� ���. ray�� �ε��� ������ hitInfo�� ����.
            if (Physics.Raycast(ray, out hitinfo, 200, ~layermask))
            {
                // �Ѿ� ����Ʈ ����ǰ� ������ ���߰� ���
                bulletEffect.Stop();
                bulletEffect.Play();
                // �΋H�� ���� �ٷ� ������ ����Ʈ�� ���̵��� ����
                bulletImpact.position = hitinfo.point;
                // �΋H�� ������ �������� �Ѿ� ����Ʈ�� ������ ����
                bulletImpact.forward = hitinfo.normal;

            }
        }
    }
}
