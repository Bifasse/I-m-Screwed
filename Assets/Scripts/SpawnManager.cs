using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemies;
    private Vector3[] spawnPoints;
    private Quaternion[] spawnRotations;
    private float boudary = 40;
    private float spawnRate=2f;

    private int randSpawn;
    private int enemyType;
    // Start is called before the first frame update
    void Start()
    {
        spawnPoints = new Vector3[12];
        spawnRotations = new Quaternion[4];

        spawnPoints[0] = new Vector3(boudary, 0, -2.5f);
        spawnPoints[1] = new Vector3(boudary, 0, 0);
        spawnPoints[2] = new Vector3(boudary, 0, 2.5f);
        spawnPoints[3] = new Vector3(2.5f, 0, boudary);
        spawnPoints[4] = new Vector3(0, 0, boudary);
        spawnPoints[5] = new Vector3(-2.5f, 0, boudary);
        spawnPoints[6] = new Vector3(-boudary, 0, 2.5f);
        spawnPoints[7] = new Vector3(-boudary, 0, 0);
        spawnPoints[8] = new Vector3(-boudary, 0, -2.5f);
        spawnPoints[9] = new Vector3(-2.5f, 0, -boudary);
        spawnPoints[10] = new Vector3(0, 0, -boudary);
        spawnPoints[11] = new Vector3(2.5f, 0, -boudary);

        spawnRotations[0] = Quaternion.Euler(0, 180, 0);
        spawnRotations[1] = Quaternion.Euler(0, 90, 0);
        spawnRotations[2] = Quaternion.Euler(0, 0, 0);
        spawnRotations[3] = Quaternion.Euler(0, 270, 0);

        StartCoroutine(RandInvok());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void RandomSpawn()
    {
        randSpawn = Random.Range(0, 12);
        enemyType = Random.Range(0, 10);

        if(enemyType <= 7)
        {
            if (randSpawn < 3)
                Instantiate(enemies[0], spawnPoints[randSpawn], spawnRotations[0]);
            else if (randSpawn > 2 && randSpawn < 6)
                Instantiate(enemies[0], spawnPoints[randSpawn], spawnRotations[1]);
            else if (randSpawn > 5 && randSpawn < 9)
                Instantiate(enemies[0], spawnPoints[randSpawn], spawnRotations[2]);
            else
                Instantiate(enemies[0], spawnPoints[randSpawn], spawnRotations[3]);
        }
        else
        {
            if (randSpawn < 3)
                Instantiate(enemies[1], spawnPoints[randSpawn], spawnRotations[0]);
            else if (randSpawn > 2 && randSpawn < 6)
                Instantiate(enemies[1], spawnPoints[randSpawn], spawnRotations[1]);
            else if (randSpawn > 5 && randSpawn < 9)
                Instantiate(enemies[1], spawnPoints[randSpawn], spawnRotations[2]);
            else
                Instantiate(enemies[1], spawnPoints[randSpawn], spawnRotations[3]);
        }



        //Instantiate(enemy, spawnPoints[randSpawn], Quaternion.identity);

    }

    private IEnumerator RandInvok()
    {
        int repeat = 1;
        while(repeat == 1)
        {
            float f_rand = Random.Range(1, 4);
            Invoke("RandomSpawn", f_rand);
            yield return new WaitForSeconds(spawnRate);
        }

    }

    public void SpawnUp()
    {
        StopAllCoroutines();
        spawnRate -= 0.1f;
        Debug.Log(spawnRate);
        StartCoroutine(RandInvok());
    }
}
