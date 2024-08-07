using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Experience_manager : MonoBehaviour
{
    #region variables

    [Header("XP")]
    [SerializeField] AnimationCurve experience;

    [Header("interface")]
    [SerializeField] Image experienceFill;

    [SerializeField] int currentLevel = 1;
    [SerializeField] int totalExperience;
    [SerializeField] int previousLevel;
    [SerializeField] int nextLevel;

    [Header("level up")]
    [SerializeField] Player player;
    [SerializeField] float levelUpDamage;
    [SerializeField] float levelUpSpeed;
    [SerializeField] float levelUpFirerate;

    #endregion
    #region base methods

    private void Start()
    {
        UpdateLevel();
    }

    private void Update()
    {

    }
    #endregion
    #region custom methods

    public void AddExperience(int amount)
    {
        totalExperience += amount;
        CheckForLevelUp();
        UpdateInterface();
    }

    private void CheckForLevelUp()
    {
        if(totalExperience >= nextLevel)
        {
            currentLevel++;
            UpdateLevel();

            player.Pdamage += levelUpDamage;
            player.Mvelocity += levelUpSpeed;
            player.fireRate += levelUpFirerate;
        }
    }

    private void UpdateLevel()
    {
        previousLevel = (int)experience.Evaluate(currentLevel);
        nextLevel = (int)experience.Evaluate(currentLevel + 1);
        UpdateInterface();
    }

    private void UpdateInterface()
    {
        int start = totalExperience - previousLevel;
        int end = nextLevel - previousLevel;

        experienceFill.fillAmount = (float)start / (float)end;
    }

    #endregion

    //public static Experience_manager Instance;

    //public delegate void ExperienceChangeHandler(int amount);
    //public event ExperienceChangeHandler OnExperienceChange;

    //public void AddExperience(int amount)
    //{
    //    OnExperienceChange?.Invoke(amount);
    //}
}
