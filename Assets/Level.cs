using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{

    public GameObject level;

    float rotationScale = .1f;  // degrees per tick

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        level.transform.Rotate(getRotateDirection(), rotationScale);
    }

    Vector3 getRotateDirection()
    {
        float xInput = Input.GetAxisRaw("Horizontal");

        switch (xInput)
        {
            case 1:
                return Vector3.forward;
            case -1:
                return Vector3.back;
            default:
                return new Vector3(0, 0, 0);  // todo... probably not this
        }
    }
}
