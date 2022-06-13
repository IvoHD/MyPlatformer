using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    [SerializeField]
    GameObject projectile;

    float spawnInterval = 3f;

    // Update is called once per frame
    void Update()
    {
        spawnInterval -= Time.deltaTime;
        if (spawnInterval < 0)
		{
            spawnInterval = 3f;
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
