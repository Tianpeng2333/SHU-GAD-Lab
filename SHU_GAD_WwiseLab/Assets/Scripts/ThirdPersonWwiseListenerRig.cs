using UnityEngine;

public class ThirdPersonWwiseListenerRig : MonoBehaviour
{
    [SerializeField] private Transform positionTarget;
    [SerializeField] private Transform rotationTarget;
    [SerializeField] private Vector3 localPositionOffset = new Vector3(0f, 1.6f, 0f);

    private Rigidbody listenerRigidbody;
    private Collider listenerCollider;

    private void Awake()
    {
        listenerCollider = GetComponent<Collider>();
        if (listenerCollider != null)
        {
            listenerCollider.isTrigger = true;
        }

        listenerRigidbody = GetComponent<Rigidbody>();
        if (listenerRigidbody == null)
        {
            listenerRigidbody = gameObject.AddComponent<Rigidbody>();
        }

        listenerRigidbody.isKinematic = true;
        listenerRigidbody.useGravity = false;
        listenerRigidbody.detectCollisions = true;
    }

    private void LateUpdate()
    {
        var changed = false;

        if (positionTarget != null)
        {
            var targetPosition = positionTarget.TransformPoint(localPositionOffset);
            if (transform.position != targetPosition)
            {
                if (listenerRigidbody != null)
                {
                    listenerRigidbody.position = targetPosition;
                }
                else
                {
                    transform.position = targetPosition;
                }

                changed = true;
            }
        }

        if (rotationTarget != null)
        {
            var targetRotation = rotationTarget.rotation;
            if (transform.rotation != targetRotation)
            {
                if (listenerRigidbody != null)
                {
                    listenerRigidbody.rotation = targetRotation;
                }
                else
                {
                    transform.rotation = targetRotation;
                }

                changed = true;
            }
        }

        if (changed && listenerCollider != null)
        {
            Physics.SyncTransforms();
        }
    }
}
