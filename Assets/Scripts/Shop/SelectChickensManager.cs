using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectChickensManager : MonoBehaviour
{
    public Button[] ChickenButtons;
    public GameObject[] ChickenCubes;
    private string[] ChickenNames = { "Chick1", "Chick2", "Chick3", "Chick4" };

    private void Start()
    {
        for (int i = 0; i < ChickenButtons.Length; i++)
        {
            int index = i;
            ChickenButtons[i].onClick.AddListener(() => SelectChicken(index));
        }
    }

    public void SelectChicken(int selectedHelmetIndex)
    {
        for (int i = 0; i < ChickenButtons.Length; i++)
        {
            PlayerPrefs.SetInt(ChickenNames[i] + "Selected", i == selectedHelmetIndex ? 1 : 0);
            ChickenButtons[i].interactable = i != selectedHelmetIndex;
            ChickenCubes[i].SetActive(i != selectedHelmetIndex);
        }
    }
}