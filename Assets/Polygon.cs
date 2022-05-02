using System.Linq;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

public class Polygon : MonoBehaviour
{
    [SerializeField] private Polygon _other;
    [SerializeField] private Vector2[] _localSpacePoints;

    public Vector3[] GetWorldSpacePoints()
    {
        return _localSpacePoints.Select(p => transform.TransformPoint(p)).ToArray();
    }

    public Vector3[] GetWorldSpaceNormals()
    {
        var worldSpacePoints = GetWorldSpacePoints();

        Vector3 GetNormalByIndex(int index)
        {
            var a = worldSpacePoints[index];
            var b = worldSpacePoints[(index + 1) % worldSpacePoints.Length];
            var tangent = b - a;
            return new Vector3(-tangent.y, tangent.x).normalized; // (-Y, X) rotates a vector 90degrees anti clockwise
        }

        return GetWorldSpacePoints().Select((_, index) => GetNormalByIndex(index)).ToArray();
    }


    private void OnDrawGizmos()
    {
        using (new HandlesColorScope(SAT.IsOverlapping(this, _other) ? Color.red : Color.black))
        {
            var worldSpacePoints = GetWorldSpacePoints();
            var worldSpaceNormals = GetWorldSpaceNormals();
            DrawEdges(worldSpacePoints);
            DrawNormals(worldSpacePoints, worldSpaceNormals);
        }
    }

    private static void DrawEdges(IReadOnlyList<Vector3> points)
    {
        for (int i = 0; i < points.Count; i++)
        {
            var a = points[i];
            var b = points[(i + 1) % points.Count];
            Handles.DrawAAPolyLine(3, a, b);
        }
    }

    private static void DrawNormals(IReadOnlyList<Vector3> points, IReadOnlyList<Vector3> normals)
    {
        for (int i = 0; i < points.Count; i++)
        {
            var a = points[i];
            var b = points[(i + 1) % points.Count];
            var center = (a + b) / 2f;
            Handles.DrawAAPolyLine(3, center, center + normals[i] * 0.1f);
        }
    }
}