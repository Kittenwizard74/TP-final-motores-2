using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class UI : MonoBehaviour
{
    [SerializeField] Image healthBar;
    [SerializeField] Player player;
    float currentPHP;


    private void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    private void Update()
    {
        HandleHPBar();
    }

    protected virtual void HandleHPBar()
    {
        if (healthBar != null)
        {
            currentPHP = player.CurrentHP;
            healthBar.fillAmount = currentPHP / player.MaxHP;
        }
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void quit()
    {
        Application.Quit();
    }
}
