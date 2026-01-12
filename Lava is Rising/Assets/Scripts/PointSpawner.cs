using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using Lean.Pool;
using Lean.Common;

public class PointSpawner : MonoBehaviour
{
    [SerializeField] private GameObject SqArea;
    [SerializeField] private Transform BasePosition;
    [SerializeField] public float PositionMult;
    [SerializeField] public int SpawnIndex;


    private void Start()
    {
        PoolStart();
    }
    public void PoolStart()
    {
        for (int i = 0; i < 10; i++)
        {
            var PointsArea = LeanPool.Spawn(SqArea, transform);
        }
    }
}
