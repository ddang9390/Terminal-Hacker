using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hacker : MonoBehaviour {
    string[] level1passwords = { "book", "aisle", "borrow", "librarian" };
    string[] level2passwords = { "handcuff", "prison", "arrest", "confinement" };
    int index;

    //Game state
    private int level;
    enum Screen { MainMenu, Password, Win };
    Screen currentScreen;
    private String password;

	// Use this for initialization
	void Start () {
        showMainMenu();
	}
	
	// Update is called once per frame
	void Update () {
        
    }

    void OnUserInput(string input)
    {
        // Go to main menu
        if (input.Equals("menu"))
        {
            showMainMenu();
        }
        switch (currentScreen)
        {
            // Handling input on main menu
            case Screen.MainMenu:
                RunMainMenu(input);
                break;
            // Handling input on acutal game screen
            case Screen.Password: //TODO Bug that causes password to reset on incorrect input
                showGameScreen(level);
                runGame(input);
                break;
            // Handling input on win screen
            case Screen.Win:
                showWinScreen();
                break;
            default:
                Debug.LogError("No valid screen");
                break;
        }
        //// Handling input on the main menu
        //else if (currentScreen == Screen.MainMenu)
        //{
        //    RunMainMenu(input);
        //}
        //// Handling input on the actual game screen
        //else if (currentScreen == Screen.Password)
        //{
        //    runGame(input);
        //}
        //// Handling input on the win screen
        //else if (currentScreen == Screen.Win)
        //{
        //    showWinScreen();
        //}
    }

    private void runGame(string input)
    {
        if (password.Equals(input))
        {
            showWinScreen();
        }
        else
        {
            Terminal.WriteLine("Incorrect password");
        }
    }

    private void RunMainMenu(string input)
    {
        bool isValidNumber = (input.Equals("1") || input.Equals("2"));
        if (isValidNumber)
        {
            level = int.Parse(input);
            showGameScreen(level);
        }
        
        else
        {
            Terminal.WriteLine("Please enter either 1 or 2");
        }
    }

    private void showGameScreen(int level)
    {
        Terminal.ClearScreen();

        setPassword(level);
        currentScreen = Screen.Password;

        Terminal.WriteLine("You have chosen level " + level);
        Terminal.WriteLine("Enter your password, hint: " + password.Anagram());
    }

    private void setPassword(int level)
    {
        switch (level)
        {
            case 1:
                Terminal.WriteLine("Hacking into library");
                password = level1passwords[index];
                break;

            case 2:
                Terminal.WriteLine("Hacking into police station");
                password = level2passwords[index];
                break;

            default:
                Debug.LogError("Invalid level");
                break;
        }
    }

    private void showMainMenu()
    {
        index = UnityEngine.Random.Range(0, 4);
        currentScreen = Screen.MainMenu;

        Terminal.ClearScreen();

        Terminal.WriteLine("Greetings meatbag. What would you like to hack into?");
        Terminal.WriteLine("Press 1: Library");
        Terminal.WriteLine("Press 2: Police Station");
        Terminal.WriteLine("Enter your selection: ");
    }

    private void showWinScreen()
    {
        currentScreen = Screen.Win;

        Terminal.ClearScreen();

        Terminal.WriteLine("Congratulations! You win!");
        Terminal.WriteLine("Please enter 'menu' to play again");
    }
}
