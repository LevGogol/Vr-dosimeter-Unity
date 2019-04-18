using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    private const int MAX_SCORE = 9999;

    private bool powerButton = false;
    private bool scoreboardButton = true;
    private bool testButton = false;
    private Range range = Range.Val1;

    private bool powerLamp = false;
    private bool rangeLamp = false;

    private int score = 0;

    public bool IsPower() => powerButton;
    public void SetPower(bool enabled) => actionPowerButton(enabled);

    public bool IsEnabledScoreboard() => scoreboardButton;
    public void SetScoreboard(bool enabled) => actionScoreboardButton(enabled);

    public bool IsPressedOnTestButton() => testButton;
    public void PressingOnTestButton() => actionTestButton(true);
    public void ReleaseTheTestButton() => actionTestButton(false);

    public Range GetRange() => range;
    public void SetRange(Range range) => this.range = range;

    public int GetScore() => score;
    public void SetScore(int score) => this.score = (score > MAX_SCORE) ? MAX_SCORE : score;

    public bool IsPowerLampOn() => powerLamp;
    public bool IsRangeLampOn() => rangeLamp;

    public enum Range
    {
        Val1 = 1,
        Val5 = 5,
        Val10 = 10,
        Val50 = 50,
        Val100 = 100
    }

    //Действие при переключении тумблера "Сеть"
    private void actionPowerButton(bool enabled)
    {
        if(powerButton == enabled){
            return;
        }

        powerButton = enabled;
        powerLamp = enabled;

        if (enabled)
        {
            startPower();
            return;
        }
        endPower();
    }

    //Действие при переключении тумблера "Табло"
    private void actionScoreboardButton(bool enabled)
    {
        if(scoreboardButton == enabled){
            return;
        }

        scoreboardButton = enabled;
        setScoreboard(enabled);
    }

    //Действие при нажатии или отпускании кнопки "Тест"
    private void actionTestButton(bool enabled)
    {
        if (testButton == enabled)
        {
            return;
        } else if (enabled && !IsPower())
        {
            testButton = true;
            rangeLamp = true;
            startTest();
            return;
        } else if(!enabled && testButton)
        {
            endTest();
        }

        testButton = false;
        rangeLamp = false;
    }
    
    //Включение или отключение табло
    private void setScoreboard(bool enabled)
    {
        //TODO: включение или выклчение табло.
    }

    //Вызывается при включении устройства
    private void startPower()
    {
        //TODO: обработчик радиации
    }

    //Вызывается при выключении устройства
    private void endPower()
    {
        //TODO: конец обработки радиации
    }

    private void startTest()
    {
        //TODO: начало тестирования прибора
    }

    private void endTest()
    {
        //TODO: конец тестирования прибора
    }

}
