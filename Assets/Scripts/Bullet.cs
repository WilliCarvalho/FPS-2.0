using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Bullet configuration")]
    public int range;
    public int damage;
    public float mass;
    public float speed;

    [Header("Imports")]
    public PlayerController player;
    public ParticleSystem impact;

    //Private
    private Rigidbody rb;
    private Vector3 origin;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        origin = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //Mass
        rb.mass = mass;
        //Go Forward
        Vector3 horizontal = transform.right * 0;
        Vector3 vertical = transform.forward * 1;
        Vector3 velocity = (horizontal + vertical).normalized * speed;
        rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
        
        //Range
        if(Vector3.Distance(origin, transform.position) > range)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Floor")
        {
            GameObject eff = Instantiate(impact, transform.position, Quaternion.identity).gameObject;
            eff.transform.position += new Vector3(0, 0.1f, 0);
            eff.transform.eulerAngles = new Vector3(-90, 0, 0);
        }
        Destroy(gameObject);
    }
}
