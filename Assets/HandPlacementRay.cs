using UnityEngine;

public class HandPlacementRay : MonoBehaviour
{
    [Header("Ray Settings")]
    public float rayLength = 10f;
    public int segmentCount = 20;
    public float curveStrength = 0.15f;
    public LayerMask placementLayer;

    [Header("Placement")]
    public GameObject previewCube;
    public GameObject cubePrefab;

    private LineRenderer line;
    private bool hasValidHit;
    private Vector3 hitPoint;

    void Awake()
    {
        line = GetComponent<LineRenderer>();
        line.positionCount = segmentCount;
    }

    void Update()
    {
        DrawCurvedRay();
        HandlePlacement();
    }

    void DrawCurvedRay()
    {
        Vector3 start = transform.position;
        Vector3 direction = transform.forward;

        hasValidHit = false;

        for (int i = 0; i < segmentCount; i++)
        {
            float t = i / (float)(segmentCount - 1);
            Vector3 point =
                start +
                direction * rayLength * t +
                Vector3.down * curveStrength * t * t * rayLength;

            line.SetPosition(i, point);

            if (i > 0)
            {
                Vector3 prev = line.GetPosition(i - 1);
                if (Physics.Raycast(prev, point - prev, out RaycastHit hit,
                    Vector3.Distance(prev, point), placementLayer))
                {
                    hasValidHit = true;
                    hitPoint = hit.point;
                    previewCube.SetActive(true);
                    previewCube.transform.position = hitPoint;
                    break;
                }
            }
        }

        if (!hasValidHit)
            previewCube.SetActive(false);
    }

    void HandlePlacement()
    {
        if (!hasValidHit)
            return;

        // Thumb tap OR pinch (Meta input)
        if (OVRInput.GetDown(OVRInput.Button.One))
        {
            Instantiate(cubePrefab, hitPoint, Quaternion.identity);
        }
    }
}
