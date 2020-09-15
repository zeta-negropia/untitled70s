using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Phase{
    INIT,
    WAITING,
    PLAYERTURN,
    ENEMYTURN,
    ACTION,
    VICTORY,
    DEFEAT,

}

public class battleSystem : MonoBehaviour
{

    public int Attack (Transform Attacker, Transform Defender)
    {
        // Attack has Base Damage of 15
        int Accuracy = Attacker.GetComponent<Unit>().agility;



        int Evasion = Defender.GetComponent<Unit>().agility;
        int AttackAccuracy = Accuracy + Random.Range (0, 25);
        int AttackEvasion = Evasion + Random.Range (0, 25);
        int AttackResult = AttackAccuracy - AttackEvasion;

        if (AttackResult < 0)
        {
            return 1;
        }

        if (AttackResult > 0)
        {
            return 2;
        }

        return 0;
    }

    public int Damage (Transform Attacker, Transform Defender, int BaseDamage)
    {
        int attack = 0;
        int defense = 0;

        attack = Attacker.GetComponent<Unit>().offense;
        defense = Defender.GetComponent<Unit>().defense;

        int AttackDamage = attack + BaseDamage + Random.Range (0, 20);
        int AttackDefense = defense + Random.Range (0, 20);

        int DamageDealt = AttackDamage - AttackDefense;

        if (DamageDealt < 1)
        {
            return 0;
        }

        if (DamageDealt > 0)
        {
            return DamageDealt;
        }

        return 0;
    }
    public Phase Phase;
    // Start is called before the first frame update
    void Start()
    {
        Phase = Phase.INIT;
    }

    
    
}
