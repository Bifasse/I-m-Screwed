using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAnimation : MonoBehaviour
{
    private int duration = 9;
    private int hitlag = 6;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Fais trembler la caméra lors de la destruction d'une tourelle
    public IEnumerator CameraShake()
    {
        var oldPos = transform.position;
        for(int i = 0; i < duration; i++)
        {
            transform.position = Random.insideUnitSphere*2 + oldPos;
            //if(i <= 2)
            //{
            //    transform.Translate(1, 0.2f, 0 * Time.deltaTime);
            //}
            //else if(i<=5 && i > 2)
            //{
            //    transform.Translate(0.2f, 1, 0 * Time.deltaTime);
            //}else if(i<=8 && i>5)
            //{
            //    transform.Translate(-1.2f, -1.2f, 0 * Time.deltaTime);
            //}
            yield return new WaitForSeconds(.01f);
        }
        transform.position = oldPos;
        
    }

    public IEnumerator OnEnemyHit()
    {
        for (int j = 0; j < hitlag; j++)
        {
            if (j <= 2)
            { 
                transform.Translate(0, 0.25f, 0 * Time.deltaTime);
                
            }
            else if (j <= 5 && j > 2)
            {
                transform.Translate(0, -0.25f, 0 * Time.deltaTime);
            }

            yield return new WaitForSeconds(.02f);
        }

    }
}
