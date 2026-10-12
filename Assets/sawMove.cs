using UnityEngine;

public class sawMove : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 4f;
    [SerializeField] private float rangeX = 8.5f;
    [SerializeField] private float rangeY = 4.5f;
    private int directionX = 1;
    private int directionY = 1;
    private float rotateSpeed = -270f;

    public LogicScript logic;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

    // Update is called once per frame
    void Update()
    {

        //MOVEMENT X
        transform.position += Vector3.right * directionX * moveSpeed * Time.deltaTime;

        if (transform.position.x >= rangeX)
        {
            directionX = -1;
            swapRotation();
        }
        else if (transform.position.x <= -rangeX)
        {
            swapRotation();
            directionX = 1;
        }

        //MOVEMENT Y
        transform.position += Vector3.up * directionY * moveSpeed * Time.deltaTime;

        if (transform.position.y >= rangeY)
        {
            swapRotation();
            directionY = -1;
        }
        else if (transform.position.y <= -rangeY)
        {
            swapRotation();
            directionY = 1;
        }

        //ROTATE
        transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerMovement player = collision.GetComponent<PlayerMovement>();
        if (collision.CompareTag("Player") && !(player.IsInvulnerable()))
        {
            logic.loseHealth();
        }
    }
    public void swapRotation()
    {
        rotateSpeed = -rotateSpeed;
    }

}
