using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private float speed = 5f;
    private float boarderX = 40;
    private float boarderZ = 40;
    private float difficulty;
    private float startupTime = 5f;
    public bool isFast;

    // Start is called before the first frame update
    void Start()
    {
        difficulty = GameObject.Find("GameManager").GetComponent<GameManager>().difficulty;
        if (isFast == false)
        {
            speed += difficulty;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Check le type de l'ennemi et défini son comportement
        if (isFast == false)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
        else
        {
            StartCoroutine(MovingFast());
            if(startupTime < 0)
                startupTime = 5f;
        }
            
        //Détruit l'ennemi si il sort des limites de la carte
        if (this.gameObject.transform.position.x > boarderX)
        {
            Destroy(this.gameObject);
        }
        else if (this.gameObject.transform.position.x < -boarderX)
        {
            Destroy(this.gameObject);
        }
        else if (this.gameObject.transform.position.z > boarderZ)
        {
            Destroy(this.gameObject);
        }
        else if (this.gameObject.transform.position.z < -boarderZ)
        {
            Destroy(this.gameObject);
        }
    }

    //Comportement pour les ennemis rapides
    private IEnumerator MovingFast()
    {

        yield return new WaitForSeconds(3);

        transform.Translate(Vector3.right * speed*10 * Time.deltaTime);
        yield return null;

    }
}
