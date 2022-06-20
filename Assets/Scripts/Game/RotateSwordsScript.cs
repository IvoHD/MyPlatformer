using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RotateSwordsScript : MonoBehaviour
{
    Transform lSword;
    Transform rSword;
    Transform player;

    Transform swords; 

    PlayerHealthStateScript playerHealthStateScript;

    bool lSwordDownrSwordUp = true;
    // Start is called before the first frame update
    void Start()
    {
        lSword = GameObject.Find("lSword").transform;
        rSword = GameObject.Find("rSword").transform;
        swords = GameObject.Find("Swords").GetComponent<Transform>();

        GameObject playerObj = GameObject.Find("Jack (Player)");
        player = playerObj.GetComponent<Transform>();
        playerHealthStateScript = playerObj.GetComponent <PlayerHealthStateScript>();

        //if swords can't be found, destroy this component
        if(lSword is null || rSword is null)
            Destroy(this);
    }

    // Update is called once per frame
    void Update()
    {
        //destroys player input component when player died
        if(!playerHealthStateScript.isAlive)
            Destroy(gameObject.GetComponent<PlayerInput>());

        swords.position = player.position;
    }

    /// <summary>
    /// rotates sword
    /// </summary>
    /// <param name="value"></param>
    void OnRotate(InputValue value)
	{
        lSwordDownrSwordUp = !lSwordDownrSwordUp;

        lSword.rotation = Quaternion.Euler(0, 0, lSwordDownrSwordUp ? 180 : 0);
        rSword.rotation = Quaternion.Euler(0, 0, lSwordDownrSwordUp ? 0 : 180);
      
    }
}
