using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;
using System;
using System.Collections;
using TMPro;
public class StartingCLI : MonoBehaviour
{
    public TMP_Text cli;
    bool one = false; // run the cli function once
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.75f);
    }
    // give it like a terminal feel, so the "start" menu will be like a terminal with the like:
    string CLI = @"
          \n
         INITIALIZING [===100%===] \n
          \n
         CONNECTING TO Strata Orbital Launch Vehicle \n
          \n
         COMPLETE \n
          \n
         INITIALIZING CONTROLS \n
          \n
         RCS [ACTIVE] \n
           LEFT...[ACTIVE] \n
           RIGHT...[ACTIVE] \n
           UP...[ACTIVE] \n
           DOWN...[ACTIVE] \n
          \n
         ANGLE CONTROLS [ACTIVE] \n
           PITCH...[ACTIVE] \n
           YAW...[ACTIVE] \n
           ROLL...[ACTIVE] \n
          \n
         THRUSTER CONTROLS [ACTIVE] \n
           FORWARD...[ACTIVE] \n
           BACKWARD...[ACTIVE] \n
          \n
         FUEL AT 100% \n
          \n
         SOLAR PANELS DEPLOYING \n
          \n
         COMPLETE \n
          \n
         SHOWING DOCKING CAM FEED 01 \n
        ";
    string[] CLIlines;
    void Start()
    {
        CLIlines = CLI.Split('\n', StringSplitOptions.RemoveEmptyEntries);
    }
    void Update()
    {
        startingCLI();
    }
    void startingCLI()
    {
        if(!one) // only run once
        {
            foreach (var line in CLIlines)
            {
                cli.text += line;
                StartCoroutine(Wait()); // make this work
            }
        }
        one = true;
    }
}
