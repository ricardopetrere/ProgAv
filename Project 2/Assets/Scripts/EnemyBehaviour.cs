using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour {
    Vector3 posOrigin;
    public Vector3 posEnd;
    EnemyController controller;
    Vector3 destination;
    
    // Use this for initialization
    void Start () {
        posOrigin = transform.position;
        posEnd += transform.position;
        controller = GetComponent<EnemyController>();
	}
	
	// Update is called once per frame
	void Update () {
	    if(transform.position == posOrigin)//Vai para o destino
        {
            destination = posEnd;
        }
        else if(transform.position == posEnd)//Vai para a posição inicial
        {
            destination = posOrigin;
        }
        controller.move(destination);
	}
}
