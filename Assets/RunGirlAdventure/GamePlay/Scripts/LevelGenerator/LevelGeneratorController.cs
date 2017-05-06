using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneratorController : MonoBehaviour {

    [SerializeField] private int levelLenght;
    [SerializeField] private int startPlatformLength = 5, endPlatformLength = 5;
    [SerializeField] private int distance_between_platforms;
    [SerializeField] private Transform platformPrefab, platform_parent;
    [SerializeField] private Transform monster, moster_parent;
    [SerializeField] private Transform health_collectable, healthCollectable_parent;
    [SerializeField] private float platformPosition_MinY = 0f, platfromPosition_MaxY = 10f;
    [SerializeField] private int platformLength_Min = 1, platformLength_Max = 4;
    [SerializeField] private float chanceForMonsterExistence = .25f, chanceForCollectableExistence = .1f;
    [SerializeField] private float healthCollectable_MinY = 1f, healthCollectable_MaxY = 3f;
    [SerializeField] private float platfromLastPositionX;



    private void Start()
    {
        GenerateLevel();
    }

    private enum PlatfromType
    {
        None,
        Flat
    }

    void FillOutPositionInfo(PlatformPositionInfo[] platformInfo)
    {
        int currentPlatformInfoIndex = 0;
        for (int i = 0; i < startPlatformLength; i++)
        {
            platformInfo[currentPlatformInfoIndex].platformType = PlatfromType.Flat;
            platformInfo[currentPlatformInfoIndex].positionY = 0f;

            currentPlatformInfoIndex++;
        }

        while(currentPlatformInfoIndex < levelLenght - endPlatformLength)
        {
            if (platformInfo[currentPlatformInfoIndex - 1].platformType != PlatfromType.None)
            {
                currentPlatformInfoIndex++;
                continue;
            }

            float platformPositionY = Random.Range(platformPosition_MinY, platfromPosition_MaxY);
            float platformLength = Random.Range(platformLength_Min, platformLength_Max);

            for (int i = 0; i < platformLength; i++)
            {
                bool has_monster = (Random.Range(0f,1f) < chanceForMonsterExistence);
                bool has_healthCollectable = (Random.Range(0f, 1f) < chanceForCollectableExistence);

                platformInfo[currentPlatformInfoIndex].platformType = PlatfromType.Flat;
                platformInfo[currentPlatformInfoIndex].positionY = platformPositionY;
                platformInfo[currentPlatformInfoIndex].hasMonster = has_monster;
                platformInfo[currentPlatformInfoIndex].hasHealthCollectable = has_healthCollectable;

                currentPlatformInfoIndex++;

                if (currentPlatformInfoIndex > (levelLenght - endPlatformLength))
                {
                    currentPlatformInfoIndex = levelLenght - endPlatformLength;
                    break;
                }
            }

            for (int i = 0; i < endPlatformLength; i++)
            {
                platformInfo[currentPlatformInfoIndex].platformType = PlatfromType.Flat;
                platformInfo[currentPlatformInfoIndex].positionY = 0f;

                currentPlatformInfoIndex++;
            }

        } // while loop
    }


    void CreatePlatformFromPositionInfo(PlatformPositionInfo[] platformPositionInfo)
    {
        for (int i = 0; i < platformPositionInfo.Length; i++)
        {
            PlatformPositionInfo positionInfo = platformPositionInfo[i];

            if (positionInfo.platformType == PlatfromType.None)
            {
                continue;
            }

            Vector3 platformPosition;
            // here we are going to check if the game is started or not
            platformPosition = new Vector3(distance_between_platforms * i, positionInfo.positionY, 0);

            // save the platform position x for later use

            Transform createBlock = Instantiate(platformPrefab, platformPosition, Quaternion.identity) as Transform;
            createBlock.parent = platform_parent;


            if (positionInfo.hasMonster)
            {
                // code later
            }
            if (positionInfo.hasHealthCollectable)
            {
                // code later
            }


        } // for loop
    }

    #region Inner class GenerateLevel
    public void GenerateLevel()
    {
        PlatformPositionInfo[] platformInfo = new PlatformPositionInfo[levelLenght];
        for (int i = 0; i < platformInfo.Length; i++)
        {
            platformInfo[i] = new PlatformPositionInfo(PlatfromType.None, -1f, false, false);
        }

        FillOutPositionInfo(platformInfo);
        CreatePlatformFromPositionInfo(platformInfo);
    }
    #endregion

    #region Inner class PlatformPositionInfo
    private class PlatformPositionInfo
    {
        public PlatfromType platformType;
        public float positionY;
        public bool hasMonster;
        public bool hasHealthCollectable;

        public PlatformPositionInfo(PlatfromType _type, float posY, bool has_monster, bool has_helth_collectable)
        {
            platformType = _type;
            positionY = posY;
            hasMonster = has_monster;
            hasHealthCollectable = has_helth_collectable;
        }
    }
    #endregion
}
