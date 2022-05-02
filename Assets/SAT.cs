using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class SAT
{
    public static bool IsOverlapping(Polygon a, Polygon b)
    {
        var normals = Array.Empty<Vector3>()
            .Concat(a.GetWorldSpaceNormals())
            .Concat(b.GetWorldSpaceNormals());

        var aWorldPoints = a.GetWorldSpacePoints();
        var bWorldPoints = b.GetWorldSpacePoints();
        
        return normals.All(normal => !CanSeparateProjections(aWorldPoints, bWorldPoints, normal));
    }

    private static bool CanSeparateProjections(IEnumerable<Vector3> a, IEnumerable<Vector3> b, Vector3 normal)
    {
        var aProjections = a.Select(point => Vector3.Dot(point, normal)).ToArray();
        var bProjections = b.Select(point => Vector3.Dot(point, normal)).ToArray();

        var aMin = aProjections.MinBy(x => x);
        var aMax = aProjections.MaxBy(x => x);
            
        var bMin = bProjections.MinBy(x => x);
        var bMax = bProjections.MaxBy(x => x);

        return AreProjectionsSeparated(aMin, aMax, bMin, bMax);
    }

    private static bool AreProjectionsSeparated(float aMin, float aMax, float bMin, float bMax)
    {
        return aMin > bMax || bMin > aMax;
    }
}