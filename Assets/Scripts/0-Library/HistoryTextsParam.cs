using UnityEngine;
using System;
using UnityEngine.UI;

namespace HistoryTexts
{
    public enum imgPositions
    {
        LEFT
        ,RIGHT
        //BOTH,
        //NONE
    }

    [Serializable]
    public class HistoryTextsParam
    {
        [SerializeField] Sprite _characterImage;
        public Sprite chracterImage { get { return _characterImage;} }

        [SerializeField] imgPositions _imgPos;
        public imgPositions imgPos { get { return _imgPos; } }

        [SerializeField] [Multiline]  string _text;
        public string text { get { return _text; } }
    }
}
