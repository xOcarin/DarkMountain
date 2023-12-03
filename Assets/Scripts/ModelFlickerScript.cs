using UnityEngine;

public class ModelFlickerScript : MonoBehaviour
{
    private static Material originalMaterial;
    public static Material pulseMaterial; // Material to swap with
    private static SkinnedMeshRenderer skinnedMeshRenderer;

    void Start()
    {
        skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();

        if (skinnedMeshRenderer == null)
        {
            Debug.LogError("SkinnedMeshRenderer component not found!");
            enabled = false; // Disable the script if SkinnedMeshRenderer is not found
        }

        // Store the original material
        originalMaterial = skinnedMeshRenderer.material;

        // Set pulseMaterial to a new material with fully transparent color
        pulseMaterial = new Material(Shader.Find("Transparent/Diffuse"));
        pulseMaterial.color = new Color(1f, 0f, 0f, 0f); // Fully transparent (red, green, blue, alpha)
    }
    
    

    public static void ChangeMaterial()
    {
        if (pulseMaterial != null)
        {
            skinnedMeshRenderer.material = Instantiate(pulseMaterial); // Instantiate a new material to avoid modifying the original
        }
        else
        {
            Debug.LogError("Pulse material not assigned!");
        }
    }

    public static void ChangeBackToOriginalMaterial()
    {
        Debug.LogError("Switching back to original");
        skinnedMeshRenderer.material = originalMaterial;
    }
}