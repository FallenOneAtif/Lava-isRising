using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour
{
    public GameObject Projectile;
    public Transform[] SpawnPoints;
    public bool allowFire;
    public float ReloadTime;
    void Update()
    {
        if (allowFire == true)
        {
            Fire();
        }
    }
    public void Fire()
    {
        for (int i = 0; i < SpawnPoints.Length; i++)
        {
            GameObject Proj = Instantiate(Projectile, SpawnPoints[i].position, SpawnPoints[i].rotation);
        }
        StartCoroutine(Reload());
    }

    IEnumerator Reload()
    {
        allowFire = false;
        yield return new WaitForSeconds(ReloadTime);
        allowFire = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            Destroy(collision.gameObject);
        }
    }
}
