using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaManagement : MonoBehaviour
{
    public Image staminaBar;
    private float defaultMaxStamina = 100;
    private float maxStamina = 100;
    private float stamina = 100;

    private float defaulthighJumpCost = 10;
    private float highJumpCost = 10;

    private float defaultWallJumpCost = 5;
    private float wallJumpCost = 5;
    
    private float defaultRunCost = 5;
    private float runCost = 5;


    public float HighJumpStaminaLoss(){
        stamina -= highJumpCost;
        if (stamina < 0) stamina = 0;
        staminaBar.fillAmount = stamina / maxStamina;
        return stamina;
    } 
    
    public float WallJumpStaminaLoss(){
        stamina -= wallJumpCost;
        if (stamina < 0) stamina = 0;
        Debug.Log(stamina / maxStamina);
        staminaBar.fillAmount = stamina / maxStamina;
        return stamina;
    } 
    
    public float RunningStaminaLoss(){
        stamina -= runCost * Time.deltaTime;
        if (stamina < 0) stamina = 0;
        Debug.Log(stamina / maxStamina);
        staminaBar.fillAmount = stamina / maxStamina;
        return stamina;
    }

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
    
    public void SetHighJumpCost(float newCost)
    {
        highJumpCost = newCost;
    } 
    
    public void SetRunCost(float newCost)
    {
        runCost = newCost;
    } 
    public void SetHighJumpCostToDefault()
    {
        highJumpCost = defaulthighJumpCost;
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

    public void SetRunCostToDefault()
    {
        runCost = defaultRunCost;
    }

    public float GetRunStaminaCost()
    {
        return runCost;
    }

}
