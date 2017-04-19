using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : TungDoesMathForYou
{
    /*Settings*/
    public GameObject AmmoSpawnSpot;
    public GameObject AmmoPrefab;
    //Total ammo stacked
    public int MaxAmmo = 5;
    public float ReloadDelay = 1; //Speed of reload

    public Transform BulletSpawnLoc;
    public GameObject shellPrefab;

    /*Data*/
    private int Ammo = 0; //Current ammo count
    private float Timer = 0;
    private bool Loaded = false; //Is the gun loaded?
    private bool Reload = false;
    private bool Toggle = false; //Prevents spam reload
    private int CurrentReloadAmmo = 0;
    private bool Cocked = false;


    /*Required components*/
    private ManagerTracker Tracker;

    void Start ()
    {
        Tracker = FindObjectOfType<ManagerTracker>();
    }
	
	void Update ()
    {
        if (Ammo <= 0 && !Toggle)
        {
            Ammo = MaxAmmo;
            Reload = true;
            Toggle = true;
            CurrentReloadAmmo = 0;
            Timer = 0;
        }

        if(Reload)
        {
            Timer += Time.deltaTime;
            if(Timer >= ReloadDelay)
            {
                //Spawn new bullet to feed
                GameObject spawnedAmmo = Instantiate(AmmoPrefab, AmmoSpawnSpot.transform.position, transform.rotation);
                CurrentReloadAmmo += 1;
                Timer = 0;
            }

            if(CurrentReloadAmmo >= MaxAmmo)
            {
                Reload = false;
                Toggle = false;
                CurrentReloadAmmo = 0;
            }
        }

        //DELETE THIS LATER BRAIDAMN
        if(Input.GetKeyDown(KeyCode.V))
        {
            Cocked = true;
            Tracker.IsFullyReady = true;
            BulletUpdate(AmmoPrefab);
            Fire();
        }
    }

    public void BulletUpdate(GameObject other)
    {
        if (Ammo <= 0 || Loaded) return;
        Ammo -= 1;
        Destroy(other);
        Loaded = true;
    }

    //Fire projectile through server
    public void Fire()
    {
        if(Cocked && Tracker.IsFullyReady)
        {
            //Braidamn was here
            Tracker.ThePlayerControlPanel.TheCommandManager.CmdFire( BulletSpawnLoc.position, BulletSpawnLoc.rotation, Tracker.ThePlayerControlPanel.ID);
            Loaded = false;
            Cocked = false;
        }
    }

    //Load the gun using crank/lever
    public void CockGun()
    {
        Cocked = true;
    }
}
