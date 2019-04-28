using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	/// <summary>
	/// Make it a singleton. 单例模式
	/// </summary>

	private static GameManager instance;
	private DataBase weaponDB;
	private WeaponFactory weaponFact;

	public WeaponManager testwm;

	void Awake ()
	{
		CheckGameObject();
		CheckSingle();
	}

	void Start()
	{
		InitWeaponDB();
		InitWeaponFactory();

		testwm.UpdataWeaponCollider("R", weaponFact.CreateWeapon("Mace", "R", testwm));
	}
	
	void OnGUI()
	{

		if (GUI.Button(new Rect(10,10,150,30) , "R:Mace"))
		{
			Collider col = weaponFact.CreateWeapon("Mace", "R", testwm);
			testwm.UpdataWeaponCollider("R", col);
		}
		if (GUI.Button(new Rect(10, 50, 150, 30), "R:Falchion"))
		{
			Collider col = weaponFact.CreateWeapon("Falchion", "R", testwm);
			testwm.UpdataWeaponCollider("R", col);
		}
		if (GUI.Button(new Rect(10, 90, 150, 30), "R:Sword"))
		{
			Collider col = weaponFact.CreateWeapon("Sword", "R", testwm);
			testwm.UpdataWeaponCollider("R", col);
		}
		if (GUI.Button(new Rect(10, 130, 150, 30), "R: Clear all weapons"))
		{
			testwm.UnLoadWeapon("R");
		}
	}



	/// 
	/// 
	/// 


	private void CheckGameObject()
	{
		if (tag == "GM")
		{
			return;
		}
		Destroy(this);
	}

	private void CheckSingle()
	{
		if (instance == null)
		{
			instance = this;
			DontDestroyOnLoad(gameObject); 
			return;
		}
		Destroy(this);
	}

	private void InitWeaponDB()
	{
		weaponDB = new DataBase();
	}

	private void InitWeaponFactory()
	{
		weaponFact = new WeaponFactory(weaponDB);
	}
}
