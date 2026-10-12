using UnityEngine;

public class coinLogic : MonoBehaviour
{
    [SerializeField] private float maxTilt = 20f;
    [SerializeField] private float rockSpeed = 2f;

    public LogicScript logic;
    private float timer = 0f;
    private float lifetime = 1000;

    private void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        timer = 0f;
    }
    void Update()
    {
        float angle = Mathf.Sin(Time.time * rockSpeed) * maxTilt;

        transform.rotation = Quaternion.Euler(0, 0, angle);

        //Destroy if stagnant for too long
        if (timer < lifetime)
        {
            timer++;
        } else
        {
            Destroy(gameObject);
            timer = 0f;
        }
            
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        logic.addScore();
        Destroy(gameObject);
    }

}