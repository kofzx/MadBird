using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    private static int _nextLevelIndex = 1;     // 下一关索引
    private Enemy[] _enemies;

    private void OnEnable()
    {
        _enemies = FindObjectsOfType<Enemy>();
    }

    private void Update()
    {
        // 遍历所有敌人，存在即未过关
        foreach(Enemy enemy in _enemies)
        {
            if (enemy != null)
            {
                return;
            }
        }

        Debug.Log("You killed all enemies");

        _nextLevelIndex++;
        string nextLevelName = "Level" + _nextLevelIndex;
        SceneManager.LoadScene(nextLevelName);
    }
}
