using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretManager : MonoBehaviour
{
    public GameObject[] turrets;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpgradeAll()
    {
        foreach(GameObject turret in turrets)
        {
            turret.GetComponent<TurretToggle>().Upgrade();
        }
    }
}
