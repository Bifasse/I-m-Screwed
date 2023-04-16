using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBound : MonoBehaviour
{
    private float xRange = 38;
    private float zRange = 38;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Détruit les projectiles au bout d'une certaine distance
        if (gameObject.transform.position.x < -xRange)
            Destroy(this.gameObject);
        else if (gameObject.transform.position.x > xRange)
            Destroy(this.gameObject);
        else if (gameObject.transform.position.z < -zRange)
            Destroy(this.gameObject);
        else if (gameObject.transform.position.z > zRange)
            Destroy(this.gameObject);
    }
}
