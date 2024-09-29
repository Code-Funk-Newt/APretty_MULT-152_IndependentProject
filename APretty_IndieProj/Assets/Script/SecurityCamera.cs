using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SecurityCamera : MonoBehaviour
{
    public float rotationSpeed = 30f;
    public float detectionRange = 10f;
    public Transform player;
    public float detectionInterval = 0.5f; // Interval in seconds
    public bool isActive = true; // Camera active state
    public float fieldOfView = 45f; // Field of view for detection
    public MeshFilter viewMeshFilter; // Reference to the MeshFilter for the field of view

    private float initialRotationY;
    private Mesh viewMesh;

    private void Start()
    {
        initialRotationY = transform.eulerAngles.y;
        viewMesh = new Mesh();
        viewMesh.name = "View Mesh";
        viewMeshFilter.mesh = viewMesh;
        StartCoroutine(DetectPlayer());
    }

    private void Update()
    {
        if (isActive)
        {
            RotateCamera();
            DrawFieldOfView();
        }
    }

    void RotateCamera()
    {
        float angle = Mathf.PingPong(Time.time * rotationSpeed, 90f) - 45f; // 90 degrees total sweep
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, initialRotationY + angle, transform.eulerAngles.z);
    }

    IEnumerator DetectPlayer()
    {
        while (true)
        {
            if (isActive)
            {
                Vector3 directionToPlayer = player.position - transform.position;
                float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);

                if (angleToPlayer < fieldOfView && directionToPlayer.magnitude < detectionRange)
                {
                    Debug.Log("Player Detected!");
                    // Add your detection logic here (e.g., trigger an alarm)
                }
            }
            yield return new WaitForSeconds(detectionInterval);
        }
    }

    void DrawFieldOfView()
    {
        int stepCount = Mathf.RoundToInt(fieldOfView * 2);
        float stepAngleSize = fieldOfView * 2 / stepCount;
        List<Vector3> viewPoints = new List<Vector3>();

        for (int i = 0; i <= stepCount; i++)
        {
            float angle = transform.eulerAngles.y - fieldOfView + stepAngleSize * i;
            ViewCastInfo newViewCast = ViewCast(angle);
            viewPoints.Add(newViewCast.point);
        }

        int vertexCount = viewPoints.Count + 1;
        Vector3[] vertices = new Vector3[vertexCount];
        int[] triangles = new int[(vertexCount - 2) * 3];

        vertices[0] = Vector3.zero;
        for (int i = 0; i < vertexCount - 1; i++)
        {
            vertices[i + 1] = transform.InverseTransformPoint(viewPoints[i]);

            if (i < vertexCount - 2)
            {
                triangles[i * 3] = 0;
                triangles[i * 3 + 1] = i + 1;
                triangles[i * 3 + 2] = i + 2;
            }
        }

        viewMesh.Clear();
        viewMesh.vertices = vertices;
        viewMesh.triangles = triangles;
        viewMesh.RecalculateNormals();
    }

    ViewCastInfo ViewCast(float globalAngle)
    {
        Vector3 direction = DirFromAngle(globalAngle, true);
        RaycastHit hit;

        if (Physics.Raycast(transform.position, direction, out hit, detectionRange))
        {
            return new ViewCastInfo(true, hit.point, hit.distance, globalAngle);
        }
        else
        {
            return new ViewCastInfo(false, transform.position + direction * detectionRange, detectionRange, globalAngle);
        }
    }

    Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        Vector3 leftBoundary = DirFromAngle(-fieldOfView, false) * detectionRange;
        Vector3 rightBoundary = DirFromAngle(fieldOfView, false) * detectionRange;

        Gizmos.DrawLine(transform.position, transform.position + leftBoundary);
        Gizmos.DrawLine(transform.position, transform.position + rightBoundary);
    }

    // Method to deactivate the camera
    public void DeactivateCamera()
    {
        isActive = false;
        Debug.Log("Camera Deactivated");
    }

    // Method to activate the camera
    public void ActivateCamera()
    {
        isActive = true;
        Debug.Log("Camera Activated");
    }

    public struct ViewCastInfo
    {
        public bool hit;
        public Vector3 point;
        public float distance;
        public float angle;

        public ViewCastInfo(bool _hit, Vector3 _point, float _distance, float _angle)
        {
            hit = _hit;
            point = _point;
            distance = _distance;
            angle = _angle;
        }
    }
}