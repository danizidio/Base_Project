using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using StateMachine;
using TMPro;

namespace HistoryTexts
{
    public class HistoryTextsBehaviour : MonoBehaviour
    {
        protected delegate void _onShowHistoryText(string s, Sprite charFace, imgPositions position, GamePlayStates gamePlayStates);
        protected static _onShowHistoryText OnShowHistoryText;

        [SerializeField] float _textSpeed;

        [SerializeField] SO_HistoryCollection _historyCol;

        [SerializeField] int _id;

        [SerializeField] GameObject _canvas;
        [SerializeField] TMP_Text _text;
        [SerializeField] Image _leftImage;
        [SerializeField] Image _rightImage;
        [SerializeField] List<HistoryTextsParam> _historyParams;

        HistoryTextsParam[] _historyArray;
        int _index = 0;
        bool _canChange;

        Coroutine _textRoutine;


        private void Start()
        {
            _historyArray = _historyParams.ToArray();

            _canvas.SetActive(false);

            this.gameObject.SetActive(_historyCol.CanShow(_id));

            _canvas.GetComponent<Canvas>().worldCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        }

        void Update()
        {
            if (Input.anyKeyDown && _canChange)
            {
                CallCanvas();
            }
        }

        void ShowHistory(string s, Sprite charFace, imgPositions position, GamePlayStates gamePlayStates)
        {
            if(_textRoutine != null)
            {
                StopCoroutine(DisplayText(null));
                _textRoutine = null;
            }

            _textRoutine = StartCoroutine(DisplayText(s));

            switch (position)
            {
                case imgPositions.LEFT:
                    {
                        _leftImage.gameObject.SetActive(true);
                        _rightImage.gameObject.SetActive(false);

                        _leftImage.sprite = charFace;

                        GetComponent<Animator>().SetTrigger("Change");

                        break;
                    }
                case imgPositions.RIGHT:
                    {
                        _rightImage.gameObject.SetActive(true);
                        _leftImage.gameObject.SetActive(false);

                        _rightImage.sprite = charFace;

                        GetComponent<Animator>().SetTrigger("Change");

                        break;
                    }
            }

            _index++;

            GameManager.OnNextGameState?.Invoke(gamePlayStates);
        }

        public void CanInteract()
        {
            _canChange = true;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            PlayerBehaviour p = collision.GetComponent<PlayerBehaviour>();

            if (p == null) return;

            GetComponent<Animator>().SetTrigger("Show");

            CallCanvas();
        }

        void CallCanvas()
        {
            try
            {
                OnShowHistoryText?.Invoke(_historyArray[_index].text, _historyArray[_index].chracterImage, _historyArray[_index].imgPos, GamePlayStates.HISTORY);
            }
            catch
            {
                _historyCol.ChangeInfo(_id);
                GetComponent<Animator>().SetTrigger("End");
                GetComponent<BoxCollider2D>().enabled = false;
                GameManager.OnNextGameState?.Invoke(GamePlayStates.GAMEPLAY);
            }
        }

        IEnumerator DisplayText(string line)
        {
            _text.text = "";

            _canChange = false;

            foreach (var item in line.ToCharArray())
            {
                //if (Input.anyKeyDown)
                //{
                //    _text.text = line;

                //    break;
                //}

                _text.text += item;

                yield return new WaitForSecondsRealtime(_textSpeed);
            }

            _canChange = true;
        }

        private void OnEnable()
        {
            OnShowHistoryText = ShowHistory;
        }

        private void OnDisable()
        {
            OnShowHistoryText -= ShowHistory;
        }
    }
}
