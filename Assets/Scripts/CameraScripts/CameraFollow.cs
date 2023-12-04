using System;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    public Vector3 offset;

    public bool useOffsetValues;

    public float sensitivity;

    public Transform pivot;

    public float maxViewAngle;
    public float minViewAngle;
    
    
    private void Start()
    {
        if (useOffsetValues)
        {
            offset = target.position - transform.position;
        }

        pivot.transform.position = target.transform.position;
        //pivot.transform.parent = target.transform;
        pivot.transform.parent = null;
    }

    void LateUpdate()
    {
        
        pivot.transform.position = target.transform.position;

        //get x of mouse to rotate target
        float horizontal = Input.GetAxis("Mouse X") * sensitivity;
        pivot.Rotate(0,horizontal,0);
        
        
        //get the y pos of the mouse & rotate the pivot
        float verticle = Input.GetAxis("Mouse Y") * sensitivity;
        pivot.Rotate(verticle,0,0);
        
        //limit the up/down camera rotation
        if (pivot.rotation.eulerAngles.x > maxViewAngle && pivot.rotation.eulerAngles.x < 180f)
        {
            pivot.rotation = Quaternion.Euler(maxViewAngle, 0, 0);
        }
       
        if (pivot.rotation.eulerAngles.x > 180f && pivot.rotation.eulerAngles.x < 360f + minViewAngle)
        {
            pivot.rotation = Quaternion.Euler(360f + minViewAngle, 0, 0);

        }
        
         
        //move camera based on current rotation of target and og offset
        float desiredYAngle = pivot.eulerAngles.y;
        float desiredXAngle = pivot.eulerAngles.x;

        Quaternion rotation = Quaternion.Euler(desiredXAngle, desiredYAngle, 0);
        transform.position = target.position - (rotation * offset);
        
        //transform.position = target.position - offset;

        if (transform.position.y < target.position.y -.5)
        {
            transform.position = new Vector3(transform.position.x, target.position.y - .5f, transform.position.z);
        }
        
        transform.LookAt(target);
    }
}