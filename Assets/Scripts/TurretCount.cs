using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretCount : MonoBehaviour
{
    [SerializeField]
    public GameObject sprite; //Pour animer le joueur quand il ne peut plus placer de tourelles

    public Sprite[] sprites;
    private Image background;
    private Color backgroundColor;
    public AudioSource turretBlock;
    private Animator anim;
    private float wait = 0.39f;
    private float f_TimedColor = 0 ;
    private bool pong = false;

    // Start is called before the first frame update
    void Start()
    {
        background = GameObject.Find("TurretBackground").GetComponent<Image>();
        backgroundColor = GameObject.Find("TurretBackground").GetComponent<Image>().color;
        anim = sprite.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TurretUpdate(int nbTurret)
    {
        GetComponent<Image>().sprite = sprites[nbTurret];
    }

    public IEnumerator TooMuchTurret()
    {
        turretBlock.Play();
        anim.SetTrigger("NoMoreTurrets");
        
        while (wait > 0)
        {
            if (pong == false)
                f_TimedColor += 0.1f;
            else
            {
                f_TimedColor -= 0.1f;
            }

            if (f_TimedColor >= 1f)
                pong = true;
            else if (f_TimedColor < 0.1f)
                pong = false;

            background.color = Color.Lerp(backgroundColor, Color.red, Mathf.PingPong(f_TimedColor, 1f));
            wait -= 0.01f;
            yield return new WaitForSeconds(0.01f);
            
        }
        wait = 0.39f;
        f_TimedColor = 0;

    yield return null;
        
    }

}
