using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayController : MonoBehaviour
{
    public GameObject player;
    public GameObject enemy;
    public GameObject dieCanvas;
    public int enemyQt;

    // Start is called before the first frame update
    void Start()
    {

        for (int i = 0; i < enemyQt; i++)
        {
            int x = Random.Range(-28, 28);
            float y = 0.5f;
            int z = Random.Range(-28, 28);
            Instantiate(enemy, new Vector3(x, y, z), Quaternion.identity);
        }    
    }

    public void Respawn()
    {
        Instantiate(player, new Vector3(0, 0, 0), Quaternion.identity);
        player.GetComponent<PlayerController>().dieCanvas.SetActive(false);
    }
}
