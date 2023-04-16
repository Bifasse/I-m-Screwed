using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRepair : MonoBehaviour
{
    public GameManager gameManager;
    public CanInteract canInteract;
    public GameObject[] target;
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void OnEnable()
    {
        target = GameObject.FindGameObjectsWithTag("onRepair");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && gameManager.instantRepair != 0)
        {
            gameManager.UseCharge();
            GetComponent<PlayerMovement>().enabled = true;
            target[0].GetComponent<CanInteract>().RepairFinish();
        }
    }


}
