using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathObjectParent : MonoBehaviour
{
    public PathPoint[] commonPathPoints;
    public PathPoint[] redPathPoint;
    public PathPoint[] greenPathPoint;
    public PathPoint[] bluePathPoint;
    public PathPoint[] yellowPathPoint;

    public PathPoint[] pawnHomePathPoints;

    [Header("Scale and Position Difference of Pawn")]
    public float[] scales;
    public float[] positionDifference;

}
