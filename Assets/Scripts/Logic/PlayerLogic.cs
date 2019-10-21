using UnityEngine;

public class PlayerLogic : MonoBehaviour {

    
    public GameObject goalGo;
    public ParticleSystem collectAllParticles;
    
  

    public int beerPoints = 0;
    public int maxBeerPoints;
    public bool hasCollectedAllPoints = false;

	// Use this for initialization
	void Start () {

        // Count amount of beer-ogjects in hierarchy
        maxBeerPoints = FindObjectsOfType<powerup>().Length;
    }
    
	void Update () {
        
        // Play particle effect if all points have been collected
        if (beerPoints == maxBeerPoints && !hasCollectedAllPoints)
        {
            hasCollectedAllPoints = true;
            collectAllParticles.Play(); // Why this fuc*** doesn't work???
        }
	}

    
}
