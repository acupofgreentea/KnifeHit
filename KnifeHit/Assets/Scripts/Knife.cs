using UnityEngine;
using UnityEngine.EventSystems;

public class Knife : MonoBehaviour
{
    [SerializeField]
    Vector2 throwForce;

    Rigidbody2D rb;

    BoxCollider2D knifeCollider;
    ParticleSystem particle;
    UIManager uiManager;

    AudioSource source;

    bool isActive = true;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        knifeCollider = GetComponent<BoxCollider2D>();
        particle = GetComponent<ParticleSystem>();
        uiManager = FindObjectOfType<UIManager>();
        source = GetComponent<AudioSource>();
    }
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && isActive && !EventSystem.current.IsPointerOverGameObject())
        {   
            rb.AddForce(throwForce, ForceMode2D.Impulse);
            rb.gravityScale = 1;
            uiManager.DecreaseUsedKnives();
        }
    }
    
    void OnCollisionEnter2D(Collision2D other)
    {
        if(!isActive)
            return;
        
        isActive = false;

        if(other.collider.CompareTag("Log"))
        {
            particle.Play();

            AudioManager.Instance.PlayAudio(source.clip, source);

            rb.velocity = new Vector2(0,0);
            rb.bodyType = RigidbodyType2D.Kinematic;
            transform.SetParent(other.collider.transform);

            knifeCollider.offset = new Vector2(knifeCollider.offset.x, -0.4f);
            knifeCollider.size = new Vector2(knifeCollider.size.x, 1.2f);

            GameManager.Instance.OnSuccessfullHit();
        }
        else if(other.collider.CompareTag("Knife"))
        {
            rb.velocity = new Vector2(rb.velocity.x, -2f);

            StartCoroutine(GameManager.Instance.GameOverSequence(1f));
        }
    }
}
