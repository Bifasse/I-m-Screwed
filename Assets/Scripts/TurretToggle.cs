using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretToggle : MonoBehaviour
{
    public GameObject Projectile;
    public GameManager gameManager;
    public Material material;
    private Material baseMaterial;
    private float shootRate = 1f;
    private float startTime = 0.5f;
    [SerializeField]
    public Image turretCount;

    public bool isActive = false;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        baseMaterial = GetComponent<Renderer>().material;
        turretCount = GameObject.Find("TurretCount").GetComponent<Image>();
    }

    private void Update()
    {
    }

    public void ToggleTurret()
    {
        if (isActive == false && gameManager.activatedTurret <3)
        { 
            isActive = true;
            InvokeRepeating("Shoot", startTime, shootRate);
            gameManager.AddTurret();
            gameObject.GetComponent<Renderer>().material = material;
        }
        else if(isActive == true)
        {
            isActive = false;
            CancelInvoke();
            gameManager.RemoveTurret();
            gameObject.GetComponent<Renderer>().material = baseMaterial;
        }else if(isActive == false && gameManager.activatedTurret >= 3)
        {
            StartCoroutine(turretCount.GetComponent<TurretCount>().TooMuchTurret());// flash in red or something
        }
    }

    private void Shoot()
    {
        Instantiate(Projectile, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z), gameObject.transform.rotation);
    }

    // Utilise la difficulté pour changer la vitesse des tourelles, redémarre la tourelle si elle est active
    public void Upgrade()
    {
        startTime -= 0.04f;
        shootRate -= 0.05f;
        if(isActive)
        {
            CancelInvoke();
            InvokeRepeating("Shoot", startTime, shootRate);
        }
    }

    //IEnumerator Shoot()
    //{
    //    yield return null;
    //}

    //IEnumerator RepeatShoot()
    //{
    //    while(isActive)
    //    {
    //        StartCoroutine(Shoot());
    //    }
        
    //    yield return null;
    //}

}
