using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveHistory
{

    private List<GameMove> history;
    // Start is called before the first frame update
    public MoveHistory()
    {
        history = new List<GameMove>();
    }


    public void UndoMove()
    {
        if (history.Count < 1)
            return;

        history[history.Count - 1].Undo();
        history.RemoveAt(history.Count - 1);
    }

    public void Reset()
    {
        while(history.Count > 0)
        {
            UndoMove();
        }
    }

    public void AddMove(Transform player, Transform pushable, Vector3 direction)
    {
        var move = new GameMove(player, pushable, direction);
        history.Add(move);
    }

    public void AddMove(Transform player, Vector3 direction)
    {
        history.Add(new GameMove(player, direction));
    }
}
