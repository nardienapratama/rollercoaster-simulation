// Sources: https://docs.unity3d.com/Manual/MultipleCameras.html
using UnityEngine;

public class ChangeCam : MonoBehaviour
{
    public Camera firstPersonCamera;
    public Camera overheadCamera;

    private void Start()
    {
        firstPersonCamera.enabled = true;
        overheadCamera.enabled = false;
    }

    private void Update()
    {
        ChooseCamView();
    }

    private void ChooseCamView()
    {
        if (Input.GetKeyDown("c"))
        {
            if (firstPersonCamera.enabled == true)
            {
                firstPersonCamera.enabled = false;
                overheadCamera.enabled = true;
            }

            else
            {
                overheadCamera.enabled = false;
                firstPersonCamera.enabled = true;
            }
        }
    }
}