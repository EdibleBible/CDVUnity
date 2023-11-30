using UnityEngine;
public class PlayerTriggerSphere : MonoBehaviour
{
    [SerializeField]
    private float detectionRadius = 10f; // Adjust the detection radius as needed

    [SerializeField]
    private int numRays = 36; // Adjust the number of rays as needed

    [SerializeField]
    private float[] verticalAngles; // Adjust the vertical angles for different vertical coverage

    public GameObject teleportMarkerPrefab; // Reference to the teleport marker prefab
    private GameObject teleportMarker; // Instance of the teleport marker object

    public float teleportMargin = 1.0f; // Adjust the margin to control when the marker appears/disappears

    private Transform closestGround;
    public LayerMask layerMask;

    private void Start()
    {
        if (verticalAngles == null || verticalAngles.Length == 0)
        {
            // Default vertical angles if not set
            verticalAngles = new float[] { 45f };
        }

        // Instantiate the teleport marker
        if (teleportMarkerPrefab != null)
        {
            teleportMarker = Instantiate(teleportMarkerPrefab, transform.position, Quaternion.identity);
            teleportMarker.SetActive(false); // Initially, set it inactive
        }
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
            collider.

        }

            // Cast a sphere around the player
            foreach (float verticalAngle in verticalAngles)
        {
            for (int i = 0; i < numRays; i++)
            {
                float horizontalAngle = i * (360f / numRays);
                Vector3 direction = Quaternion.Euler(verticalAngle, horizontalAngle, 0) * transform.forward;

                // Use Physics.SphereCast instead of Physics.Raycast
                if (Physics.SphereCast(transform.position, detectionRadius, direction, out RaycastHit hit, detectionRadius))
                {
                    // Check if the hit object has the "Ground" tag
                    if (hit.collider.CompareTag("Ground"))
                    {
                        // Check if the distance to the ground is closer than the current closest distance
                        if (hit.distance < closestDistance)
                        {
                            closestDistance = hit.distance;
                            newClosestGround = hit.transform;
                            teleportPosition = hit.point; // Set teleport position to the hit point
                        }
                    }
                }
            }
        }

        // Update the closest ground
        closestGround = newClosestGround;

        // Handle the detection of the closest ground (you can print the ground name for now)
        if (closestGround != null)
        {
            Debug.Log("Closest ground: " + closestGround.gameObject.name);

            // Calculate the distance to the closest ground
            float distanceToGround = Vector3.Distance(transform.position, closestGround.position);

            // Check if the distance is larger than the margin
            if (distanceToGround > teleportMargin)
            {
                // Player is not on the ground, activate the teleport marker
                if (teleportMarker != null)
                {
                    teleportMarker.SetActive(true);

                    // Teleport the marker to the hit point
                    teleportMarker.transform.position = teleportPosition;
                }
            }
            else
            {
                // Player is on the ground, deactivate the teleport marker
                if (teleportMarker != null)
                {
                    teleportMarker.SetActive(false);
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Draw a wireframe sphere in the Scene view for visualization purposes
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
/*
public class PlayerTriggerSphere : MonoBehaviour
{
    [SerializeField]
    private float detectionRadius = 10f; // Adjust the detection radius as needed

    public GameObject teleportMarker; // Reference to the teleport marker prefab

    public float teleportMargin = 1.0f; // Adjust the margin to control when the marker appears/disappears

    private Transform closestGround;

    private void Start()
    {
        // Instantiate the teleport marker
        //if (teleportMarkerPrefab != null)
        //{
        //    teleportMarker = Instantiate(teleportMarkerPrefab, transform.position, Quaternion.identity);
        //    teleportMarker.SetActive(false); // Initially, set it inactive
        //}
    }

    private void Update()
    {
        CheckClosestGround();
    }

    void CheckClosestGround()
    {
        float closestDistance = float.MaxValue;
        Transform newClosestGround = null;
        Vector3 teleportPosition = Vector3.zero;
        Vector3 direction = transform.forward;

        if (Physics.SphereCast(transform.position, detectionRadius, direction, out RaycastHit hit, detectionRadius))
        {
            // Check if the hit object has the "Ground" tag
            if (hit.collider.CompareTag("Ground"))
            {
                // Check if the distance to the ground is closer than the current closest distance
                if (hit.distance < closestDistance)
                {
                    closestDistance = hit.distance;
                    newClosestGround = hit.transform;
                    teleportPosition = hit.point; // Set teleport position to the hit point
                }
            }
        }

        // Update the closest ground
        closestGround = newClosestGround;

        // Handle the detection of the closest ground
        if (closestGround != null)
        {
            Debug.Log("Closest ground: " + closestGround.gameObject.name);

            // Calculate the distance to the closest ground
            float distanceToGround = Vector3.Distance(transform.position, closestGround.position);

            // Check if the distance is larger than the margin
            if (distanceToGround > teleportMargin)
            {
                // Player is not on the ground, activate the teleport marker
                {
                    teleportMarker.SetActive(true);

                    // Teleport the marker to the hit point
                    teleportMarker.transform.position = teleportPosition;
                }
            }
            else
            {
                // Player is on the ground, deactivate the teleport marker
                {
                    teleportMarker.SetActive(false);
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Draw a wireframe sphere in the Scene view for visualization purposes
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
*/