using System.Collections;
using UnityEngine;

public class ModelFlickerScript : MonoBehaviour
{
    private Material originalMaterial;
    public Material pulseMaterial; // Material to swap with
    private SkinnedMeshRenderer skinnedMeshRenderer;
    private bool isFlickering = false;

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

    private void Update()
    {
        if (HealthScript.invulnerable && !isFlickering)
        {
            StartCoroutine(FlickerModel());
        }
    }

    public void ChangeMaterial()
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

    public void ChangeBackToOriginalMaterial()
    {
        skinnedMeshRenderer.material = originalMaterial;
    }

    IEnumerator FlickerModel()
    {
        isFlickering = true;
        float flickerDuration = 3f;
        float timer = 0f;

        while (timer < flickerDuration)
        {
            ChangeMaterial();
            yield return new WaitForSeconds(0.15f);
            ChangeBackToOriginalMaterial();
            yield return new WaitForSeconds(0.15f);

            timer += 0.3f;
        }

        isFlickering = false;
    }
}
