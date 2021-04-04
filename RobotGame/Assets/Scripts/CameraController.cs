using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform targetLook;
    [SerializeField]
    private Transform mainCamera;
    [SerializeField]
    private Transform pointCamera;
    [SerializeField]
    private float speedTranslateCamera = 1;
    [SerializeField]
    private float speedRotateCamera = 1; 


    // Start is called before the first frame update
    void Start()
    {
       // mainCamera.transform.position = pointCamera.position;
        mainCamera.transform.LookAt(targetLook);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, pointCamera.transform.position, speedTranslateCamera * Time.fixedDeltaTime);
        mainCamera.transform.LookAt(targetLook);
      
    }



    private void Update()
    {
      
    }
}
