using UnityEngine;
using System.Collections;

public class TiroController : ControllerRayCast {

    public Vector2 startPosition;
    public float velocity;
    public float directionX;
    public float directionY;

	// Use this for initialization
	//void Start () {
 //       base.Start();
 //       //this.startPosition = new Vector2(5,0);
 //       //transform.position = startPosition;
 //   }
	
	// Update is called once per frame
	void Update () {
        
        transform.position -= (transform.right)*velocity*Time.deltaTime;
        Debug.DrawRay(transform.position, -transform.right*1,Color.red);
        collisions(velocity * Time.deltaTime);
        UpdateRaycastOrigins();
    }

    //Caso o objeto já esteja invisível quando instanciado, ele não entra nesse método
    //Esse método ocorre APENAS na transição de visível para invisível
    //void OnBecameInvisible()
    //{
    //    Destroy(gameObject);
    //}

    void collisions(float deltaMove)
    {
        directionX = -1;
        float rayLength = Mathf.Abs(deltaMove) + skinWidth;

        for (int i = 0; i < horizontalRayCount; i++)
        {

            Vector2 rayOrigin = (directionX == -1) ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight;
            rayOrigin += Vector2.up * (horizontalRaySpacing * i);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin,Vector2.left, rayLength, collisionMask);

            Debug.DrawRay(rayOrigin, Vector2.up * directionY, Color.red);

            if (hit)
            {
                Debug.Log("log");

                if (hit.collider.name == "Inimigo")
                {
                    continue;
                }
                Debug.Log("!!COLIDIU!!");
                Destroy(gameObject);
            }
        }
    }
}
