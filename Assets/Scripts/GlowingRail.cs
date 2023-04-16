using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowingRail : MonoBehaviour
{
    private Material m_Material;
    private List<GameObject> others = new List<GameObject>();
    private float dangerousTransition = 0f;
    private float colorTransition = 0f;
    private float noMoreTransition = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        m_Material = this.gameObject.GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        if(others.Count != 0)
        {
            for (int i = 0; i< others.Count; i++)
            {
                if(!others[i])
                {
                    others.RemoveAt(i);
                }
            }

            if (others.Count == 0)
            {
                StartCoroutine(NoMoreEnnemies());
            }
        }
    }

    //Vérifie le tag de l'ennemi entré dans le collider, si un ennemi rapide y est déjà alors le rail ne change pas de couleur
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            others.Add(other.gameObject);
            for(int i = 0; i < others.Count; i++)
            {
                if(others[i].tag == "FastEnemy")
                    return;
            }
                StartCoroutine(EmissionTransition());

        }else if (other.tag == "FastEnemy")
        {
            others.Add(other.gameObject);
            StartCoroutine(DangerIncoming());
        }

    }

    private IEnumerator NoMoreEnnemies()
    {
        while (noMoreTransition > 0.02)
        {
            noMoreTransition -= 0.02f;
            m_Material.SetColor("_EmissionColor", Color.magenta * colorTransition);

            yield return new WaitForSeconds(0.01f);
        }
        noMoreTransition = 0.2f;
        yield return null;

    }

    private IEnumerator EmissionTransition()
    {
        if(others.Count != 0)
        { 
            while (colorTransition < 0.2)
            { 
                colorTransition += 0.02f;
                m_Material.SetColor("_EmissionColor", Color.magenta * colorTransition);
        
                yield return new WaitForSeconds(0.01f);
            }
            colorTransition = 0;
        }
        yield return null;
    }

    private IEnumerator DangerIncoming()
    {
        if (others.Count != 0)
        {
            while (dangerousTransition < 1)
            {
                dangerousTransition += 0.2f;
                m_Material.SetColor("_EmissionColor", Color.blue * dangerousTransition);

                yield return new WaitForSeconds(0.01f);
            }
            dangerousTransition = 0f;
        }
        yield return null;
    }
}
