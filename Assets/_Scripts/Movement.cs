using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour 
{
    [SerializeField]private float speed;
	private float runScaler;
    [SerializeField]private float runSpeed;
	private Rigidbody rigid;
	private Vector3 movement;
	private Vector3 prevMovement;

	private void Start()
	{
		runScaler = 0;
		movement = Vector3.zero;
		prevMovement = Vector3.zero;

		rigid = GetComponent<Rigidbody> ();
	}

	private void Update()
	{
		var x = Input.GetAxisRaw ("Horizontal");
		var z = Input.GetAxisRaw ("Vertical");
		if ((z!=0 || x!=0) && Input.GetKey(KeyCode.LeftShift))
			runScaler = runSpeed;
		else
			runScaler = runSpeed/runSpeed;

		movement = new Vector3 (x,0,z);

		if (!(x == 0 && z == 0))
			prevMovement = movement;
	}

	private void FixedUpdate()
	{
		Vector3 velocity = movement.normalized * speed * runScaler * Time.fixedDeltaTime;
		rigid.MovePosition(rigid.position + velocity);
		transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(prevMovement), 0.1F);
	}
}