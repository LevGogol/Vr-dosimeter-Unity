using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class Box : MonoBehaviour
{
    private const int MAX_SCORE = 9999;
    private const int FIVE_MINUTE = 5000/*60_000 * 5*/;
    private const int TEN_SECOND = 10_000;

    private StateMachine machine;

    private bool powerButton = false;
    private bool scoreboardButton = true;
    private bool testButton = false;
    private Range range = Range.Val1;

    private bool powerLamp = false;
    private bool rangeLamp = false;

    private float score = 0f;
    
    private bool isPrepared = false;
    private bool isTest = false;

    public Tablo tablo;

    public bool IsPower() => powerButton;
    public void SetPower(bool enabled)
    {
        powerButton = enabled;
        machine.Move();
    }

    public bool IsEnabledScoreboard() => scoreboardButton;
    public void SetScoreboard(bool enabled)
    {
        scoreboardButton = enabled;
        machine.Move();
    }

    public bool IsPressedOnTestButton() => testButton;

    public void PressingOnTestButton()
    {
        testButton = true;
        machine.Move();
    }

    public void ReleaseTheTestButton()
    {
        {
            testButton = false;
            machine.Move();
        }
    }

    public Range GetRange() => range;
    public void SetRange(Range range) => changeRange(range);
    public Range NextRange() => nextRange();

    public float GetScore() => score * (int) GetRange();
    public void SetScore(float score) => changeScore(score);

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

    private void Start()
    {
        StartCoroutine(TasksPool.Instance.Courutine());
        
        var inactive = new StateMachine.State();
        var preparationForWork = new StateMachine.State();
        var active = new StateMachine.State();
        var activeWithoutScoreboard = new StateMachine.State();
        var activeAlongWithScoreboard = new StateMachine.State();
        var pressTestButton = new StateMachine.State();
        var test = new StateMachine.State();

        machine = new StateMachine()
            .Enter(inactive)
            .Add(preparationForWork)
            .Add(activeWithoutScoreboard)
            .Add(activeAlongWithScoreboard)
            .Add(pressTestButton)
            .Add(test);

        inactive.Add(new StateMachine.Transition(
            IsPower,
            () =>
            {
                powerOn();
                if (!isPrepared)
                {
                    machine.MoveAfter(FIVE_MINUTE, () => isPrepared = true);
                }
            },
            preparationForWork));

        var powerOffTransition = new StateMachine.Transition(() => !IsPower(), () => {powerOff(); Debug.Log("Power Off");}, inactive);
        preparationForWork.Add(new StateMachine.Transition(() => isPrepared, () => { Debug.Log("Box active"); }, active))
            .Add(powerOffTransition);

        active.Add(new StateMachine.Transition(IsEnabledScoreboard, () => {Debug.Log("EnabledScoreboard state"); }, activeWithoutScoreboard))
            .Add(new StateMachine.Transition(() => !IsEnabledScoreboard(), () => {Debug.Log("DisabledScoreboard state"); }, activeAlongWithScoreboard));

        var pressTestTransition = new StateMachine.Transition(() => testButton,() =>
                {
                    isTest = false;
                    machine.MoveAfter(TEN_SECOND,
                        () =>
                        {
                            if (testButton)
                            {
                                isTest = true;
                            }
                        });
                    Debug.Log("Press test");
                }, pressTestButton);
        activeWithoutScoreboard.Add(pressTestTransition)
            .Add(powerOffTransition)
            .Add(new StateMachine.Transition(() => !scoreboardButton, () => {updateScoreboard(scoreboardButton);}, activeAlongWithScoreboard));
        activeAlongWithScoreboard.Add(pressTestTransition)
            .Add(powerOffTransition)
            .Add(new StateMachine.Transition(() => scoreboardButton, () => {updateScoreboard(scoreboardButton);}, activeWithoutScoreboard));

        pressTestButton.Add(new StateMachine.Transition(() => isTest, startTest, test))
            .Add(powerOffTransition)
            .Add(new StateMachine.Transition(() => !testButton, () => { }, active));

        test.Add(new StateMachine.Transition(() => !IsPower(), () => {endTest(); powerOff();}, inactive))
            .Add(new StateMachine.Transition(() => !testButton, endTest, active));

    }

    private void updateScoreboard(bool enabled)
    {
        if (enabled)
        {
            tablo.Enable();
        }
        else
        {
            tablo.Disable();
        }
    }

    private void SetPowerLamp(bool enabled)
    {
        powerLamp = enabled;
        if (enabled)
        {
            tablo.EnablePowerLamp();
            return;
        }

        tablo.DisablePowerLamp();
    }

    private void SetRangeLamp(bool enabled)
    {
        rangeLamp = enabled;
        if (enabled)
        {
            tablo.EnableRangeLamp();
            return;
        }

        tablo.DisableRangeLamp();
    }

    private void powerOn()
    {
        updateScoreboard(IsEnabledScoreboard());
        SetPowerLamp(true);
        SetRangeLamp(false);
    }

    private void powerOff()
    {
        updateScoreboard(false);
        SetPowerLamp(false);
        SetRangeLamp(false);
    }

    private void changeRange(Range range)
    {
        this.range = range;
    }

    private Range nextRange()
    {
        var ranges = Enum.GetValues(typeof(Range));
        for (var i = 0; i < ranges.Length; i++)
        {
            if (ranges.GetValue(i).Equals(GetRange()))
            {
                return (Range) ranges.GetValue(++i >= ranges.Length ? 0 : i);
            }
        }

        return Range.Val1;
    }
    
    private void changeScore(float score)
    {
        this.score = score;
        tablo.SetScore(GetScore());   
    }

    private void startTest()
    {
        Debug.Log("Start test");
        //TODO: при запуске теста
    }
    
    private void endTest()
    {
        Debug.Log("End test");
        //TODO: при конце теста
    }
}
