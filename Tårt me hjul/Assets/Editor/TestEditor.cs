using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TestCreator))]
public class TestEditor : Editor {

    TestCreator creator;
    Path path;

    private void OnSceneGUI()
    {
        Draw();
    }

    void Draw()
    {
        Handles.color = Color.red;
        for (int i = 0; i < path.NumPoints; i++)
        {
            Vector2 newPos = Handles.FreeMoveHandle(path[i], Quaternion.identity, 0.3f, Vector2.zero, Handles.CylinderHandleCap);
            if(path[i] != newPos)
            {
                Undo.RecordObject(creator, "Move point");
                path.MovePoint(i, newPos);
            }

            if(i < path.NumPoints - 1) Handles.DrawLine(path[i], path[i + 1]);
        }
    }

    private void OnEnable()
    {
        creator = (TestCreator) target;
        if(creator.path == null)
        {
            creator.CreatePath();
        }
        path = creator.path;
    }
}
