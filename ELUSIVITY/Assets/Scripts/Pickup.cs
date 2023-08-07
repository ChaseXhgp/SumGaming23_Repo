using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PickupAbility
{
    //List all potential powerups.
    FreezeMonster,
    BoostSpeed,
    BoostFlashlight,
}

public class Pickup : MonoBehaviour
{

    public PickupAbility pickupAbility; //The type of pickup this is

    private void OnDestroy()
    {
        //When the pickup is collected

    }
}
