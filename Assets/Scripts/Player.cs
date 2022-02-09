using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform groundTransformCheck = null;
    private bool jumpKeyPressed;
    private float horizontalInput;
    private Rigidbody rigidbodycomponent;
    [SerializeField]  private LayerMask playerMask;
    private int superjump;

    // Start is called  before the first frame update
    void Start()
    {
        rigidbodycomponent = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) == true){
            jumpKeyPressed = true;
        }
        horizontalInput = Input.GetAxis("Horizontal");
    }

    private void FixedUpdate()
    {


        rigidbodycomponent.velocity = new Vector3(horizontalInput, rigidbodycomponent.velocity.y, 0);


        if (Physics.OverlapSphere(groundTransformCheck.position,0.1f, playerMask
           ).Length == 0)
        {
            return;
        }

        if (jumpKeyPressed == true)
        {
            float jumppower = 5;
            if(superjump > 0)
            {
                jumppower *= 2;
                superjump--;
            }
            rigidbodycomponent.AddForce(Vector3.up * jumppower, ForceMode.VelocityChange);
            jumpKeyPressed = false;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 7)
        {
            Destroy(other.gameObject);
            superjump++;
        }
    }

}
