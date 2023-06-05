using UnityEngine;

public class SnowballThrower : MonoBehaviour
{
    public float minThrowPower = 5f;
    public float maxThrowPower = 20f;
    public float maxHoldTime = 2f;      // �ִ� ������ �ɸ��� �ð�
    public float forwardOffset = 1f;
    public int numPoints = 30;
    public LineRenderer lineRenderer;
    public GameObject snowballPrefab;

    private float currentHoldTime;
    private bool isThrowing;
     
    void Update()
    {
        if (ARAVRInput.GetDown(ARAVRInput.Button.One, ARAVRInput.Controller.RTouch))
        {
            isThrowing = true;
            currentHoldTime = 0f;
            lineRenderer.positionCount = 0;
        }

        else if (ARAVRInput.Get(ARAVRInput.Button.One, ARAVRInput.Controller.RTouch) && isThrowing)
        {
            currentHoldTime = Mathf.Clamp(currentHoldTime + Time.deltaTime, 0f, maxHoldTime); // 0 ~ maxHoldTime ���̰��� ����
            CalculateTrajectory();
        }
        else if (ARAVRInput.GetUp(ARAVRInput.Button.One, ARAVRInput.Controller.RTouch) && isThrowing)
        {
            isThrowing = false;
            ThrowSnowball();
            lineRenderer.positionCount = 0;
        }
    }

    void CalculateTrajectory()
    {
        float normalizedHoldTime = currentHoldTime / maxHoldTime;
        float throwPower = Mathf.Lerp(minThrowPower, maxThrowPower, normalizedHoldTime); // ������ �� 

        Vector3 throwDirection = ARAVRInput.RHandDirection;

        Vector3 initialPosition = ARAVRInput.RHandPosition + throwDirection; // forwardOffset���� �����ؼ� ������ ���� ��ġ ������


        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;

        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, initialPosition);   // ���� ������ �ʱ� ��ġ

        // ���� �׸���
        float timeStep = maxHoldTime / numPoints;
        for (int i = 1; i <= numPoints; i++)
        {
            float t = timeStep * i;
            Vector3 pointPosition = GetPointPosition(initialPosition, throwDirection, throwPower, t);
            lineRenderer.positionCount = i + 1;
            lineRenderer.SetPosition(i, pointPosition);
        }
    }

    Vector3 GetPointPosition(Vector3 initialPosition, Vector3 throwDirection, float throwPower, float time)
    {
        Vector3 velocity = throwDirection * throwPower; // ������ �ӵ�
        Vector3 gravity = Physics.gravity;
        Vector3 pointPosition = initialPosition + velocity * time + 0.5f * gravity * time * time; // �߷� ȿ��
        return pointPosition;
    }

    void ThrowSnowball()
    {
        float normalizedHoldTime = currentHoldTime / maxHoldTime;
        float throwPower = Mathf.Lerp(minThrowPower, maxThrowPower, normalizedHoldTime);

        Vector3 throwDirection = ARAVRInput.RHandDirection;

        Vector3 initialPosition = ARAVRInput.RHandPosition + throwDirection;
        GameObject snowball = Instantiate(snowballPrefab, initialPosition, Quaternion.identity);
        snowball.SetActive(true);

        Rigidbody rb = snowball.GetComponent<Rigidbody>();
        rb.AddForce(throwDirection * throwPower, ForceMode.Impulse);
    }
}
