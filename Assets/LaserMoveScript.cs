using UnityEngine;

public class LaserMoveScript : MonoBehaviour
{

    public float moveSpeed = 5;
    public float deadZone = -15;

    public LogicScript logic;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (Vector3.left * moveSpeed ) * Time.deltaTime;

        //Delete Lasers out of sight
        if(transform.position.x < deadZone )
        {
            Destroy(gameObject);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
        {
        PlayerMovement player = collision.GetComponent<PlayerMovement>();
        if (collision.CompareTag("Player") && !(player.IsInvulnerable()))
        {
            logic.loseHealth();
        }
    }

}
