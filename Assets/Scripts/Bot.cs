using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bot : MonoBehaviour
{
    public int life;
    public float range;
    public float speed;
    public float speedAim;
    public float speedFire;
    public float speedBullet;
    public int damage;
    public Vector3 minArea;
    public Vector3 maxArea;
    public GameObject bullet;
    public GameObject bulletOutPosition;

    private NavMeshAgent agent;
    private PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = FindObjectOfType<PlayerController>();
        agent.speed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        bool foundPlayer = false;
        if(Vector3.Distance(player.transform.position, transform.position) < range)
        {
            foundPlayer = true;
        }

        if (foundPlayer)
        {
            agent.SetDestination(player.transform.position);
            if(Vector3.Distance(player.transform.position, transform.position) < range / 2)
            {
                StartCoroutine(AimTime());
            }
        }
        else
        {
            if(Vector3.Distance(transform.position, agent.destination) < 2)
            {
                float x = Random.Range(0 + minArea.x, maxArea.x);
                float y = minArea.y;
                float z = Random.Range(0 + minArea.z, maxArea.z);

                agent.SetDestination(new Vector3(x, y, z));
            }
        }
    }

    public void TakeDamage(int i)
    {
        life -= i;
        if (life <= 0)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator AimTime()
    {
        yield return new WaitForSeconds(speedAim);
        transform.LookAt(player.transform.position);
        StartCoroutine(FireTime());
    }

    IEnumerator FireTime()
    {
        yield return new WaitForSeconds(speedFire);
        GameObject go = Instantiate(bullet, bulletOutPosition.transform.position, bulletOutPosition.transform.rotation);
        Bullet _bullet = go.GetComponent<Bullet>();
        _bullet.mass = 1;
        _bullet.damage = damage;
        _bullet.range = 500;
        _bullet.speed = speedBullet;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Bullet")
        {
            TakeDamage(25);
        }
    }
}
