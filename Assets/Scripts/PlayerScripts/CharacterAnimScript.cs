using UnityEngine;

public class CharacterAnimScript : MonoBehaviour
{
    

    public float rotationSpeed = 5f;

    void Update()
    {
        // Rotate the object around its up axis
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}