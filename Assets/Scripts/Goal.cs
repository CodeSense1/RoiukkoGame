using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour {
    
    public ParticleSystem winParticle;

    public bool HasWon;

    private void Start()
    {
        HasWon = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "pushAndHide")
        {
            HasWon = true;
            winParticle.Play();
        }
    }
}
