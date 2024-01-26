using System.Collections.Generic;

[System.Serializable]
public class GdStageInfo
{
    public int id;
    public int zoneId;
    public int questId;
    public string stageName;
    public string mailKey;
    public int difficultyType;
    public int retry;
    public int stageMode;
    public int completeDropSet;
    public string stageUi;
    public string stageUiPos;
    public string stageScenario;
}

[System.Serializable]
public class GdStageRoot
{
    public List<GdStageInfo> list;
}

[System.Serializable]
public class VersionRoot
{
    public List<VersionInfo> Version;
}

[System.Serializable]
public class VersionInfo
{
    public int id;
    public string col1;
    public string col2;
    public string col3;
    public string so;
    public string so2;
    public string dex;
    public string sign;
}