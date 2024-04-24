using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    private const int MIN_CLEARED_LINES = 1;
    private const int MAX_CLEARED_LINES = 4;

    private int _score;
    private int _level = 1;
    private int _lines = 5;

    public int Level { get { return _level; } }

    public bool DidLevelChanged { get; private set; }

    [SerializeField] private int _linesPerLevel = 5;


    [SerializeField]  TMP_Text _linesText;
    [SerializeField]  TMP_Text _levelText;
    [SerializeField]  TMP_Text _scoreText;

    private void Start()
    {
        UpdateUIText();
    }

    public void ScoreLines(int lines)
    {
        DidLevelChanged = false;
        lines = Mathf.Clamp(lines, MIN_CLEARED_LINES, MAX_CLEARED_LINES);
        _lines -= lines;

        switch(lines)
        {
            case 1:
                _score += 40 * _level;
                break;
            case 2:
                _score += 100 * _level;
                break;
            case 3:
                _score += 300 * _level;
                break;
            case 4:
                _score += 1200 * _level;
                break;
        }
        
        if(_lines <= 0)
        {
            var linesCompleted = Mathf.Abs(_lines);
            LevelUp();
            _lines -= linesCompleted;
        }

        UpdateUIText();
    }

    public void LevelUp()
    {
        _level++;
        _lines = _level * _linesPerLevel;
        DidLevelChanged = true;
    }

    public void Reset()
    {
        _score = 0;
        _level = 1;
    }

    void UpdateUIText()
    {
        if(_levelText)
        {
            _levelText.text = _level.ToString();
        }

        if (_linesText)
        {
            _linesText.text = _lines.ToString();
        }

        if (_scoreText)
        {
            _scoreText.text = _score.ToString("000000");
        }
    }


}
