using UnityEngine;

public class ParallaxBackGround : MonoBehaviour
{
    [SerializeField] private ParallaxLayer[] backGroundLayers;

    private Camera mainCamera;
    private float lastCameraXPosition;
    private float cameraHalfWidth;

    private void Awake()
    {
        mainCamera = Camera.main;
        cameraHalfWidth = mainCamera.orthographicSize * mainCamera.aspect;
        CaculateBackGroundImageWidth();
    }

    private void FixedUpdate()
    {
        float currentCameraXPosition = mainCamera.transform.position.x;
        float distanceToMove = currentCameraXPosition - lastCameraXPosition;
        lastCameraXPosition = currentCameraXPosition;

        float leftCameraEdge = currentCameraXPosition - cameraHalfWidth;
        float rightCameraEdge = currentCameraXPosition + cameraHalfWidth;

        foreach (ParallaxLayer layer in backGroundLayers)
        {
            layer.Move(distanceToMove);
            layer.LoopBackGround(leftCameraEdge, rightCameraEdge); 
        }
    }

    private void CaculateBackGroundImageWidth()
    {
        foreach (ParallaxLayer layer in backGroundLayers)
        {
            layer.CaculateImageWidth();
        }
    }
}

