using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class GWorld //sealed helps us so we don't get any conflict (one access at a time)
{
    private static readonly GWorld instance = new GWorld();
    private static WorldStates world; //singleton

    static GWorld()
    {
        world = new WorldStates();
    }

    private GWorld()
    {
    }

    public static GWorld Instance
    {
        get { return instance; }
    }

    public WorldStates GetWorld()
    {
        return world;
    }
}