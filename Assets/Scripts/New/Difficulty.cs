using System.Collections.Generic;
using UnityEngine;

public class Difficulty : MonoBehaviour
{
    private struct DifficultyInfo
    {
        public float playerSpeed;
        public float minPlatformWidth;
        public float maxPlatformWidth;
    }

    public float switchDifficultyTime;
    
    private List<DifficultyInfo> difficulties = new List<DifficultyInfo>();
    private LevelCreator levelCreator;
    private Movement playerMovement;
    private float difficultyTimeCounter = 0f;
    private int difficultyIndex = -1;

    private void Awake()
    {
        levelCreator = GameObject.Find("LevelCreator").GetComponent<LevelCreator>();
        playerMovement = GameObject.Find("Player").GetComponent<Movement>();
        InitDifficulties();
    }

    private void Update()
    {
        difficultyTimeCounter += Time.deltaTime;
        if (difficultyTimeCounter >= switchDifficultyTime
            && difficultyIndex < difficulties.Count - 1)
        {
            SwitchDifficulty();
            difficultyTimeCounter = 0f;
        }
    }

    private void Start()
    {
        SwitchDifficulty();
    }

    public float GetMaxPlatformWidthAll()
    {
        float max = 0f;
        foreach (var d in difficulties)
        {
            if (d.maxPlatformWidth > max)
                max = d.maxPlatformWidth;
        }
        return max;
    }

    public float GetSpeed()
    {
        return difficulties[difficultyIndex].playerSpeed;
    }

    public float GetMinPlatformWidth()
    {
        return difficulties[difficultyIndex].minPlatformWidth;
    }

    public float GetMaxPlatformWidth()
    {
        return difficulties[difficultyIndex].maxPlatformWidth;
    }

    private void InitDifficulties()
    {
        AddDifficulty(3f, 6f, 9f);
        AddDifficulty(4f, 6f, 8f);
        AddDifficulty(5f, 5f, 8f);
        AddDifficulty(6f, 5f, 7f);
        AddDifficulty(7f, 4f, 7f);
        AddDifficulty(8f, 3f, 7f);
        AddDifficulty(9f, 3f, 6f);
        AddDifficulty(10f, 2.5f, 5f);
    }

    private void AddDifficulty(
        float playerSpeed,
        float minPlatformWidth,
        float maxPlatformWidth)
    {
        difficulties.Add(new DifficultyInfo
        {
            playerSpeed = playerSpeed,
            minPlatformWidth = minPlatformWidth,
            maxPlatformWidth = maxPlatformWidth
        });
    }
    
    private void SwitchDifficulty()
    {
        difficultyIndex++;
        playerMovement.DifficultySwitched(difficulties[difficultyIndex].playerSpeed);
        levelCreator.DifficultySwitched(
            difficulties[difficultyIndex].minPlatformWidth,
            difficulties[difficultyIndex].maxPlatformWidth
            );
    }
}
