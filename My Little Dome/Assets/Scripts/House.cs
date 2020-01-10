﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{
    public int woodAmount;

    [SerializeField] private GameObject[] woodpile;

    private float isPressingTime = 0.5f;
    private float currentIsPressingTime;
    private bool isPressing = false;


    void Start()
    {
        currentIsPressingTime = isPressingTime;

        for (int i = 0; i < woodpile.Length; i++)
        {
            woodpile[i].SetActive(false);
        }

        for (int i = 0; i < woodAmount; i++)
        {
            woodpile[i].SetActive(true);
        }
    }

    void Update()
    {
        if (isPressing)
        {
            currentIsPressingTime = Timer(currentIsPressingTime);

            if(currentIsPressingTime <= 0)
            {
                isPressing = false;
                currentIsPressingTime = isPressingTime;
            }
        }
    }

    private void OnTriggerStay(Collider collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            if (woodAmount > 0 && woodAmount < woodpile.Length)
            {
                if (Input.GetKeyDown(KeyCode.Q) && !isPressing)
                {
                    isPressing = true;
                    UpdateWood(-1, false);
                }
            }
            else
            {
                //UI implemtatie (dat je geen hout hebt)
            }
        }
    }

    public void UpdateWood(int amount, bool active)
    {
        if(woodAmount < woodpile.Length)
        {
            woodAmount += amount;
            if (amount > 0)
            {
                woodpile[woodAmount - 1].SetActive(active);
            }
            else
            {
                woodpile[woodAmount].SetActive(active);
            }
        }
    }

    private float Timer(float timer)
    {
        timer -= Time.deltaTime;
        return timer;
    }

    #region Singleton
    private static House instance;

    private void Awake()
    {
        instance = this;
    }

    public static House Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new House();
            }

            return instance;
        }
    }
    #endregion
}