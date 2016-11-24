using UnityEngine;
using System.Collections;

public class TiroController : ControllerRayCast {

    public Vector2 startPosition;
    public float velocity;
    public float directionX;
    public float directionY;
    //public GameObject parent;
    public float damage;

    // Update is called once per frame
    void Update () {
        
        transform.position -= (transform.right*velocity*Time.deltaTime);
        Debug.DrawRay(transform.position, -transform.right*1,Color.red);
        collisions(velocity * Time.deltaTime);
        UpdateRaycastOrigins();
    }

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
                if (LayerMask.LayerToName(hit.transform.gameObject.layer) == "Player")
                {
                    if (hit.transform.gameObject.tag == "Sword")
                    {
                        if (hit.transform.root.GetComponent<Player>().attacking)
                        {
                            Destroy(gameObject);
                        }
                    }
                    else
                    {
                        hit.transform.GetComponent<Player>().takeDamage(damage);
                        Destroy(gameObject);
                    }
                }
                else
                {
                    //Debug.Log("!!COLIDIU!!");
                    Destroy(gameObject);
                }
            }
        }
    }
}
