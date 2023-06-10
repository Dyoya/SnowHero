using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VRHandController : MonoBehaviour
{
    private AudioSource audioSource;
    public Transform crosshairImage;
    //public LineRenderer myLine;
    // Start is called before the first frame update
    //public AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hitinfo;
        if(Physics.Raycast(ARAVRInput.RHandPosition, ARAVRInput.RHandDirection,out hitinfo, 1000f))
        {
            crosshairImage.position = hitinfo.point;
            //myLine.startWidth = 0.1f;
            //myLine.endWidth = 0.1f;
            //myLine.positionCount = 2;
            //myLine.SetPosition(0, ARAVRInput.RHandPosition);
            //myLine.SetPosition(1, hitinfo.point);
            GameObject curHitObj = hitinfo.transform.gameObject;

            if (ARAVRInput.GetDown(ARAVRInput.Button.One))
            {
                if (hitinfo.transform.CompareTag("GazeObj"))
                {
                    Button button = curHitObj.GetComponent<Button>();
                    if (button != null && button.interactable)
                    {
                        audioSource.Play();
                        button.onClick.Invoke();
                    }
                }
            }
              
        }
        
    }
}
