using UnityEngine;

public class StartIntro : MonoBehaviour
{
    public Animator animator;
    public ScreenStart ScreenStart;
    public AudioSource gamestart;
    bool startgame = false;
    void Start()
    {
        gamestart = GameObject.Find("GameStartSound").GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }


    void Update()
    {
        if (Input.touchCount > 0 && ScreenStart.intro == true)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began && ScreenStart.intro == true)
            {
                startgame = true;
            }
        }
        if (Input.GetMouseButtonDown(0) && ScreenStart.intro == true)
        {
            startgame = true;
        }

        if (startgame)
        {
            gamestart.Play();
            ScreenStart.IntroSound.Stop();
            ScreenStart.Intromusic.Stop();
            ScreenStart.intro = false;
            startgame = false;
            animator.SetBool("Startgame", true);
            StartCoroutine(ScreenStart.Starting());
        }

    }
}
