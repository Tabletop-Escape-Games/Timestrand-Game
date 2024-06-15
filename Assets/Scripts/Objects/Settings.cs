using Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Settings
{
    public static float scrollSpeed;
    public static int pointsPerHit;
    public static int pointsTrigger;
    public static GameMode gameMode = GameMode.Basic;
    public static IScoreStrategy scoreStrategy;
}
