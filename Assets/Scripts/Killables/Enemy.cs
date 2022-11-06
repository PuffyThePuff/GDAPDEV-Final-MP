using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public enum RockPaperScissors { rock, paper, scissors };

public class Enemy : Killable
{
    [SerializeField] private SwipeDirection swipeWeakness;
    public SwipeDirection SwipeWeakness
    {
        get { return swipeWeakness; }
    }
}
