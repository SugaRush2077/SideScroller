using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController character;
    
    private Vector3 direction;

    // Animation Sprite
    public Sprite[] Slide_sprite;
    public Sprite[] sprite;
    private SpriteRenderer spriteRenderer;
    private int frame;

    // Player value
    public float gravity = 9.81f * 2f;
    public float jumpForce = 10f;

    //defaulte position
    private bool isSlide = false;
    private float default_radius = 0.36f;
    private float default_height = 0.76f;
    private Vector3 default_center = new Vector3(0, 0.05f, 0);

    // Cheat Code
    private bool isCheat = false;

    private void Awake()
    {
        character = GetComponent<CharacterController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        direction = Vector3.zero;
        Invoke(nameof(Animate), 0f);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    private void Update()
    {
        direction += Vector3.down * gravity * Time.deltaTime;

        if (character.isGrounded)
        {
            direction = Vector3.down;
            if (Input.GetButton("Jump"))
            {
                direction = Vector3.up * jumpForce;
            }
        }

        character.Move(direction * Time.deltaTime);

        if (Input.GetButton("Crouch"))
        {
            isSlide = true;
            slide();
        }
        else
        {
            character.height = default_height;
            character.radius = default_radius;
            character.center = default_center;
            isSlide = false;
        }

        if (Input.GetButtonUp("Crouch"))
        {
            isSlide = false;

        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            UnityEngine.Color newColor;
            if (isCheat) 
            {
                // Cheat Off
                newColor = new UnityEngine.Color(0, 0, 0);
                //GameManager.Instance.ScoreTitle.color = newColor;

                isCheat = false;
                Debug.Log("CheatOff!");
            }
            else
            {
                // Cheat On
                newColor = new UnityEngine.Color(83f, 83f, 83f);
                //GameManager.Instance.ScoreTitle.color = newColor;
                //GameManager.Instance.ScoreTitle.color = new UnityEngine.Color(100, 0, 0, 255);
                //GameManager.Instance.ScoreTitle.color = Color.blue;
                isCheat = true;
                Debug.Log("CheatOn!");
            }

        }
    }

    private void slide()
    {
        character.height = 0.2f;
        character.radius = 0.2f;
        character.center = new Vector3(0, -0.13f, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isCheat)
        {
            if (other.CompareTag("Obstacle") || other.CompareTag("Enemy"))
            {
                GameManager.Instance.GameOver();
            }
        }
        
    }

    private void Animate()
    {
        frame++;
        if (isSlide)
        {
            
            if (frame >= sprite.Length)
            {
                frame = 0;
            }

            if (frame >= 0 && frame < sprite.Length)
            {
                spriteRenderer.sprite = Slide_sprite[frame];
            }

        }
        else
        { 
            if (frame >= sprite.Length)
            {
                frame = 0;
            }

            if (frame >= 0 && frame < sprite.Length)
            {
                spriteRenderer.sprite = sprite[frame];
            }

            
        }
        
        Invoke(nameof(Animate), 1f / GameManager.Instance.gameSpeed);

    }





}
