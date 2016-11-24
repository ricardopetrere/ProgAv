using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Player))]
public class PlayerInput : MonoBehaviour {

    Player player;

	// Use this for initialization
	void Start () {
        player = GetComponent<Player>();
	}
	
	// Update is called once per frame
	void Update () {
	    Vector2 directionalInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        player.SetDirectionalInput(directionalInput);

        if(Input.GetKeyDown(KeyCode.Space)) {

            StartCoroutine(Jump());
            StopCoroutine(Jump());

            //player.mAnimator.SetTrigger("Jump");
            //player.OnJumpInputDown();
        }
        if (Input.GetKeyUp(KeyCode.Space)) {
            player.OnJumpInputUp();
        }
        if(Input.GetKeyDown(KeyCode.E))
        {
            player.Attack();
        }
    }

    IEnumerator Jump () {
        player.mAnimator.SetTrigger("Jump");
        yield return new WaitForSeconds(0.2f);
        player.OnJumpInputDown();
    }
}
