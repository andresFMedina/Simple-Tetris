using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    Board _gameBoard;
    Spawner _spawner;
    Shape _activeShape;
    [SerializeField] GameObject _gameOverPanel;


    [SerializeField] float _dropInterval = 1f;
    [SerializeField] float _dropIntervalModded;

    float _timeToDrop;

    //float _timeToNextKey;

    float _timeToNextKeyHorizontal;
    float _timeToNextKeyDown;
    float _timeToNextKeyRotate;

    //[Range(0.02f, 1f)]
    //[SerializeField] float _keyRepeatRate = 0.25f;

    [Range(0.02f, 1f)]
    [SerializeField] float _keyRepeatRateHorizontal = 0.15f;

    [Range(0.02f, 1f)]
    [SerializeField] float _keyRepeatRateDown = 0.01f;

    [Range(0.01f, 1f)]
    [SerializeField] float _keyRepeatRateRotate = 0.25f;

    bool _isGameOver = false;

    SoundManager _soundManager;
    ScoreManager _scoreManager;

    public bool _isPaused;
    [SerializeField] GameObject _pausePanel;
    
    void Start()
    {
        _gameBoard = FindObjectOfType<Board>();
        _spawner = FindObjectOfType<Spawner>();
        _soundManager = FindObjectOfType<SoundManager>();
        _scoreManager = FindObjectOfType<ScoreManager>();

        _timeToNextKeyHorizontal = Time.time;
        _timeToNextKeyRotate = Time.time;
        _timeToNextKeyDown = Time.time;        

        if (_activeShape == null)
        {
            _activeShape = _spawner.SpawnShape();
        }

        if(_gameOverPanel)
        {
            _gameOverPanel.SetActive(false);
        }

        if (_pausePanel)
        {
            _pausePanel.SetActive(false);
        }

        _dropIntervalModded = _dropInterval;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isGameOver) return;

        PlayerInput();
    }

    private void PlayerInput()
    {
        if (Input.GetButton("MoveRight") && Time.time > _timeToNextKeyHorizontal || Input.GetButtonDown("MoveRight"))
        {
            _activeShape.MoveRight();

            _timeToNextKeyHorizontal = Time.time + _keyRepeatRateHorizontal;

            if (!_gameBoard.IsValidPosition(_activeShape))
            {
                _activeShape.MoveLeft();
                PlaySound(_soundManager.ErrorSound, 0.75f);
            }
            else 
            {
                PlaySound(_soundManager.MoveSound, 0.75f);
            }
        }
        else if (Input.GetButton("MoveLeft") && Time.time > _timeToNextKeyHorizontal || Input.GetButtonDown("MoveLeft"))
        {
            _activeShape.MoveLeft();
            _timeToNextKeyHorizontal = Time.time + _keyRepeatRateHorizontal;

            if (!_gameBoard.IsValidPosition(_activeShape))
            {
                _activeShape.MoveRight();
                PlaySound(_soundManager.ErrorSound, 0.75f);
            }
            else
            {
                PlaySound(_soundManager.MoveSound, 0.75f);
            }
        }

        else if (Input.GetButton("Rotate") && Time.time > _timeToNextKeyRotate)
        {
            _activeShape.RotateRight();
            _timeToNextKeyRotate = Time.time + _keyRepeatRateRotate;

            if (!_gameBoard.IsValidPosition(_activeShape))
            {
                _activeShape.RotateLeft();
                PlaySound(_soundManager.ErrorSound, 0.75f);
            }
            else
            {
                PlaySound(_soundManager.MoveSound, 0.75f);
            }
        }
        else if (Input.GetButton("MoveDown") && Time.time > _timeToNextKeyDown || Time.time > _timeToDrop)
        {
            _timeToDrop = Time.time + _dropIntervalModded;
            _timeToNextKeyDown = Time.time + _keyRepeatRateDown;

            _activeShape.MoveDown();

            if (!_gameBoard.IsValidPosition(_activeShape))
            {
                if(_gameBoard.IsOverLimit(_activeShape))
                {
                    GameOver();
                }
                LandShape();
            }
        }
    }

    private void GameOver()
    {
        _activeShape.MoveUp();
        _isGameOver = true;
        print("Game Over");
        PlaySound(_soundManager.GameOverSound, 5f);
        PlaySound(_soundManager.GameOverVocalSound, 0.75f);
        _gameOverPanel.SetActive(true);
    }

    private void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    private void LandShape()
    {
        _activeShape.MoveUp();
        _gameBoard.StoreShapeInGrid(_activeShape);

        PlaySound(_soundManager.DropSound, 0.50f);

        _activeShape = _spawner.SpawnShape();

        _timeToNextKeyHorizontal = Time.time;
        _timeToNextKeyRotate = Time.time;
        _timeToNextKeyDown = Time.time;

        _gameBoard.ClearAllRows();

        if(_gameBoard.completedRows > 0)
        {
            _scoreManager.ScoreLines(_gameBoard.completedRows);

            if (_scoreManager.DidLevelChanged)
            {
                PlaySound(_soundManager.LevelUpVocal, 0.75f);
                _dropIntervalModded = Mathf.Clamp(_dropInterval - ((float)_scoreManager.Level - 1) * 0.05f, 0.05f, 1);
            }

            PlaySound(_soundManager.ClearRowSound, 0.75f);
            if(_gameBoard.completedRows > 1)
            {
                int randomIndex = Random.Range(0, 4);

                PlaySound(_soundManager.VocalPlays[randomIndex], 0.75f);
            }
        }
    }

    private void PlaySound(AudioClip clip, float fxVolMultiplier)
    {
        if (_soundManager._fxEnabled && clip)
        {
            AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position, Mathf.Clamp(fxVolMultiplier * _soundManager._fxVolume, 0.05f, 1f));
        }
    }

    public void TogglePause()
    {
        if (_isGameOver) return;

        _isPaused = !_isPaused;

        if(_pausePanel)
        {
            _pausePanel.SetActive(_isPaused);
            
            Time.timeScale = _isPaused ? 0 : 1;
        }
    }
}
