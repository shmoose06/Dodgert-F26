using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class EvilPillarManager : MonoBehaviour
{
    
    //Movement
    [SerializeField]private float rangeX = 12.5f;
    private float startingPos = 17f;
    private float extendedPos = 5f;
    private int directionX = 1;
    private int DOWN = 1;
    private int moveSpeed = 5;
    private int slamSpeed = 12;
    [SerializeField]private float diveFrequency = 6f; //Higher less
    public bool isDiving = false;

    [SerializeField] private float rotationAmount = 2f;
    [SerializeField] private float rotationSpeed = 0.5f;

    private float timer = 0f;

    public LogicScript logic;


    //AUDIO
    AudioManager audioManager;
    private bool hasSoundPlayed = false;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

    void Update()
    {

        timer++;

        //idle rotation:
        float angle = Mathf.Sin(Time.time * rotationSpeed) * rotationAmount;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        //Timer goes up if not Diving
        if (timer > diveFrequency && !isDiving)
        {
            isDiving = true;
        }

        //Move Up if not Diving
        if (!isDiving)
        {
            
            //Move if out of sight
            if (transform.position.y > startingPos )
            {
                //Movement X
                transform.position += Vector3.right * directionX * moveSpeed * Time.deltaTime;

                if (transform.position.x >= rangeX)
                {
                    directionX = -1;
                }
                else if (transform.position.x <= -rangeX)
                {
                    directionX = 1;
                }
            }
            else { transform.position += Vector3.down * -DOWN * moveSpeed * Time.deltaTime; } //Move OOS
            
        } else if(isDiving) //DIVING
        {
            
            if (transform.position.y > extendedPos)
            {
                transform.position += Vector3.down * DOWN * slamSpeed * Time.deltaTime;
                timer = 0f;

                if(!hasSoundPlayed)
                {
                    audioManager.PlaySFX(audioManager.alert);
                    hasSoundPlayed = true;
                }
                
            } else
            {
                timer++;
                if(timer > diveFrequency / 2)
                {
                    isDiving = false;
                    hasSoundPlayed = false;
                }
                
                
            }
        }

    }
}
