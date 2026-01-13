using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using Lean.Pool;
using Lean.Common;

public class PointSpawner : MonoBehaviour
{
    [SerializeField] private GameObject SqArea;
    [SerializeField] private Transform BasePosition;
    [SerializeField] public float HorizontalPos, VerticalPos;
    [SerializeField] public int HorizontalIndex, VerticalIndex;

    private void Start()
    {
        PoolStart();
    }
    public void PoolStart()
    {
        for (int i = 0; i < 12; i++)
        { 
             var PointsArea = LeanPool.Spawn(SqArea, transform, true);
            PointsArea.GetComponent<PointsArea>().spawner = this;
        }
    }
}
