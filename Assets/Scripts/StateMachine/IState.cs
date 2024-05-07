using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    void OnEnter(BotAi botai);
    void OnExcute(BotAi botai);
    void OnExit(BotAi botai);
}
