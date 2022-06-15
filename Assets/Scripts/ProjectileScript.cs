using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    [SerializeField]
    GameObject projectile;

    [SerializeField]
    float DefaultSpawnInterval;

    float spawnInterval;

	void Start()
	{
        spawnInterval = DefaultSpawnInterval;
	}

	// Update is called once per frame
	void Update()
    {
        spawnInterval -= Time.deltaTime;
        if (spawnInterval < 0)
		{
            spawnInterval = DefaultSpawnInterval;
            SpawnProjectile();
		}
    }

    void SpawnProjectile()
	{ 
        GameObject obj = Instantiate(projectile, transform.position, gameObject.transform.rotation);
        //gets parent direction
        obj.GetComponent<ProjectileBehaviousScript>().isFacingLeft = gameObject.transform.parent.gameObject.transform.localScale.x > 0; ;
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
        Debug.Log("Contact");
	}
} 
