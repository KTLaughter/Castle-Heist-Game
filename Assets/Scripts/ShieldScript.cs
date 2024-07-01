using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldScript : MonoBehaviour
{
    public int life;
    private string label;


    //new

    public GameBehavior gameBehavior;

    void Start()
    {
        // Get the GameBehavior component from the GameObject
        gameBehavior = FindObjectOfType<GameBehavior>();
    }


   
    
    void test()
    {

        gameBehavior.labelText = "Destroy all of the generators to break the shield!";
        if (life <= 0)
        {
            Destroy(this.gameObject);
            gameBehavior.labelText = "The shield is down! Steal the crystal of power!";
        }

        if (life < 6 && life > 0)
        {
            gameBehavior.labelText = "Only " + life.ToString() + " more generators left to go!";
        }
    }
//end of new



    /* void test()
     {
         GameBehavior.labelText = "Destroy all of the generators to break the shield!";
         if (life <= 0)
         {
             Destroy(this.gameObject);
             GameBehavior.labelText = "The shield is down! Steal the crystal of power!";
         }

         if (life < 6 && life > 0)
         {
             GameBehavior.labelText = "Only " + life.ToString() + " more generators left to go!";
         }
     }*/

    void Update()
    {
        test();
    }

   
       
    
}