using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public float speed = 10f;
	public float jumpForce = 5f;
	//Components
	private Rigidbody2D rb2d;
	private SpriteRenderer sprite;
	private BoxCollider2D bCol2d;
	//Private attributes
	[SerializeField]
	private bool jump = false;

	// Start is called before the first frame update
	void Start()
	{
		rb2d = GetComponent<Rigidbody2D>();
		sprite = GetComponent<SpriteRenderer>();
		bCol2d = GetComponent<BoxCollider2D>();
	}

	private void Update()
	{
		//Pressing the key space
		if (Input.GetKeyDown(KeyCode.Space))
		{
			//Let the player jump
			jump = true;
		}
	}

	void FixedUpdate()
	{

		//Store the current horizontal input in the float moveHorizontal.
		float moveHorizontal = Input.GetAxis("Horizontal");

		//Flip the sprite according to the direction
		if (moveHorizontal < 0)
		{
			sprite.flipX = true;
		}
		else if (moveHorizontal > 0)
		{
			sprite.flipX = false;
		}

		//Move the player through its body
		rb2d.velocity = new Vector2(moveHorizontal * speed, rb2d.velocity.y > -4.0f ? rb2d.velocity.y : -4.0f);

		//Check if the player ray is touching the ground and jump is enable
		if (Grounded() && jump)
		{
			//Jump
			rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
		}
		//Disable the jump
		jump = false;

	}

	bool Grounded()
	{
		//Laser length
		float laserLength = 0.025f;
		//Start point of the laser
		Vector2 startPosition = (Vector2)transform.position - new Vector2(0, (bCol2d.bounds.extents.y + 0.05f));
		//Hit only the objects of Floor layer
		int layerMask = LayerMask.GetMask("Ground");
		//Check if the laser hit something
		RaycastHit2D hit = Physics2D.Raycast(startPosition, Vector2.down, laserLength, layerMask);
		//The color of the ray for debug purpose
		Color rayColor = Color.red;
		//If the object is not null
		if (hit.collider != null)
		{
			//Change the color of the ray for debug purpose
			rayColor = Color.green;
		}
		else
		{
			//Change the color of the ray for debug purpose
			rayColor = Color.red;
		}
		//Draw the ray for debug purpose
		Debug.DrawRay(startPosition, Vector2.down * laserLength, rayColor);
		//If the ray hits the floor return true, false otherwise
		return hit.collider != null;
	}
}


