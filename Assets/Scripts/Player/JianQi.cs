using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JianQi : MonoBehaviour
{
    public float expandDistancePerFrame;
    [HideInInspector] public Vector2 currentDirection;
    public float damage = 10f;
    private BoxCollider2D boxCollider;
    private SpriteRenderer spriteRenderer;
    public int surviveFrame;
    private int currentFrame;
    private Vector3 startLocation;
    private float transparency;
    void Start()
    {
        //currentDirection=new Vector2 (1,0);
        boxCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        startLocation = transform.position;
        currentFrame = 0;
        transparency = 1f;

        transform.eulerAngles=Main.GetEulerAnglesByDirection(currentDirection);
        transform.Rotate(0, 0, 90f);
    }
    void Update()
    {
        switch (spriteRenderer.sprite.name)
        {
            case "jianQi_0":
                boxCollider.offset = new Vector2(-0.007259116f, 0.000311235f);
                boxCollider.size = new Vector2(0.080942f, 0.1167021f);
                break;
            case "jianQi_1":
                boxCollider.offset = new Vector2(-0.000391528f, -0.004839465f);
                boxCollider.size = new Vector2(0.1427509f, 0.2677902f);
                
                break;
            case "jianQi_2":
                boxCollider.offset = new Vector2(0.004759196f, 0.01232962f);
                boxCollider.size = new Vector2(0.2285965f, 0.4532168f);
                break;
            case "jianQi_3":
                boxCollider.offset = new Vector2(-0.002108455f, -0.001405686f);
                boxCollider.size = new Vector2(0.2629347f, 0.4944227f);
                break;
            case "jianQi_4":
                boxCollider.offset = new Vector2(-0.007259198f, 0.0003111959f);
                boxCollider.size = new Vector2(0.2732362f, 0.6489449f);
                currentFrame++;
                break;
            default:

                Debug.LogError("没有找到该图片！");
                break;

        }
        transparency -= 0.001f;
        spriteRenderer.color = new Color(1f, 1f, 1f, transparency);
        Vector2 unitDirection = SurfaceForm.UnitDirection(currentDirection);
        Vector2 moving =unitDirection*expandDistancePerFrame*Time.deltaTime;
        Vector3 finalMoving = new Vector3(moving.x, moving.y, 0);
        transform.position += finalMoving;
        if (currentFrame>=surviveFrame)
        {
            Destroy(gameObject);
        }
        

    }
}
