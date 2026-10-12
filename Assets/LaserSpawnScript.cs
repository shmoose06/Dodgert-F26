using UnityEngine;

public class LaserSpawnScript : MonoBehaviour
{

    public GameObject laser;
    public float spawnRate = 20;
    //public float spawnRateMax = 15;
    private float timer = 0;

    //AUDIO
    AudioManager audioManager;
    private bool hasSoundPlayed = false;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //spawnLaser();
    }

    // Update is called once per frame
    void Update()
    {

        if (timer < spawnRate)
        {
            timer += Time.deltaTime;
        } else
        {
            spawnLaser();
            timer = 0;
        }

            
    }

    void spawnLaser()
    {
        Instantiate(laser, transform.position, transform.rotation);
        audioManager.PlaySFX(audioManager.alert2);
    }

}
