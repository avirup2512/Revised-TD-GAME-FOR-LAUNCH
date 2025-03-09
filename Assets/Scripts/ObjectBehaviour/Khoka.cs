// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using Assets.Script.globalVar;

// public class Khoka : MonoBehaviour
// {
//     public Vector2 destination;
//     private Rigidbody2D rb;
//     public float limit;
//     public Vector3 startPos;
//     public bool isInstantiated = false;

//     public bool goFlag = false;
//     public Bullet bulletComponent;
//     public Animator anim;
//     public GlobalData globalVar;
//     // Start is called before the first frame update
//     void Start()
//     {
//         rb = gameObject.GetComponent<Rigidbody2D>();
//         startPos = new Vector3(transform.position.x, transform.position.y, 0);
//         gameObject.SetActive(false);
//         // anim = gameObject.GetComponent<Animator>();  
//     }

//     public void go()
//     {
//         if (!isInstantiated && goFlag)
//         {
//             rb.velocity = new Vector2(destination.x, destination.y) * 8;
//         }
//     }

//     void OnTriggerEnter2D(Collider2D enemy)
//     {
//         if (enemy.gameObject.tag == "enemy")
//         {
//             globalVar.firstEnemy = "enemy";
//         }
//     }
//     // void OnTriggerEnter2D(Collider2D Obj){
//     // 	if(Obj.gameObject.tag == "enemy"){
//     // 		
//     // 		rb.velocity = new Vector2(0,0);
//     //     	// anim.SetBool("blast",true);
//     //     	gameObject.transform.position = startPos;
//     //     	gameObject.SetActive(false);
//     //     	isInstantiated = false;
//     //     	globalVar.currentIndex++;
//     //     	globalVar.currentBulletIndex--;
//     //     	if(globalVar.currentIndex == globalVar.bulletlimit || globalVar.currentBulletIndex == 0){
//     //     		globalVar.currentIndex = 0;
//     //     		globalVar.isReloading = true;
//     //     	}
//     // 	}
//     // }

//     // Update is called once per frame
//     void Update()
//     {
//         if (transform.position.x > limit)
//         {
//             globalVar.firstEnemy = null;
//             rb.velocity = new Vector2(0, 0);
//             // anim.SetBool("blast",true);
//             gameObject.transform.position = startPos;
//             gameObject.SetActive(false);
//             isInstantiated = false;
//             globalVar.currentIndex++;
//             globalVar.currentBulletIndex--;

//             if (globalVar.currentIndex == globalVar.bulletlimit || globalVar.currentBulletIndex == 0)
//             {
//                 globalVar.currentIndex = 0;
//                 if (globalVar.totalMagaZine > 0)
//                 {
//                     globalVar.totalMagaZine = globalVar.totalMagaZine - 1;
//                 }

//                 globalVar.isReloading = true;
//             }
//         }

//     }
// }
