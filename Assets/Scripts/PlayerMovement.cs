using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //private Vector3[] verticalPos = new Vector3[5];
    //private Vector3[] horizontalPos = new Vector3[5];
    //private Vector3 currentPos;
    private Vector3 oldPos;
    private int posV = 2;
    private int posH = 2;
    public bool canMove = true;
    private float wait = 0;
    private float moveSpeed = 0.15f;
    private float nextMove = 0;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<PlayerRepair>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        //if(Input.GetKeyDown(KeyCode.DownArrow) && posV < 4)
        //{
        //    posV++;
        //    transform.Translate(Vector3.forward * -2.5f);
        //}
        //else if (Input.GetKeyDown(KeyCode.UpArrow) && posV > 0)
        //{
        //    posV--;
        //    transform.Translate(Vector3.forward * 2.5f);
        //}
        //else if (Input.GetKeyDown(KeyCode.LeftArrow) && posH < 4)
        //{
        //    posH++;
        //    transform.Translate(Vector3.right * -2.5f);
        //}
        //else if (Input.GetKeyDown(KeyCode.RightArrow) && posH > 0)
        //{
        //    posH--;
        //    transform.Translate(Vector3.right * 2.5f);
        //}
        if(Time.time > nextMove)
        {
            if (Input.GetKey(KeyCode.DownArrow) && posV < 4)
            {
                posV++;
                /*wait = 0;
                oldPos = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
                while (wait < 1)
                {
                    transform.position = Vector3.Lerp(oldPos, Vector3.forward * -2.5f, wait);
                    wait += 1 / moveSpeed * Time.deltaTime;
                    nextMove = Time.time + moveSpeed;
                    Debug.Log(wait);
                }*/
                transform.Translate(Vector3.forward * -2.5f);
                nextMove = Time.time + moveSpeed;
            }
            else if (Input.GetKey(KeyCode.UpArrow) && posV > 0)
            {
                posV--;
                transform.Translate(Vector3.forward * 2.5f);
                nextMove = Time.time + moveSpeed;
            }
            else if (Input.GetKey(KeyCode.LeftArrow) && posH < 4)
            {
                posH++;
                transform.Translate(Vector3.right * -2.5f);
                nextMove = Time.time + moveSpeed;
            }
            else if (Input.GetKey(KeyCode.RightArrow) && posH > 0)
            {
                posH--;
                transform.Translate(Vector3.right * 2.5f);
                nextMove = Time.time + moveSpeed;
            }
            
        }

    }

    private void OnEnable()
    {
        GetComponent<PlayerRepair>().enabled = false;
    }

}
