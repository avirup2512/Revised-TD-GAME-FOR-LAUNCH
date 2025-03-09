using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Script.interfaces;
using Assets.Script.globalVar;
using UnityEngine.UI;

namespace Assets.Script.entity

{
    public class HeroEntity : MonoBehaviour
    {

        [SerializeField] public GameObject FirePoint;
        [SerializeField] public GameObject bulletTrails;
        [SerializeField] public float _weaponRange = 10f;
        public Transform Bullet;
        public Rigidbody2D BulletRB;
        public LineRenderer lineRenderer;
        private GameObject HeroLifeBar;

        public Slider LifeBar;
        private float HeroLife = 100f;

        public GlobalData GlobalVar;
        public GameObject GunObject;
        public Animator GunAnim;
        public HeroGunEntity Gun;
        public AudioSource EnemyHititing;
        // Start is called before the first frame update
        void Start()
        {
            GunObject = GameObject.Find("Gun");
            GunAnim = GunObject.GetComponent<Animator>();
            Gun = GunObject.GetComponent<HeroGunEntity>();
            HeroLifeBar = GameObject.Find("HeroLifeBar");
            if (HeroLifeBar)
            {
                LifeBar = HeroLifeBar.GetComponent<Slider>();
                LifeBar.maxValue = 100f;
                LifeBar.value = HeroLife;
            }

            Bullet = gameObject.transform.Find("09");
            if (Bullet)
            {
                Bullet.position = FirePoint.transform.position;
                BulletRB = Bullet.gameObject.GetComponent<Rigidbody2D>();
            }
        }

        public void countEnemyandSetSliderValue(int enemyCount)
        {

        }

        private int createRandomNumber(int min, int max)
        {
            return 0;
        }


        void reload()
        {

        }

        public void idle()
        {
        }
        // IEnumerator fireCoroutine()
        // {
        //     if (FirePoint)
        //     {
        //         RaycastHit2D hit = Physics2D.Raycast(FirePoint.transform.position, FirePoint.transform.right);
        //         lineRenderer.enabled = true;
        //         // wait 
        //         yield return new WaitForSeconds(0.02f);
        //         lineRenderer.enabled = false;
        //         if (hit)
        //         {
        //             // Destroy(GameObject.Find(hit.collider.gameObject.name));
        //             lineRenderer.SetPosition(0, FirePoint.transform.position);
        //             lineRenderer.SetPosition(0, hit.transform.position);
        //         }
        //         else
        //         {
        //             lineRenderer.SetPosition(0, FirePoint.transform.position);
        //             lineRenderer.SetPosition(0, FirePoint.transform.right * 100);
        //         }
        //     }
        // }
        public void fire(string weaponType)
        {
            if (weaponType == "Primary" || weaponType == "Special")
            {
                Gun.FirePrimary();
            }
            else
            {
                Gun.Firing = true;
            }
        }
        public void FireStop()
        {
            Gun.Firing = false;
        }
        public void GetHit(float value)
        {
            HeroLife = GlobalVar.heroLife -= value;
            LifeBar.value = HeroLife;
            EnemyHititing.Play();
        }
        // Update is called once per frame
        void Update()
        {

        }
        void FixedUpdate()
        {
            if (GlobalVar.heroIsHitting)
            {
                HeroLife = GlobalVar.heroLife -= .2f;
                LifeBar.value = HeroLife;
            }
            if (GlobalVar.heroIsHittingByBoss)
            {
                HeroLife = GlobalVar.heroLife -= .02f;
                LifeBar.value = HeroLife;
            }
        }


    }
}