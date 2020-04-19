using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionSpawner
{
    public List<Vector3> holes_pos;
    private float margin;

    public PositionSpawner()
    {
        margin = 0.5f;
        holes_pos = new List<Vector3>();
    }

    public Vector3 generateNewHolePos(float minAngle, float maxAngle, float minDist, float maxDist)
    {
        float angle = Random.Range(minAngle, maxAngle);
        if (Random.value > 0.5f)
        {
            angle *= -1;
        }
        float dist = Random.Range(minDist, maxDist);

        Vector3 ranPos = new Vector3(dist * Mathf.Sin(angle * (3.14159f / 180)), 0, dist * Mathf.Cos(angle * (3.14159f / 180)));

        bool fit = true;
        foreach (Vector3 pos in holes_pos)
        {
            if (Vector3.Distance(pos, ranPos) < margin)
            {
                fit = false;
                break;
            }
        }

        if (fit)
        {
            holes_pos.Add(ranPos);
            return (ranPos);
        }
        else
            return generateNewHolePos(minAngle,maxAngle,minDist,maxDist);

    }

}
