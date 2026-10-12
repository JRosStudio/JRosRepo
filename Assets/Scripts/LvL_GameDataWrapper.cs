using System;
using System.Collections.Generic;

[Serializable]
public class LvL_GameDataWrapper
{
    public List<LvL_Score> lvlGameData;

    public LvL_GameDataWrapper(List<LvL_Score> data)
    {
        lvlGameData = data;
    }
}