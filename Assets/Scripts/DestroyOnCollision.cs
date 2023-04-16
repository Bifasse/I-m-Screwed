using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnCollision : MonoBehaviour
{
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Au contact avec un ennemi, détruit le projectile, l'ennemi, un point en plus pour l'économie de pack de réparation
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy" || other.gameObject.tag == "FastEnemy")
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
            gameManager.AddScore();
            if(gameManager.instantRepair >= 3)
            {
                gameManager.AddScore();
            }
            
        }
        
    }

}
