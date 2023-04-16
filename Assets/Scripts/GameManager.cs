using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    private SpawnManager spawnManager;
    public TurretManager TurretManager;
    public Sprite EmptyCharge;
    public Sprite FullCharge;
    public Image FillCharge;
    public Image FillSlot;
    public Image[] chargeBar;
    public Image turretCount;
    public Canvas uiCanvas;

    public AudioSource onHit;
    private Camera c_Camera;
    public GameObject mainAudio;
    private bool isHere = false;
    [SerializeField]
    private Canvas gameOver;

    [SerializeField]
    private GameObject faster;
    [SerializeField]
    private Image fasterImg;
    [SerializeField]
    private Text t_Score;
    [SerializeField]
    private Image maxTurret;
    [SerializeField]
    private Sprite notMax;
    [SerializeField]
    private Sprite max;

    public int score = 0;
    public int instantRepair = 1;
    public float instantGauge = 0f;
    private bool isMaxed = false;
    public float difficulty = 0;
    private int difRamp = 0;
    private float fastWait = 0f;
    private bool isGameOver = false;

    public int activatedTurret = 0;
    // Start is called before the first frame update
    void Start()
    {
        spawnManager = GetComponent<SpawnManager>();
        faster.SetActive(false);
        mainAudio = GameObject.Find("MainTheme");
        uiCanvas = GameObject.Find("CanvasUi").GetComponent<Canvas>();
        TurretManager = GameObject.Find("TurretManager").GetComponent<TurretManager>();
        
        chargeBar[0].sprite = FullCharge;
        chargeBar[1].sprite = EmptyCharge;
        chargeBar[2].sprite = EmptyCharge;

        FillCharge.fillAmount = instantGauge;
        t_Score.text = "SCORE : " + score;
        turretCount.GetComponent<TurretCount>().TurretUpdate(activatedTurret);
        c_Camera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i<3; i++)
        {
            if(i < instantRepair)
            {
                chargeBar[i].sprite = FullCharge;
            }
            else
            {
                chargeBar[i].sprite = EmptyCharge;
            }
        }
    }

    //change le comtpeurs de tourelles lors d'un toggle on
    public void AddTurret()
    {
        activatedTurret++;
        if (activatedTurret == 3)
            maxTurret.sprite = max;
        turretCount.GetComponent<TurretCount>().TurretUpdate(activatedTurret);
    }

    //change le comtpeurs de tourelles lors d'un toggle off
    public void RemoveTurret()
    {
        if(activatedTurret == 3)
            maxTurret.sprite = notMax;
        activatedTurret--;
        turretCount.GetComponent<TurretCount>().TurretUpdate(activatedTurret);
    }

    //fini la partie dès qu'un ennemi atteint la zone sécurisée
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            GameOver();
            Destroy(player.gameObject);
            GetComponent<BoxCollider>().enabled = false;
        }
        
    }

    //Ajoute des points au score et gère l'augmentation de la jauge de réparation
    public void AddScore()
    {
        score++;
        difRamp++;
        if(difRamp == 30 && !isGameOver)
        {
            DifficultyUp();

        }

        StartCoroutine(c_Camera.GetComponent<CameraAnimation>().OnEnemyHit());
        onHit.Play();
        t_Score.text = "SCORE : " + score;

        if (instantGauge < 1f && !isMaxed)
        {
            instantGauge += 0.1f;

        }
        else
        {
            if(instantRepair < 3)
            {
                instantRepair++;
                instantGauge = 0;
                if (instantRepair == 3)
                {
                    FillCharge.enabled = false;
                    FillSlot.enabled = false;
                    isMaxed = true;
                }
                    
            }
        }

        FillCharge.fillAmount = instantGauge;
    }

    public void UseCharge()
    {
        instantRepair--;
        isMaxed = false;
        FillCharge.enabled = true;
        FillSlot.enabled = true;
    }

    //Augmente la difficulté d'un cran
    public void DifficultyUp()
    {
        StartCoroutine(Faster());
        float oldDifficulty = difficulty;
        difficulty += 1f;
        difRamp = 0;
        TurretManager.UpgradeAll();
        spawnManager.SpawnUp();
    }

    public void ChargeGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void GameOver()
    {
        isGameOver = true;
        mainAudio.SetActive(false);
        Instantiate(gameOver);
    }

    private IEnumerator Faster()
    {
        fastWait = 0f;
        isHere = false;
        mainAudio.SetActive(false);
        faster.SetActive(true);
        Vector3 imgPos = fasterImg.rectTransform.anchoredPosition;
        while (faster.activeInHierarchy == true)
        {
            if (!isHere)
            {
                while (fastWait < 1)
                {
                    fasterImg.rectTransform.anchoredPosition = Vector3.Lerp(new Vector3(-800, fasterImg.rectTransform.anchoredPosition.y, 0), imgPos, fastWait);
                    fastWait += Time.deltaTime * (1 + difficulty/10);
                    yield return new WaitForSeconds(0.001f);
                }
                isHere = true;
                fastWait = 0;

                yield return new WaitForSeconds(2.50f * (1 - difficulty/15));
            }

            if (isHere)
            {
                while (fastWait < 1)
                {
                    fasterImg.rectTransform.anchoredPosition = Vector3.Lerp(imgPos, new Vector3(800, fasterImg.rectTransform.anchoredPosition.y, 0), fastWait);
                    fastWait += Time.deltaTime * (1 + difficulty / 10);
                    yield return new WaitForSeconds(0.001f);
                }
            }
            faster.SetActive(false);
            fasterImg.rectTransform.anchoredPosition = imgPos;
            mainAudio.SetActive(true);
            yield return null;
        }
        faster.GetComponent<AudioSource>().pitch = 1 + (difficulty / 10);
        mainAudio.GetComponent<AudioSource>().pitch = 1 + (difficulty / 10);
        
    }
}
