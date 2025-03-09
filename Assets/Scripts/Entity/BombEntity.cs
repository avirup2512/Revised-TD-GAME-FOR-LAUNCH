using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Script.globalVar;
public class BombEntity : MonoBehaviour
{
    public GameObject targetObject;
    public Vector2 targetPosition;
    public GameObject sourceObject;
    public float speed = 15f;
    public float positionX;
    public float targetPositionX;

    public float distance;
    public float nextX;
    public float baseY;
    public float height;

    public Animator anim;
    public bool fadeOutStarted = false;

    public SpriteRenderer renderer;

    public GlobalData globalVar;
    public AudioSource luncherBlast;
    bool isBlasst  = false;
    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        renderer = gameObject.GetComponent<SpriteRenderer>();
        luncherBlast = GameObject.Find("bombBlast").GetComponent<AudioSource>();
        isBlasst = false;
    }

    // Update is called once per frame
    void Update()
    {
        // transform.Rotate (Vector3.right * 50 * Time.deltaTime, Space.World);
        positionX = sourceObject.transform.position.x;
        // targetPositionX = targetObject.transform.position.x;
        targetPositionX = targetPosition.x;

        distance = targetPositionX - positionX;
        nextX = Mathf.MoveTowards(transform.position.x, targetPositionX, speed * Time.deltaTime);
        baseY = Mathf.Lerp(sourceObject.transform.position.y, targetPosition.y, (nextX - positionX) / distance);
        height = 2 * (nextX - positionX) * (nextX - targetPositionX) / (-0.25f * distance * distance);

        Vector3 movePosition = new Vector3(nextX, baseY + height, transform.position.z);
        // transform.rotation = LookAtTarget(movePosition - transform.position);

        if (Mathf.Floor(transform.position.x) == Mathf.Floor(targetPositionX) && !isBlasst)
        {

            anim.SetTrigger("blast");
            luncherBlast.Play();
            isBlasst = true;
            if (!fadeOutStarted)
                startFadingOut();

            // globalVar.bombExplosion.Invoke();
        }
        else
        {
            transform.position = movePosition;
        }
        if (renderer.color.a < .1)
        {
            StopCoroutine("fadeOut");
            deActivate();
        }
    }
    private void deActivate()
    {
        gameObject.SetActive(false);
    }
    public void startFadingOut()
    {
        StartCoroutine("fadeOut");
        Collider2D[] affectedEnemy = Physics2D.OverlapCircleAll(targetPosition, 2.00f);
        // Hero.fire(targetPosition);
        for (int i = 0; i < affectedEnemy.Length; i++)
        {
            GameObject touchedObject = affectedEnemy[i].transform.gameObject;
            if (touchedObject.tag == "Enemy")
            {

                EnemyEntity Enemy = touchedObject.GetComponent<EnemyEntity>();
                if (Enemy)
                {
                    Enemy.GetHit(1000);
                }
            }
        }
        fadeOutStarted = true;

    }
    IEnumerator fadeOut()
    {
        for (float f = 1f; f >= -1f; f -= 0.01f)
        {
            Color c = renderer.color;
            c.a = f;
            renderer.color = c;
            yield return new WaitForSeconds(0.03f);
        }
    }
    public static Quaternion LookAtTarget(Vector2 rotation)
    {
        return Quaternion.Euler(0, 0, Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg);
    }
}
