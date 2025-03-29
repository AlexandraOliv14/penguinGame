using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float smooth;
    [SerializeField] private Vector3 minValue, maxValue;

    private void FixedUpdate()
    {
        var playerViewportPos = Camera.main.WorldToViewportPoint(target.position);

        if (playerViewportPos.x >= 0.5f)
        {
            Follow();
        }
    }

    private void Follow()
    {
        Vector3 targetPosition = target.position + offset;

        Vector3 boundPosition = new Vector3(Mathf.Clamp(targetPosition.x,minValue.x, maxValue.x ), Mathf.Clamp(targetPosition.y, minValue.y, maxValue.y), Mathf.Clamp(targetPosition.z, minValue.z, maxValue.z));

        Vector3 smoothPosition = Vector3.Lerp(transform.position, boundPosition, smooth * Time.fixedDeltaTime);
    
        transform.position = smoothPosition;

        minValue.x = smoothPosition.x;

    }

}
