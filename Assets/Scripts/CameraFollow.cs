using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float smoothSpeed = 0.125f;

    private void FixedUpdate()
    {
        Vector3 _desiredPosition = target.position + offset;
        Vector3 _smoothedPosition = Vector3.Lerp(transform.position, _desiredPosition, smoothSpeed);
        transform.position = _smoothedPosition;
    }
}
