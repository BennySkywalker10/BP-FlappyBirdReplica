using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    public float upForce = 200f;

    public int oofs = 2;

    private bool isDead = false;

    private Rigidbody2D rb2d;

    private Animator anim;

    private SpriteRenderer spritey;

    private PolygonCollider2D poliCol;

    private AudioSource grunt;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spritey = GetComponent<SpriteRenderer>();
        poliCol = GetComponent<PolygonCollider2D>();
        grunt = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isDead == false)
        {
            if(Input.GetButtonDown("Flap"))
            {
                rb2d.velocity = Vector2.zero;
                rb2d.AddForce(new Vector2(0, upForce));
                anim.SetTrigger("Flap");
                grunt.Play();
            }
        }
        
    }

    void OnCollisionEnter2D (Collision2D other)
    {
        if (other.gameObject.tag == "column")
        {
            oofs--;
            if (oofs <= 0)
            {
                rb2d.velocity = Vector2.zero;
                isDead = true;
                anim.SetTrigger("Die");
                GameControl.instance.BirdDied();
            }
            else
            {
                poliCol.enabled = false;
                spritey.color = new Color(1, 0, 0, 0.5f);
                StartCoroutine(EnableBox(1.0F));
            }
        }
        else
        {
            rb2d.velocity = Vector2.zero;
            isDead = true;
            anim.SetTrigger("Die");
            GameControl.instance.BirdDied();
        }
        
    }

    IEnumerator EnableBox(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        poliCol.enabled = true;
        spritey.color = new Color(1, 1, 1, 1);
    }
}
