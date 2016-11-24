using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {
    public float Vida = 1;
    public GameObject tiro;
    float timeSinceLastAttack;
    public float attackCooldown;
    //Vector3 mPosition;
    public float Speed;
    Vector3 deltaMov;
    // Use this for initialization
 //   void Start () {
 //       mPosition = transform.position;
	//}
	
	// Update is called once per frame
	void Update () {
        if(Time.time-timeSinceLastAttack>attackCooldown)
        {
            fire();
        }
	}

    public void fire()
    {
        Instantiate(tiro, transform.position, transform.rotation);
        timeSinceLastAttack = Time.time;
    }

    public void move(Vector3 destination)
    {
        deltaMov = destination - transform.position;
        //Vector3 movi = new Vector3(0.7f, 0.5f, 0);
        //transform.position += (movi * Speed * Time.deltaTime);
        if(deltaMov.magnitude <= 0.01)
        {
            transform.position = destination;
        } else
        {
            transform.position += (deltaMov.normalized * Speed * Time.deltaTime);
        }
    }

    public bool takeDamage(float damageAmount)
    {
        Vida -= damageAmount;
        if(Vida<=0)
        {
            Destroy(gameObject);
        }
        return Vida <= 0;
    }



    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Colisão - Enemy");
        if (other.gameObject.tag == "Sword" && other.transform.root.gameObject.GetComponent<Player>().isAttacking)
        {
            if(takeDamage(other.gameObject.GetComponent<EspadaController>().damage))
            {
                Debug.Log("Inimigo morreu");
            }
            Debug.Log("Tirou vida");
        }
    }
}
