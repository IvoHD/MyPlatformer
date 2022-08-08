using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerScript : MonoBehaviour
{
    [SerializeField]
    GameObject projectile;
    [SerializeField]
    float DefaultSpawnInterval;
    [SerializeField]
    float startDelay;
    [SerializeField]
    float bulletSpeed;

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
        GameObject obj = Instantiate(projectile, transform.position, Quaternion.identity);
        ProjectileBehaviousScript projectileBehaviousScript = obj.GetComponent<ProjectileBehaviousScript>();

        //sets bullet variables
        projectileBehaviousScript.SetIsFacingLeft(gameObject.transform.parent.gameObject.transform.localScale.x > 0);
        if(bulletSpeed > 0)
            projectileBehaviousScript.SetBulletSpeed(bulletSpeed);
	}
} 
