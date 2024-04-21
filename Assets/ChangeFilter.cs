using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.UI;

public class ChangeFilter : MonoBehaviour
{
    public GameObject facePrefab;
    public Material neonMaterial;
    public Material greenMaterial;
    public Material blueMaterial;
    Renderer meshRenderer;
    public ARFaceManager faceManager;

    private void Start()
    {
        meshRenderer = facePrefab.GetComponent<Renderer>();
    }
    public void chooseGreenFilter()
    {
        var materialsCopy = meshRenderer.materials;
        materialsCopy[0] = greenMaterial;
        meshRenderer.materials = materialsCopy;

        faceManager.enabled = false;
        faceManager.enabled = true;
    }
    public void chooseNeonFilter()
    {
        var materialsCopy = meshRenderer.materials;
        materialsCopy[0] = neonMaterial;
        meshRenderer.materials = materialsCopy;

        faceManager.enabled = false;
        faceManager.enabled = true;
    }

    public void chooseBlueFilter()
    {
        var materialsCopy = meshRenderer.materials;
        materialsCopy[0] = blueMaterial;
        meshRenderer.materials = materialsCopy;

        faceManager.enabled = false;
        faceManager.enabled = true;
    }

    public void CaptureImage()
    {
        ScreenCapture.CaptureScreenshot("MyPic.png");
        Debug.Log("Capture");
    }
}
