using System;
using UnityEditor;
using UnityEngine;

public class HandlesColorScope : IDisposable
{
    private readonly Color _previousColor;

    public HandlesColorScope(Color color)
    {
        _previousColor = Handles.color;
        Handles.color = color;
    }

    public void Dispose()
    {
        Handles.color = _previousColor;
    }
}