using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;

public class EnemyManager : MonoBehaviour
{
    public GameObject Projectile;
    public Transform[] SpawnPoints;
    public bool allowFire;
    public float ReloadTime;
    public float DistanceReq;
    public List<GameObject> Projs;
    Vector2 Direction;
    private GameObject Player;
    private void Start()
    {
        Player = GameObject.FindWithTag("Player");
        PoolObjects();
        StartCoroutine(Reload());
    }
    void LateUpdate()
    {
        float Distance = Vector2.Distance(transform.position, Player.transform.position);
        if (allowFire == true && Distance < DistanceReq)
        {
            Fire();
        }
    }
    public void Fire()
    {
        for (int i = 0; i < Projs.Count; i++)
        {
            Debug.Log(Projs.Count);
            Projs[i].gameObject.SetActive(true);
            Projs[i].transform.position = SpawnPoints[i].transform.position;
            Projs[i].transform.rotation = SpawnPoints[i].transform.rotation;
        }
        StartCoroutine(Reload());
    }

    IEnumerator Reload()
    {
        allowFire = false;
        yield return new WaitForSeconds(ReloadTime);
        for (int i = 0; i < Projs.Count; i++)
        {
            Projs[i].gameObject.SetActive(false);
        }
        allowFire = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            Destroy(collision.gameObject);
        }
    }
    public void PoolObjects()
    {
        for (int i = 0; i < SpawnPoints.Length; i++)
        {
            GameObject Proj = Instantiate(Projectile, SpawnPoints[i].position, SpawnPoints[i].rotation);
            Projs.Add(Proj);
            Proj.SetActive(false);
        }
    }
}
