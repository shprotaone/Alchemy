using System;
using UnityEngine;

[Serializable]
public struct GuildIndicatorSet
{
    [SerializeField] public GuildsType _guild;
    [SerializeField] public CircularProgressBar _progressBar;
}
