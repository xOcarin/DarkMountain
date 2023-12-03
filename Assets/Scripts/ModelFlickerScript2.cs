using System.Collections;
using UnityEngine;

public class ModelFlickerScript2 : MonoBehaviour
{
    private Material originalMaterial;
    public Material pulseMaterial; // Material to swap with
    private MeshRenderer meshRenderer;
    private bool isFlickering = false;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();

        if (meshRenderer == null)
        {
            Debug.LogError("MeshRenderer component not found!");
            enabled = false; // Disable the script if MeshRenderer is not found
        }

        // Store the original material
        originalMaterial = meshRenderer.material;

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
            meshRenderer.material = Instantiate(pulseMaterial); // Instantiate a new material to avoid modifying the original
        }
        else
        {
            Debug.LogError("Pulse material not assigned!");
        }
    }

    public void ChangeBackToOriginalMaterial()
    {
        meshRenderer.material = originalMaterial;
    }

    IEnumerator FlickerModel()
    {
        isFlickering = true;
        Debug.Log("FLICKERING!!");
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