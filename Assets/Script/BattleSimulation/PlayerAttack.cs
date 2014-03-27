﻿/// <summary>
/// Player attack.cs
/// Author: Harris Kevin
/// </summary>
using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour {
	StoredInformation stored;
	public GameObject[] enemyObjects;
	public GameObject EnemyTarget;
	public bool attempt;
	Transform myTransform;
	public int funTimes;
	string attackType;
	GameObject giggles;
	public bool magicCheck = false;
	
	public GameObject[] particles =new GameObject[3];

	void Awake(){
		//constVar = GameObject.FindGameObjectsWithTag("Enemy2");
		//player = GameObject.FindGameObjectWithTag("Player2");
		myTransform = transform;
		attempt = false;
		
		giggles = new GameObject();
	}

	// Use this for initialization
	public void Start () {


		myTransform = GameObject.FindGameObjectWithTag("Player2").transform;
	
		enemyObjects = GameObject.FindGameObjectsWithTag("Enemy2");
		//EnemyTarget = enemyObjects[0];

	}
	
	// Update is called once per frame
	void Update () {
		
	
		enemyObjects = GameObject.FindGameObjectsWithTag("Enemy2");

		if(enemyObjects.Length!=0){
			GameObject constVar= GameObject.FindGameObjectWithTag("Constant");
			StoredInformation stored = constVar.GetComponent<StoredInformation>();
			int counter = stored.BattlePosition;

			PlayerAttack pl;
			GameObject[] obj = GameObject.FindGameObjectsWithTag("Player2");
			pl = obj[counter].GetComponent<PlayerAttack>();
			
			enemyObjects = GameObject.FindGameObjectsWithTag("Enemy2");
			
			BattleAttackDisplay bat =  obj[counter].GetComponent<BattleAttackDisplay>();
			funTimes = bat.EnemyNumber;
			EnemyTarget = enemyObjects[funTimes];

			if(EnemyTarget==null){
				EnemyTarget = enemyObjects[funTimes+1];
			}

			if(myTransform ==null ){
				myTransform = transform;
			}

			if(attempt){
				Quaternion rot;
				
				rot = Quaternion.Slerp (myTransform.rotation, Quaternion.LookRotation (EnemyTarget.transform.position - myTransform.position), 6 * Time.deltaTime);
				myTransform.rotation = rot;
				myTransform = GameObject.FindGameObjectWithTag("Player2").transform;
				myTransform.position+= new Vector3 (myTransform.forward.x * 12 * Time.deltaTime, 
				                                    0, 
				                                    myTransform.forward.z * 12 * Time.deltaTime);
				animation.Play("run");

			}
			if(Vector3.Distance(myTransform.position,EnemyTarget.transform.position)<=1){
				
				EnemyTarget = enemyObjects[funTimes];

				EnemyHealth attackEnemy = EnemyTarget.GetComponent<EnemyHealth>();
				attackType = bat.attackType;
				if(attackType == "Attack"){
					attackEnemy.AddjustCurrentHealth(stored._attackValue[0],"minus");
				}
				if(attackType == "FireAttack"){

					giggles = Instantiate(particles[0],EnemyTarget.transform.position, Quaternion.identity) as GameObject;
					giggles.name = "Fire";

					attackEnemy.AddjustCurrentHealth(stored._attackValue[1],"minus");
				}
				if(attackType == "IceAttack"){
					attackEnemy.AddjustCurrentHealth(stored._attackValue[2],"minus");
					giggles = Instantiate(particles[1],EnemyTarget.transform.position, Quaternion.identity) as GameObject;
					giggles.name = "Ice";
				}
				if(attackType == "LightningAttack"){
					giggles = Instantiate(particles[2],EnemyTarget.transform.position, Quaternion.identity) as GameObject;
					giggles.name = "Lightning";
					attackEnemy.AddjustCurrentHealth(stored._attackValue[3],"minus");

				}

				pl.attempt = false;

				GameObject[] atemptingChange = GameObject.FindGameObjectsWithTag("PlayerBattle");
				obj[counter].transform.position = atemptingChange[counter].transform.position;
				
				BattleTimeScript batTimeScript = obj[counter].GetComponent<BattleTimeScript>();
				batTimeScript.timeToAttack1=0;

				animation.Play("idle");

			}

		}
	}
	
	public void retrieveEnemies(int fight){
		funTimes = fight;
	}

	public void AttackEnemy(){
		myTransform = GameObject.FindGameObjectWithTag("Player2").transform;
		PlayerAttack pl;
		GameObject obj = GameObject.FindGameObjectWithTag("Player2");

		EnemyTarget = enemyObjects[funTimes];
		Transform enemyTransform = EnemyTarget.transform;
		pl = obj.GetComponent<PlayerAttack>();

		if(Vector3.Distance(myTransform.position,enemyTransform.position)>1){

			myTransform.position+= new Vector3 (myTransform.forward.x * 12 * Time.deltaTime,
			                                    0,
			                                    myTransform.forward.z * 12 * Time.deltaTime);
			pl.attempt =true;

		}
	}

	public void FireAttackEnemy(){
		myTransform = GameObject.FindGameObjectWithTag("Player2").transform;
		PlayerAttack pl;
		GameObject obj = GameObject.FindGameObjectWithTag("Player2");
		
		EnemyTarget = enemyObjects[funTimes];

		pl = obj.GetComponent<PlayerAttack>();

		if(Vector3.Distance(myTransform.position,EnemyTarget.transform.position)>1){
			
			myTransform.position+= new Vector3 (myTransform.forward.x * 12 * Time.deltaTime,
			                                    0,
			                                    myTransform.forward.z * 12 * Time.deltaTime);
			pl.attempt =true;
			
		}
	}

	public void IceAttackEnemy(){
		myTransform = GameObject.FindGameObjectWithTag("Player2").transform;
		PlayerAttack pl;
		GameObject obj = GameObject.FindGameObjectWithTag("Player2");
		
		EnemyTarget = enemyObjects[funTimes];

		pl = obj.GetComponent<PlayerAttack>();

		if(Vector3.Distance(myTransform.position,EnemyTarget.transform.position)>1){
			
			myTransform.position+= new Vector3 (myTransform.forward.x * 12 * Time.deltaTime, 
			                                    0,
			                                    myTransform.forward.z * 12 * Time.deltaTime);
			pl.attempt =true;
			
		}
	}

	public void LightningAttackEnemy(){
		myTransform = GameObject.FindGameObjectWithTag("Player2").transform;
		PlayerAttack pl;
		GameObject obj = GameObject.FindGameObjectWithTag("Player2");
		
		EnemyTarget = enemyObjects[funTimes];
		
		pl = obj.GetComponent<PlayerAttack>();

		if(Vector3.Distance(myTransform.position,EnemyTarget.transform.position)>1){
			
			myTransform.position+= new Vector3 (myTransform.forward.x * 12 * Time.deltaTime,
			                                    0,
			                                    myTransform.forward.z * 12 * Time.deltaTime);
			pl.attempt =true;
			
		}
	}
}
