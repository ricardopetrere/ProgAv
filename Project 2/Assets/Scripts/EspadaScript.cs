using UnityEngine;
using System.Collections;

public class EspadaScript : MonoBehaviour {
    public Player player;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void animationCallback()
    {
        //Debug.Log("Fim de animação");
        player.resetAttack();
    }
}
