using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {
    public int Vida = 1;
    public GameObject tiro;
    float timeSinceLastAttack;
    public float attackCooldown;
    Vector3 mPosition;
	// Use this for initialization
	void Start () {
        mPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        if(Time.time-timeSinceLastAttack>attackCooldown)
        {
            fire();
        }
	}

    public void fire()
    {
        Instantiate(tiro, mPosition, transform.rotation);
        timeSinceLastAttack = Time.time;
    }
}
