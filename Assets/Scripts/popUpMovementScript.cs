using UnityEngine;

public class YourScriptName : MonoBehaviour
{
    public GameObject mainCamera;
    
    void Update()
    {
        if (mainCamera != null)
        {
            float cameraYRotation = mainCamera.transform.rotation.eulerAngles.y;
            transform.rotation = Quaternion.Euler(0f, cameraYRotation, 0f);
        }
    }
}

