using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePadManager : MonoBehaviour {

    public static GamePadManager instance;
    public Dictionary<int, int> gamePadAssoc;

	// Use this for initialization
	void Start ()
    {
        instance = this;
        gamePadAssoc = new Dictionary<int, int>();
        DontDestroyOnLoad(gameObject);
	}
	
    public bool Bind(int playerIndex, int padIndex)
    {
        if (!gamePadAssoc.ContainsValue(padIndex) && !gamePadAssoc.ContainsKey(playerIndex))
        {
            gamePadAssoc.Add(playerIndex, padIndex);
            return true;
        }

        return false;
    }

    public void Clear()
    {
        gamePadAssoc.Clear();
    }
}
