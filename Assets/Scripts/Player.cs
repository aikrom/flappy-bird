using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private SpriteRenderer spriteRenderer;

    private GameManager gameManager;

    private Vector3 direction;

    private int spriteIndex = 0;

    public Sprite[] spires;

    public float strength = 5f;

    public float gravity = -9.8f;


    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    private void Start()
    {
        InvokeRepeating(nameof(Animate), 0.15f, 0.15f);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            Jump();
        }

        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if(touch.phase == TouchPhase.Began)
            {
                Jump();
            }
        }

        Move();
    }

    private void OnEnable()
    {
        Vector3 position = transform.position;
        position.y = 0f;
        transform.position = position;
        direction = Vector3.zero;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Obstacle"))
        {
            gameManager.GameOver();
        } else if(collision.gameObject.CompareTag("Scoring"))
        {
            gameManager.IncreaseScore();
        }
    }

    public void Jump()
    {
        direction = Vector3.up * strength;
    }

    public void Move()
    {
        direction.y += gravity * Time.deltaTime;
        transform.position += direction * Time.deltaTime;
    }

    public void Animate()
    {
        spriteIndex++;

        if (spriteIndex >= spires.Length)
        {
            spriteIndex = 0;
        }

        spriteRenderer.sprite = spires[spriteIndex];
    }
}
