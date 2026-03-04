using UnityEngine;

[System.Serializable]
public class ParallaxLayer
{
    [SerializeField] private Transform backGroundTransform;
    [SerializeField] private float parallaxEffectMultiplier;

    private float imageFullWidth;
    private float imageHalfWidth;
    private float imageWidthOffset = 10;

    public void Move(float distanceToMove)
    {
        backGroundTransform.position += distanceToMove * parallaxEffectMultiplier * Vector3.right;//Move background according command X
    }
    public void CaculateImageWidth()
    {
        imageFullWidth = backGroundTransform.GetComponent<SpriteRenderer>().bounds.size.x;
        imageHalfWidth = imageFullWidth / 2f;
    }
    public void LoopBackGround(float leftCameraEdge, float rightCameraEdge)
    {
        float leftImageEdge = backGroundTransform.position.x - imageHalfWidth + imageWidthOffset;
        float rightImageEdge = backGroundTransform.position.x + imageHalfWidth - imageWidthOffset;
        if(leftImageEdge > rightCameraEdge)
        {
            backGroundTransform.position -= Vector3.right * imageFullWidth;
        }
        else if (rightImageEdge < leftCameraEdge)
        {
            backGroundTransform.position += Vector3.right * imageFullWidth;
        }
    }
}
