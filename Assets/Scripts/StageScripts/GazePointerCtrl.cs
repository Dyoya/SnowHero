using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class GazePointerCtrl : MonoBehaviour
{
    // Start is called before the first frame update
    public Image gazeImg;

    public float uiScaleVal = 1f;
    public Transform crosshairImage;
    bool isHitObj;
    GameObject prevHitObj;
    GameObject curHitObj;
    float curGazeTime;
    public float gazeChargeTime = 3f;

    void Start()
    {
        curGazeTime = 0;
        prevHitObj = null;
    }

    void Update()
    {
        Vector3 dir = transform.TransformPoint(new Vector3(0,0,1000f));
        Ray ray = new Ray(transform.position, dir);
        Debug.DrawRay(ray.origin, ray.direction * 100f, Color.green);
        RaycastHit hitinfo;
        if (Physics.Raycast(ray.origin, ray.direction, out hitinfo, 1000f))
        {
            crosshairImage.position = hitinfo.point;
            Debug.Log(hitinfo.collider.name);
            if (hitinfo.transform.CompareTag("GazeObj"))
            {
                isHitObj = true;
            }
            else
            {
                isHitObj = false;
                curGazeTime = 0;
            }
            curHitObj = hitinfo.transform.gameObject;
        }

        if (isHitObj)
        {
            if (curHitObj == prevHitObj)
            {
                Debug.Log("Gazing");
                curGazeTime += Time.deltaTime;
                if (curGazeTime >= gazeChargeTime)
                {
                    Button button = curHitObj.GetComponent<Button>();
                    if (button != null && button.interactable)
                    {
                        button.onClick.Invoke();
                    }
                    curGazeTime = 0;
                }
                else
                {
                    prevHitObj = curHitObj;
                }
            }
            else
            {
                curGazeTime = 0;
            }
            prevHitObj = curHitObj;
            curGazeTime = Mathf.Clamp(curGazeTime, 0, gazeChargeTime);

            gazeImg.fillAmount = curGazeTime / gazeChargeTime;

        }
        else
        {
            prevHitObj = null;
            curGazeTime = 0;
            gazeImg.fillAmount = 0;
        }
    }
}