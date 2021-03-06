﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Enumerar opciones en el inspector 
public enum PathDisplayMode { None, Connections, Paths }

public class AIWaypointNetwork : MonoBehaviour
{
    //llamar a ese display en el script
    [HideInInspector] public PathDisplayMode DisplayMode = PathDisplayMode.Connections;

    //Waypoint Inicial y final 
    [HideInInspector] public int WInitial = 0; 
    [HideInInspector] public int WEnd = 0; 

    //Lista con la transformada de los waypoints
    public List<Transform> _waypoints = new List<Transform>();
}
