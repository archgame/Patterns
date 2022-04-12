using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BallController : MonoBehaviour
{
    [SerializeField]
    private float Speed;

    private float Size = 1;

    private Rigidbody _rigidbody;
    private Camera _camera;

    // Start is called before the first frame update
    private void Start()
    {
        _rigidbody = gameObject.GetComponent<Rigidbody>();
        _camera = Camera.main;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 input = new Vector3(x, 0, z);

        Vector3 move = (input.z * _camera.transform.forward) + (input.x * _camera.transform.right);

        _rigidbody.AddForce(move * Speed * Time.fixedDeltaTime * Size); //fixed delta time
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Sticky") && collision.transform.localScale.magnitude <= Size)
        {
            collision.transform.parent = this.transform;
            Size += collision.transform.localScale.magnitude;
        }
    }
}