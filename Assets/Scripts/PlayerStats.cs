using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerStats {

    public static int playerHealth;

    public static int playerScore;

    public static Weapon[] weaponList;

    public static int currency;

    public static void reset()
    {
        weaponList = new Weapon[2];
        playerHealth = 100;
        playerScore = 0;
        currency = 0;
    }


}
