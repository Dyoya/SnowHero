using UnityEngine;

public class SnowballThrower : MonoBehaviour
{
    public float minThrowPower = 5f;
    public float maxThrowPower = 20f;
    public float maxHoldTime = 2f;      // 최대 힘까지 걸리는 시간
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
            currentHoldTime = Mathf.Clamp(currentHoldTime + Time.deltaTime, 0f, maxHoldTime); // 0 ~ maxHoldTime 사이값만 존재
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
        float throwPower = Mathf.Lerp(minThrowPower, maxThrowPower, normalizedHoldTime); // 던지는 힘 

        Vector3 throwDirection = ARAVRInput.RHandDirection;

        Vector3 initialPosition = ARAVRInput.RHandPosition + throwDirection; // forwardOffset값을 조정해서 던지는 시작 위치 맞추자


        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;

        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, initialPosition);   // 라인 렌더러 초기 위치

        // 궤적 그리기
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
        Vector3 velocity = throwDirection * throwPower; // 던지는 속도
        Vector3 gravity = Physics.gravity;
        Vector3 pointPosition = initialPosition + velocity * time + 0.5f * gravity * time * time; // 중력 효과
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
