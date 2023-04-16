using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanInteract : MonoBehaviour
{
    public GameObject player;
    private TurretToggle turret;
    public Material baseMaterial;
    public Material damagedMaterial;
    public Canvas canvas;
    public Image loadingBar;
    public Animation opening;
    public Animation ending;
    public Camera m_Camera;
    public AudioSource hurtAudio;
    private Animator anim; //Permet de changer le sprite lors d'une réparation

    private bool IsTrigger = false;
    private float maxWait = 2.5f;
    private float wait;
    private bool isDestroyed = false;
    private bool isRepairing = false;
    // Start is called before the first frame update
    void Start()
    {
        m_Camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        player = GameObject.Find("Player");
        baseMaterial = GetComponent<Renderer>().material;
        turret = this.gameObject.GetComponent<TurretToggle>();
        canvas.enabled = false;
        canvas.GetComponent<AnimateUI>().enabled = false;
        anim = GameObject.Find("Player/Sprite").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsTrigger == true && Input.GetKeyDown(KeyCode.Space)) //Si le joueur appuie sur "Espace" à proximmité d'une tourelle
        {
            if(wait <= 0 && isDestroyed == false)
            {
                turret.ToggleTurret();
                wait = 1f;
            }else if (isDestroyed == true)
            {
                if(wait <= 0 && isRepairing == false)
                {
                    OnRepair();
                }                
            }
        }

        if (wait <= 0 && isRepairing == true)
        {
            RepairFinish();
        }
        wait -= Time.deltaTime;
        loadingBar.fillAmount = (maxWait - wait) / maxWait;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            IsTrigger = true;
        }else if ((other.tag == "Enemy" && isDestroyed == false) || (other.tag == "FastEnemy" && isDestroyed == false))
        {
            OnTurretDestroy(other);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            IsTrigger = false;
        }
    }

    public void RepairFinish() // à la fin d'une réparation
    {
        gameObject.tag = "Untagged";
        anim.SetBool("Repairing", false);
        isDestroyed = false;
        isRepairing = false;
        wait = 0;
        player.GetComponent<PlayerMovement>().enabled = true;
        player.GetComponent<PlayerRepair>().enabled = false;
        GetComponent<Renderer>().material = baseMaterial;
        loadingBar.fillAmount = 1f;
        canvas.enabled = false;
        canvas.GetComponent<AnimateUI>().enabled = false;
    }

    public void OnRepair()
    {
        anim.SetBool("Repairing", true);
        gameObject.tag = "onRepair";
        isRepairing = true;
        canvas.enabled = true;
        canvas.GetComponent<AnimateUI>().enabled = true;
        player.GetComponent<PlayerMovement>().enabled = false;
        player.GetComponent<PlayerRepair>().enabled = true;
        wait = maxWait;
        loadingBar.fillAmount = (maxWait - wait) / maxWait;
    }

    public void OnTurretDestroy(Collider other)
    {
        isDestroyed = true;
        hurtAudio.Play();
        if (turret.isActive == true)
            turret.ToggleTurret();
        wait = 0f;
        Destroy(other.gameObject);
        GetComponent<Renderer>().material = damagedMaterial;
        GetComponent<TurretToggle>().enabled = false;
        StartCoroutine(m_Camera.GetComponent<CameraAnimation>().CameraShake());
        //GetComponent<BoxCollider>().enabled = false;
    }
}
