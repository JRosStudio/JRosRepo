using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StaminaManagement : MonoBehaviour
{
    public Image staminaBar;
    public TMP_Text staminaNumber;

    public GameObject food1, food2, food3;
    [SerializeField]
    private int currentFood = 0;

    private float defaultMaxStamina = 10;
    [SerializeField]
    private float maxStamina = 10;

    [SerializeField]
    private float stamina;

    private float defaulthighJumpCost = 4;
    [SerializeField]
    private float highJumpCost = 4;

    private float defaultWallJumpCost = 1;
    [SerializeField]
    private float wallJumpCost = 1;

    //private float defaultRunCost = 5;
    //private float runCost = 5;

    private float defaultDashCost = 4f;
    [SerializeField]
    private float dashCost = 2f;

    private float defaultAttackCost = 1f;
    [SerializeField]
    private float attackCost = 1f;

    public void Start()
    {
        drawFoodGUI();
    }

    private void Update()
    {
        staminaBar.fillAmount = stamina / maxStamina;
        staminaNumber.text = stamina.ToString("0");
    }

    public void drawFoodGUI (){
        if (currentFood == 0)
        {
            food1.SetActive(false);
            food2.SetActive(false);
            food3.SetActive(false);
        }
        if (currentFood == 1)
        {
            food1.SetActive(true);
            food2.SetActive(false);
            food3.SetActive(false);
        }
        if (currentFood == 2)
        {
            food1.SetActive(true);
            food2.SetActive(true);
            food3.SetActive(false);
        }
        if (currentFood == 3)
        {
            food1.SetActive(true);
            food2.SetActive(true);
            food3.SetActive(true);
        }
    }

    public void CollectFood() {
        

        if (currentFood < 3) {
            

            currentFood++;
            //Debug.Log("CURRENT FOOD: " + currentFood);

            if (currentFood == 1) {
                food1.SetActive(true);
            }
            if (currentFood == 2)
            {
                food2.SetActive(true);
            }
            if (currentFood == 3)
            {
                food3.SetActive(true);
            }
        }
    }

    public void ConsumeFood() {

        if (currentFood > 0) {
            stamina = maxStamina;
            currentFood--;

            if (currentFood == 0)
            {
                food1.SetActive(false);

            }
            if (currentFood == 1)
            {
                food2.SetActive(false);

            }
            if (currentFood == 2)
            {
                food3.SetActive(false);
            }

        }  
       
    }

    public void DebugFullStamina() {
        stamina = maxStamina;
    }
    public int GetCurrentFood() {

        return currentFood;
    }

    public float HighJumpStaminaLoss(){
        stamina -= highJumpCost;
        if (stamina < 0) stamina = 0;
        
        
        return stamina;
    }

    public float DashStaminaLos() {
        stamina -= dashCost;
        if (stamina < 0) stamina = 0;
        return stamina;
    }

    
    public float WallJumpStaminaLoss(){
        stamina -= wallJumpCost;
        if (stamina < 0) stamina = 0;
        return stamina;
    } 
    
   /*
    public float RunningStaminaLoss(){
        stamina -= runCost * Time.deltaTime;
        if (stamina < 0) stamina = 0;
        //Debug.Log(stamina / maxStamina);
        staminaBar.fillAmount = stamina / maxStamina;
        staminaNumber.text = stamina.ToString("0.00");
        return stamina;
    }
   */

    public float GetCurrentStamina() {
        return stamina;
    }

    public void SetMaxStamina(float newMax) {
        maxStamina = newMax;
    }
    public void SetMaxStaminaToDefault() {
        maxStamina = defaultMaxStamina;
    }

    public float GetHighJumpStaminaCost() {
        return highJumpCost;
    } 
    
    public float GetDashStaminaCost() {
        return dashCost;
    } 
    
    public void SetHighJumpCost(float newCost)
    {
        highJumpCost = newCost;
    } 
    
    public void SetDashCost(float newCost)
    {
        dashCost = newCost;
    } 
    
    /*
    public void SetRunCost(float newCost)
    {
        runCost = newCost;
    } 
    */

    public void SetHighJumpCostToDefault()
    {
        highJumpCost = defaulthighJumpCost;
    }
    public void SetDashToDefault()
    {
        dashCost = defaultDashCost;
    }


    public float GetWallJumpStaminaCost()
    {
        return wallJumpCost;
    }

    public void SetWallJumpCost(float newCost)
    {
        wallJumpCost = newCost;
    }
    public void SetWallJumpCostToDefault()
    {
        wallJumpCost = defaultWallJumpCost;
    }

    public void SetAttackCost(float newCost)
    {
        attackCost = newCost;
    }
    public void SetAttackCostToDefault()
    {
        attackCost = defaultAttackCost;
    }
    public float GetAttackCost()
    {
        return attackCost;
    }

    public float AttackStaminaLoss() {
        stamina -= attackCost;
        if (stamina < 0) stamina = 0;
        return stamina;
    }

    /*
    public void SetRunCostToDefault()
    {
        runCost = defaultRunCost;
    }

    public float GetRunStaminaCost()
    {
        return runCost;
    }
    */
}
