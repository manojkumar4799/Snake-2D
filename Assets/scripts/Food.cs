using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public BoxCollider2D gridArea;
    public Transform shield;
    public Transform frog;

    int randomSpawner;
    void Start()
    {
        RandomFoodSpawn();
        shield.gameObject.SetActive(false);
        frog.gameObject.SetActive(false);
    }

    private void RandomFoodSpawn()
    {
        randomSpawner=Random.Range(1,10);
        Bounds bounds = gridArea.bounds;
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);
        transform.position = new Vector3(Mathf.Round(x), Mathf.Round(y), 0);
        if (randomSpawner >= 3)
        {
            StartCoroutine(FrogSpawner());
        }
        if (randomSpawner >= 5)
        {
            StartCoroutine(ShieldSpawner());
            
        }
       
    }

    IEnumerator FrogSpawner()
    {
        Bounds bounds = gridArea.bounds;
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);
        frog.transform.position = new Vector3(Mathf.Round(x), Mathf.Round(y), 0);
        frog.gameObject.SetActive(true);
        yield return new WaitForSeconds(4f);
        frog.gameObject.SetActive(false);
    }

    IEnumerator ShieldSpawner()
    {
        Bounds bounds = gridArea.bounds;
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);
        shield.transform.position = new Vector3(Mathf.Round(x), Mathf.Round(y), 0);
        shield.gameObject.SetActive(true);
        yield return new WaitForSeconds(3f);
        shield.gameObject.SetActive(false);
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            RandomFoodSpawn(); 
        }
    }

}
