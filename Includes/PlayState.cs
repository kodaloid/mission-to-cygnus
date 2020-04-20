using System;

namespace MTC.Includes
{
    public enum PlayState : int
    {
        // The intro animation sequence (fade in from black, caeruleum flies in from left to center screen).
        Intro = 0,
        // The inventory screen shown when the player mining ship is docked. Loading a game always enables 
        // this state immediately, as this is the state at which a player leaves.
        InventoryMenu = 1,
        // Animation when alighting from the top.
        Leaving_Top = 2,
        // Animation when alighting from the bottom.
        Leaving_Bottom = 3,
        // The main play state where the player can fly around the level.
        Mining = 4,
        // Animation when returning from the top.
        Return_Top = 5,
        // Animation when returning from the bottom.
        Return_Bottom = 6,
        // Animation when the level is complete and the caeruleum leaves to the right.
        Level_Complete = 7,
        // Sequence when the player dies.
        GameOver = 8
    }
}