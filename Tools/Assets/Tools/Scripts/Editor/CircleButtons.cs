using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CircleButtons : EditorWindow
{
    static bool Draw = false;
    static bool bypassRepaintAll = true;

    static float Timer;

    static Vector2 MousePos;

    static List<Vector2> UpdatedPos = new List<Vector2>();
    public static int numberOfButtons = 4;
    static Texture2D LogoTex;
    static bool lockPosition;


    [InitializeOnLoadMethod]
    private static void Initialize()
    {
        Config.Init();
        LogoTex = (Texture2D)Resources.Load("Logo") as Texture2D;
        SceneView.duringSceneGui += OnSceneGUI;
        EditorApplication.update += UpdateAnimation;
    }

    private static void UpdateAnimation()
    {
        if (bypassRepaintAll)
        {
            SceneView.RepaintAll();
        }
    }

    private static void OnSceneGUI(SceneView view)
    {
        Event e = Event.current;
        if (e.keyCode == KeyCode.C)
        {
            if (e.type == EventType.KeyDown && !lockPosition)
            {
                Draw = true;
                lockPosition = true;
                MousePos = Event.current.mousePosition;
                InitializeButtons();
            }
            else if (e.type == EventType.KeyUp)
            {
                Draw = false;
                lockPosition = false;
                Timer = 0f;
                UpdatedPos.Clear();
            }
        }

        Handles.BeginGUI();
        if (Draw)
        {
            DrawCircle((int)numberOfButtons, MousePos, 120);
            GUILayout.BeginArea(new Rect(MousePos.x - 15, MousePos.y - 15, 30, 30), LogoTex);
            GUILayout.EndArea();
        }
        Handles.EndGUI();
    }

    public static void DrawCircle(int num, Vector2 point, float radius)
    {
        for (int i = 0; i < num; i++)
        {
            /* Distance around the circle */
            var radians = 2 * 3.17f / num * i;

            /* Get the vector direction */
            var vertical = Mathf.Sin(radians);
            var horizontal = Mathf.Cos(radians);

            var spawnDir = new Vector2(horizontal, vertical);

            /* Get the spawn position */
            var spawnPos = point + spawnDir * radius; // Radius is just the distance away from the point

            spawnPos.x -= 50;
            spawnPos.y -= 10;

            GUILayout.BeginArea(new Rect(UpdatedPos[i].x,
                    UpdatedPos[i].y,
                    100,
                    100));


            Timer = Time.deltaTime;

            Vector2 newUpdatedPos = Vector2.Lerp(UpdatedPos[i], spawnPos, Timer);

            UpdatedPos[i] = newUpdatedPos;

            if (GUILayout.Button(Config.buttonConfigs[i].title,GUILayout.Width(100),GUILayout.Height(20)))
            {
                Config.buttonConfigs[i].action();
            }


            GUILayout.EndArea();
        }
    }

    public static void InitializeButtons()
    {
        for (int i = 0; i < numberOfButtons; i++)
        {
            UpdatedPos.Add(MousePos);
        }
    }

}
