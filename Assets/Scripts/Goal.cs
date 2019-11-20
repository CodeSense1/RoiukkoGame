using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour {
    
    public ParticleSystem winParticle;

    public static Goal instance;
    public bool HasWon { get; set; }

    private void Start()
    {
        instance = this;
        HasWon = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "pushAndHide")
        {
            HasWon = true;

            Instantiate(winParticle, FindObjectOfType<playerMovement>().transform.position, Quaternion.identity);
            winParticle.Play();
            

        }
    }
}
