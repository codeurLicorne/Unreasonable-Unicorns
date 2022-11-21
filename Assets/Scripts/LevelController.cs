using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    [SerializeField] string nextLevelName;

    Monster[] monsters;

    void OnEnable()
    {
        monsters = FindObjectsOfType<Monster>();
    }

    void Update()
    {
        if (MonstersAreAllDead())
            NextLevel();

    }

    private void NextLevel()
    {
        Debug.Log("Go to Level " + nextLevelName);
        SceneManager.LoadScene(nextLevelName);
    }

    private bool MonstersAreAllDead()
    {
        foreach(var monster in monsters)
        {
            if (monster.gameObject.activeSelf)
                return false;
        }
        return true;
    }
}
