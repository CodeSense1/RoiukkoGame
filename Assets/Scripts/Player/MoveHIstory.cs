using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveHistory
{

    private List<GameMove> History;
    // Start is called before the first frame update
    public MoveHistory()
    {
        History = new List<GameMove>();
    }


    public void UndoMove()
    {
        if (History.Count < 1)
            return;

        History[History.Count - 1].Undo();
        History.RemoveAt(History.Count - 1);
    }



    public void AddMove(Transform player, Transform pushable, Vector3 direction)
    {
        var move = new GameMove(player, pushable, direction);
        History.Add(move);
    }

    public void AddMove(Transform player, Vector3 direction)
    {
        History.Add(new GameMove(player, direction));
    }
}
