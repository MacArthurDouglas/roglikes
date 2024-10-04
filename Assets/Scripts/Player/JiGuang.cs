using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JiGuang : MonoBehaviour
{
    GameObject player;
    SurfaceForm surfaceForm;
    private int curFrame;
    SpriteRenderer spriteRenderer;
    int surviveFrame = 2;
    private float centerDistance = 1.5f;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GameObject.FindWithTag("Player");
        surfaceForm=player.GetComponent<SurfaceForm>();
        curFrame = 0;
    }
    private void Update()
    {
        this.transform.eulerAngles = Main.GetEulerAnglesByDirection(PlayerControl.CurrentDirection);
        Vector3 center = new Vector3(0, 0, 0);
        Vector2 unitDirection = SurfaceForm.UnitDirection(PlayerControl.CurrentDirection);

        center.x = player.transform.position.x + unitDirection.x * centerDistance;
        center.y = player.transform.position.y + unitDirection.y * centerDistance;
        center.z = player.transform.position.z;
        this.transform.position=center;
                if (spriteRenderer.sprite.name == "baozha_10")
                {
                    curFrame++;
                    if (curFrame >= surviveFrame)
                    {
                        Destroy(gameObject);
                    }
                }
    }

}
