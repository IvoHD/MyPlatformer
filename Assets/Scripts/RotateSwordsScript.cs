using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RotateSwordsScript : MonoBehaviour
{
    Transform lSword;
    Transform rSword;
    bool isFacingRight;
    bool lSwordDownrSwordUp = true;
    // Start is called before the first frame update
    void Start()
    {
        lSword = GameObject.Find("lSword").transform;
        rSword = GameObject.Find("rSword").transform;
    }

    // Update is called once per frame
    void Update()
    {
        OnRotate(null);
    }

    void OnRotate(InputValue value)
	{
        if(value is not null)
            lSwordDownrSwordUp = !lSwordDownrSwordUp;

        isFacingRight = gameObject.transform.localScale.x > 0;

        if (isFacingRight)
        {
            lSword.rotation = Quaternion.Euler(0, 0, lSwordDownrSwordUp ? 180 : 0);
            rSword.rotation = Quaternion.Euler(0, 0, lSwordDownrSwordUp ? 0 : 180);
        }
        else
        {
            lSword.rotation = Quaternion.Euler(0, 0, lSwordDownrSwordUp ? 0 : 180);
            rSword.rotation = Quaternion.Euler(0, 0, lSwordDownrSwordUp ? 180 : 0);
        }
    }
}
