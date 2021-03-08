using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
  public GameController _GC;

  [Header("Config Move")]
  [SerializeField] float walkSpeed = 5f;
  [SerializeField] float jumpForce = 500f;

  [Header("Quests Info")]
  public int jumpCount = 0;
  public int enemyCount = 0;
  public int itemCount = 0;

  private Rigidbody2D rb;
  private float xAxis;
  private bool isJumpPressed;

  void Awake()
  {
    rb = GetComponent<Rigidbody2D>();
  }

  // Update is called once per frame
  void Update()
  {
    if (_GC.currentState == gameState.GAMEPLAY)
    {
      GetInput();
    }
    else 
    {
      walkSpeed = 0;
    }
  }

  void FixedUpdate()
  {
    ApplyMovement();
    ApplyJumping();
  }

  void GetInput()
  {
    xAxis = Input.GetAxisRaw("Horizontal");

    if (Input.GetButtonDown("Jump") && rb.velocity.y == 0)
    {
      isJumpPressed = true;
      jumpCount++;
      _GC.remainQuests();
    }
  }

  void ApplyMovement()
  {
    rb.velocity = new Vector2(xAxis * walkSpeed, rb.velocity.y);
  }

  void ApplyJumping()
  {
    if (isJumpPressed)
    {
      isJumpPressed = false;
      rb.AddForce(new Vector2(0, jumpForce));
    }
  }

  void OnTriggerEnter2D(Collider2D col)
  {
    switch (col.gameObject.tag)
    {
      case "Exit":
        _GC.exitLevel();
        break;

      case "Enemy":
        enemyCount++;
        _GC.remainQuests();
        Destroy(col.gameObject);
        break;

      case "Collectable":
        itemCount++;
        _GC.remainQuests();
        Destroy(col.gameObject);
        break;
    }
  }
}
