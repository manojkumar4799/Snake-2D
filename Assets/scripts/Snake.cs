using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class Snake : MonoBehaviour
{
    public int score;
    public TextMeshProUGUI player1Score;
    public Sprite snakeHead;
    public Transform bodyPart;
    public Sprite shieldEffect;
    public GameObject gameOverUI;

    Vector3 direction;
    Vector3 rotation;
    List<Transform> body;
    bool isShiledActive = false;
    bool right, left, down, up;
    void Start()
    {   right = true; left = false; down = true; up = true;
        body = new List<Transform>();
        body.Add(transform);  
        direction = Vector3.right;
        transform.position += direction;
                
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Food"))
        {
            SoundManager.Instance.PlaySound(Sound.Eat);
            Grow();
        }
        if (collision.gameObject.CompareTag("Frog"))
        {
            SoundManager.Instance.PlaySound(Sound.Eat);
            DecreaseSize();
            collision.gameObject.SetActive(false);
        }
        if (collision.gameObject.CompareTag("Walls"))
        {
            ScreenSwrap();
            SoundManager.Instance.PlaySound(Sound.Teleport);
            
        }
        if (collision.gameObject.CompareTag("Body")&&isShiledActive==false)
        {
            GameOver();
        }
        if (collision.gameObject.CompareTag("Shield"))
        {
            isShiledActive = true;
            collision.gameObject.SetActive(false);
        }
       
    }

    private void GameOver()
    {
        this.enabled = false;
        SoundManager.Instance.PlayBGM(Sound.GameOver);
        gameOverUI.SetActive(true);
    }

    private void ScreenSwrap()
    {
        Vector3 newPos = transform.position;
        if (up == false||down==false)
        {
             newPos.y = -(transform.position.y);
        }
        if (left == false||right==false)
        {
            newPos.x = -(transform.position.x);
        }
        transform.position = newPos;
    }

    private void FixedUpdate()
    {
        if (isShiledActive == true)
        {
            StartCoroutine(Shield());
        }
        for(int snakeBodyPart = body.Count - 1; snakeBodyPart > 0; snakeBodyPart--)
        {
            body[snakeBodyPart].position = body[snakeBodyPart - 1].position;
            body[snakeBodyPart].rotation = body[snakeBodyPart-1].rotation;
        }
        transform.position = new Vector3(Mathf.Round(transform.position.x) + direction.x,
            Mathf.Round(transform.position.y) + direction.y, 0f);

        transform.rotation = Quaternion.Euler(rotation);

    }
    void Update()
    {

        PlayerMovements();
    }

    private void PlayerMovements()
    {
        if (Input.GetKeyDown(KeyCode.W) && up == true)
        {
            direction = Vector2.up;
            rotation = new Vector3(0, 0, 90);
            right = true; left = true; down = false; up = true;
        }
        else if (Input.GetKeyDown(KeyCode.S) && down == true)
        {
            direction = Vector2.down;
            rotation = new Vector3(0, 0, 270);
            right = true; left = true; down = true; up = false;
        }
        else if (Input.GetKeyDown(KeyCode.D) && right == true)
        {
            direction = Vector2.right;
            rotation = new Vector3(0, 0, 0);
            right = true; left = false; down = true; up = true;
        }
        else if (Input.GetKeyDown(KeyCode.A) && left == true)
        {
            direction = Vector2.left;
            rotation = new Vector3(0, 0, 180);
            right = false; left = true; down = true; up = true;
        }
    }

    private void Grow()
    {
        score++;
        player1Score.text = " Score:" + score;
        Transform newBody= Instantiate(bodyPart);
        newBody.position = body[body.Count - 1].position;
        body.Add(newBody);
    }

    private void DecreaseSize()
    {
        score--;
        player1Score.text = " Score:" + score;
        int bodyCount = body.Count - 1;
        if (bodyCount >= 1)
        {
            Destroy(body[bodyCount].gameObject);
            body.RemoveAt(bodyCount);

        }
        else
        {
            return;
        }

    }

    IEnumerator Shield()
    {
        this.gameObject.GetComponent<SpriteRenderer>().sprite = shieldEffect;
        yield return new WaitForSeconds(3f);
        isShiledActive=false;
        this.gameObject.GetComponent<SpriteRenderer>().sprite = snakeHead;
    }

}
