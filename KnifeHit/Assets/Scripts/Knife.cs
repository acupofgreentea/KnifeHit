using UnityEngine;
using UnityEngine.EventSystems;

public class Knife : MonoBehaviour
{
    [SerializeField] private Vector2 throwForce;

    private Rigidbody2D rb;

    private BoxCollider2D knifeCollider;
    private ParticleSystem particle;
    private AudioSource source;

    private LevelManager levelManager;

    private bool isActive = true;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        knifeCollider = GetComponent<BoxCollider2D>();
        particle = GetComponent<ParticleSystem>();
        source = GetComponent<AudioSource>();
        
        levelManager = FindObjectOfType<LevelManager>();
    }

    // Before build use !EventSystem.current.IsPointerOverGameObject()
    private void Update()
    {
        if(Input.GetMouseButtonDown(0) && isActive && !EventSystem.current.IsPointerOverGameObject())
        {   
            UseKnife();
        }
    }
    
    private void UseKnife()
    {
        rb.AddForce(throwForce, ForceMode2D.Impulse);
        rb.gravityScale = 1;
        UIManager.Instance.DestroyUsedKnives();
    }

    private void HitTomato()
    {
        GameManager.Instance.UpdateScore(20);
        //AudioManager.Instance.PlayAudio(clip, source);        play tomato sound
    }
    
    private void HitLog()
    {
        particle.Play();
        AudioManager.Instance.PlayAudio(source.clip, source);
    }
    private void SetPhysicsOnHitKnife() => rb.velocity = new Vector2(rb.velocity.x, -2f);

    private void SetPhysicsOnHitLog()
    {
        rb.velocity = new Vector2(0,0);
        rb.bodyType = RigidbodyType2D.Kinematic;

        knifeCollider.offset = new Vector2(knifeCollider.offset.x, -0.4f);
        knifeCollider.size = new Vector2(knifeCollider.size.x, 1.2f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Tomato"))
        {
            HitTomato();
            other.gameObject.SetActive(false);
        }
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(!isActive)
        {
            return;
        } 

        isActive = false;
        
        if(other.collider.CompareTag("Log"))
        {
            HitLog();
            
            SetPhysicsOnHitLog();

            transform.SetParent(other.collider.transform);
            
            levelManager.OnSuccessfullHit();
        }
        else
        {
            SetPhysicsOnHitKnife();
            GameManager.Instance.Invoke("GameOver", 0.5f);
        }
    }
}
