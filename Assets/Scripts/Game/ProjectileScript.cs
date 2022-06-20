using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    [SerializeField]
    GameObject projectile;

    [SerializeField]
    float DefaultSpawnInterval;

    [SerializeField]
    float startDelay;

    float spawnInterval;

	void Start()
	{
        spawnInterval = startDelay;
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
        //sets parent direction
        obj.GetComponent<ProjectileBehaviousScript>().SetIsFacingLeft(gameObject.transform.parent.gameObject.transform.localScale.x > 0);
	}
} 
