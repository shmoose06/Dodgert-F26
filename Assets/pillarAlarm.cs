using UnityEngine;
using UnityEngine.UIElements;

public class pillarAlarm : MonoBehaviour
{
    [SerializeField] private float shadowPos = 0f;

    private EvilPillarManager pillarManager;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        GameObject pillarObject = GameObject.FindWithTag("Pillar");
        pillarManager = pillarObject.GetComponent<EvilPillarManager>();

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        transform.position = new Vector3(
            transform.position.x,
            shadowPos,
            transform.position.z
        );

        Color color = spriteRenderer.color;

        if (pillarManager.isDiving)
        {
            color.a = 0.8f;      // Fully visible
        }
        else
        {
            color.a = 0.01f;   // Mostly transparent
        }

        spriteRenderer.color = color;
    }
}