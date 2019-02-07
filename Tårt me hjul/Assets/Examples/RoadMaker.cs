using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RoadCreator))]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class RoadMaker : MonoBehaviour {

    [Range(0.05f, 1.5f)]
    public float spacing = 1f;
    public float roadWidth = 3.5f;
    public float tiling = 4;
    public Material material;
    public bool useTimer;

    GameObject target;
    Path unevenPath;
    List<GameObject> dots = new List<GameObject>();

    bool recording = false;
    float pointDstThreshold = 1f;
    float closedPathThreshold = 2f;
    float timeInterval = 0.25f;
    float timeTillNextPoint = 0.25f;

    void Update()
    {

        if(Input.GetKeyDown(KeyCode.P) && !recording) StartRecordingPath(); // Start recording path
        else if(Input.GetKeyDown(KeyCode.P) && recording) StopRecordingPath(false); // Stop recording path

        if (recording)
        {
            if (timeTillNextPoint <= 0 && useTimer) // Timed check
            {
                timeTillNextPoint = timeInterval;
                AddSegmentAndDot(transform.position);
            }
            timeTillNextPoint -= Time.deltaTime;

            float dstToLastPoint = Vector2.SqrMagnitude(unevenPath[unevenPath.NumPoints - 1] - (Vector2)transform.position);
            if (dstToLastPoint > Mathf.Pow(pointDstThreshold, 2) && !useTimer) // Distance dependent check
            {
                AddSegmentAndDot(transform.position);
            }

            Vector2 dirToFirstPoint = unevenPath[0] - (Vector2)transform.position;
            float sqrDstToFirstPoint = Vector2.SqrMagnitude(dirToFirstPoint); // Check distance to first point
            float angleThreshold = 30;
            bool  facingTowardsEndpoint = Vector2.Angle(transform.up, dirToFirstPoint) < angleThreshold;
            if (sqrDstToFirstPoint < Mathf.Pow(closedPathThreshold, 2) && facingTowardsEndpoint && recording) StopRecordingPath(true);
        }
    }

    void AddDot(Vector2 pos)
    {
        GameObject dot = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        dot.transform.position = pos;
        dots.Add(dot);
    }

    void AddSegmentAndDot(Vector2 pos)
    {
        unevenPath.AddSegment(pos);
        AddDot(pos);
    }

    void ClearDots()
    {
        for (int i = 0; i < dots.Count; i++)
        {
            Destroy(dots[i]);
        }
        dots.Clear();
    }

    void StartRecordingPath()
    {
        recording = true;

        Vector2 anchor1 = transform.position - transform.up * pointDstThreshold;
        Vector2 anchor2 = transform.position;
        unevenPath = new Path(transform.position);
        unevenPath.AutoSetControlPoints = true;
        unevenPath.MovePoint(0, anchor1);
        AddDot(anchor1);
        unevenPath.MovePoint(3, anchor2);
        AddDot(anchor2);
    }

    void StopRecordingPath(bool isClosed)
    {
        AddSegmentAndDot(transform.position);
        ClearDots();

        // Draw unevenPath
        unevenPath.IsClosed = isClosed;
        CreateRoadObject(isClosed);

        recording = false;
    }

    void CreateRoadObject(bool isClosed)
    {
        // Get even points
        Vector2[] evenPoints = unevenPath.CalculateEvenlySpacedPoints(spacing);

        // Create Road object
        target = new GameObject("RoadObject");

        MeshFilter mFilter = target.AddComponent<MeshFilter>();
        MeshRenderer mRenderer = target.AddComponent<MeshRenderer>();

        // Draw Path
        Mesh mesh = RoadCreator.CreateRoadMesh(evenPoints, isClosed, roadWidth);
        mFilter.mesh = mesh;
        int textureRepeat = Mathf.RoundToInt(tiling * evenPoints.Length * spacing * 0.05f);
        mRenderer.material = material;
        mRenderer.material.mainTextureScale = new Vector2(1, textureRepeat);
    }
}
