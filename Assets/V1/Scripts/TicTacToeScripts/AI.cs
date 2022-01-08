using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AI {

    List<int> choice = new List<int>();
    string[] TabAI = new string[9];

    public int BestPosition(string difficulty)
    {
        // Recovery of the table
        string[] source = GameObject.Find("GameManagerTicTacToe").GetComponent<GameManagerTicTacToe>().Tab;
        Array.Copy(source, TabAI, source.Length);

        choice.Clear();
        for (int i = 0; i < TabAI.Length; i++)
        {
            if (TabAI[i] == string.Empty) choice.Add(i);
        }

        switch (difficulty)
        {
            case "Hard":
                // Winning shot
                for (int i = 0; i < TabAI.Length; i++)
                {
                    if (TabAI[i] == String.Empty)
                    {
                        TabAI[i] = "O";
                        if (isWinner("O"))
                        {
                            return i;
                        }
                        TabAI[i] = string.Empty;
                    }
                }

                // Defense
                for (int i = 0; i < TabAI.Length; i++)
                {
                    if (TabAI[i] == String.Empty)
                    {
                        TabAI[i] = "X";
                        if (isWinner("X"))
                        {
                            return i;
                        }
                        TabAI[i] = string.Empty;
                    }
                }

                // Random
                return choice[UnityEngine.Random.Range(0, choice.Count)];
            case "Medium":
                // Defense
                for (int i = 0; i < TabAI.Length; i++)
                {
                    if (TabAI[i] == String.Empty)
                    {
                        TabAI[i] = "X";
                        if (isWinner("X"))
                        {
                            return i;
                        }
                        TabAI[i] = string.Empty;
                    }
                }

                //Random
                return choice[UnityEngine.Random.Range(0, choice.Count)];
            case "Easy":
                // Random
                return choice[UnityEngine.Random.Range(0, choice.Count)];
            default:
                // Random
                return choice[UnityEngine.Random.Range(0, choice.Count)];
        }
    }

    // Check if there is a winner
    bool isWinner(string p)
    {
        if ( TabAI[0] == p && TabAI[1] == p && TabAI[2] == p ||
             TabAI[3] == p && TabAI[4] == p && TabAI[5] == p ||
             TabAI[6] == p && TabAI[7] == p && TabAI[8] == p ||
             TabAI[0] == p && TabAI[3] == p && TabAI[6] == p ||
             TabAI[1] == p && TabAI[4] == p && TabAI[7] == p ||
             TabAI[2] == p && TabAI[5] == p && TabAI[8] == p ||
             TabAI[0] == p && TabAI[4] == p && TabAI[8] == p ||
             TabAI[2] == p && TabAI[4] == p && TabAI[6] == p)
        {
            return true;
        }
        return false;
    }
}
