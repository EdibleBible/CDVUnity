using Unity.VisualScripting;
using UnityEngine;
public class PlayerTriggerSphere : MonoBehaviour
{
    [SerializeField]
    private float detectionRadius = 10f; // Adjust the detection radius as needed

    public GameObject teleportMarker; // Reference to the teleport marker prefab

    public float teleportMargin = 1.0f; // Adjust the margin to control when the marker appears/disappears

    public LayerMask layerMask;

    private void Start()
    {
    }

    private void Update()
    {
        CheckClosestGround();
    }

    void CheckClosestGround()
    {
        // Initialize variables for closest distance and closest ground
        float closestDistance = float.MaxValue;
        Transform newClosestGround = null;
        Vector3 teleportPosition = Vector3.zero;

        var colliders = Physics.OverlapSphere(transform.position, detectionRadius, layerMask);
        foreach (var collider in colliders)
        {
            float colliderDistance = Vector3.Distance(collider.transform.position, transform.position);
            if (closestDistance > colliderDistance)
            {
                closestDistance = Vector3.Distance(collider.transform.position, transform.position);
                newClosestGround = collider.gameObject.transform;
            }
        }

        // Update the closest ground
        teleportPosition = newClosestGround.transform.position;
        teleportMarker.transform.position = teleportPosition;
    }

    private void OnDrawGizmosSelected()
    {
        // Draw a wireframe sphere in the Scene view for visualization purposes
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}