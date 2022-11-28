using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private List<Killable> killables = new List<Killable>();

    public void AddToList(Killable killable)
    {
        killables.Add(killable);
        CheckIfWon();
    }

    public void RemoveFromList(Killable killable)
    {
        if (killables.Contains(killable))
        {
            killables.Remove(killable);
        }
        CheckIfWon();
    }
    private bool CheckIfWon()
    {
        if (killables.Count < 0) return false;

        GameOverManager.Instance.OnGameOver(GameOverState.win);

        return true;
    }
}
