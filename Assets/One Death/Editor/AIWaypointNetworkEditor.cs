using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.AI;

[CustomEditor(typeof(AIWaypointNetwork))]
public class AIWaypointNetworkEditor : Editor
{
    public override void OnInspectorGUI()
    {

        AIWaypointNetwork network = (AIWaypointNetwork)target;

        //Enumerar opeciones en el inpector
        network.DisplayMode = (PathDisplayMode)EditorGUILayout.EnumPopup("Display mode:", network.DisplayMode);

        //Asignar Waypoint Inicial y Final
        if (network.DisplayMode == PathDisplayMode.Paths)
        {
            network.WInitial = EditorGUILayout.IntSlider("Waypoint Start ", network.WInitial, 0, network._waypoints.Count - 1);
            network.WEnd     = EditorGUILayout.IntSlider("Waypoint End ", network.WEnd, 0, network._waypoints.Count - 1);
        }

        DrawDefaultInspector();
    }

    //Render en la vista de escena
    void OnSceneGUI()
    {
        //Obtener referencias de los object con el script waypoint
        AIWaypointNetwork network = (AIWaypointNetwork)target;

        //NOMBRE DEL WAYPOINT
        NameWaypoint(network);


        //Colorar Conections and Path lines waypoints
        NavegationWaypointColor(network);

       
    }


    private void NameWaypoint(AIWaypointNetwork network)
    {
        //cambiar color letras
        GUIStyle style = new GUIStyle();
        style.normal.textColor = Color.white;

        //iterar waypoint y asignacion del nombre
        for (int i = 0; i < network._waypoints.Count; i++)
        {
            if (network._waypoints[i] != null)
            {
                Handles.Label(network._waypoints[i].position, "Waypoint " + i.ToString(), style);
            
            }
        }
    }

    private void NavegationWaypointColor(AIWaypointNetwork network) {

        if (network.DisplayMode == PathDisplayMode.Connections)
        {

            //Inicializar un vector vacio con la cantidad de waypoint
            Vector3[] linePoints = new Vector3[network._waypoints.Count];

            for (int i = 0; i < network._waypoints.Count; i++)
            {
                // index va a tomar el valor de la posicion distinta a la cantidad de waypoint o 0.
                int index = i != network._waypoints.Count ? i : 0;

                if (network._waypoints[index] != null)
                {
                    linePoints[i] = network._waypoints[index].position;
                    //Debug.Log("EN FOR" + linePoints[i]);
                }
                else
                {
                    linePoints[i] = new Vector3(Mathf.Infinity, Mathf.Infinity, Mathf.Infinity);
                    Debug.Log("EN FOR" + linePoints[i]);
                }
            }

            //Cambiar color a la linea
            Handles.color = Color.cyan;
            //Metodo magico para dibujar los vectores de los waypoint
            Handles.DrawPolyLine(linePoints);
        }
        else if (network.DisplayMode == PathDisplayMode.Paths)
        {
            NavMeshPath path = new NavMeshPath();
            Vector3 from = network._waypoints[network.WInitial].position;
            Vector3 to = network._waypoints[network.WEnd].position;

            NavMesh.CalculatePath(from, to, NavMesh.AllAreas, path);

            Handles.color = Color.yellow;
            Handles.DrawPolyLine(path.corners);
        }
    }
}
