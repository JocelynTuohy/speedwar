﻿using System;
using System.Collections.Generic;

namespace speedwar
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();
            Deck warDeck = new Deck();
            warDeck.shuffle();
            Player ava = new Player();
            ava.setHand(warDeck, 26);
            int discardTarget = 2;
            int discarded = 0;
            Console.WriteLine("Welcome to Speed War! Enter your name to play:");
            Player user = new Player(Console.ReadLine());
            user.setHand(warDeck, 26);
            Console.WriteLine("Hello, {0}!", user.name);
            GameState(ava, user);
            Instruct();
            Player winner = null;
            Player roundWinner = null;
            while (winner == null)
            {
                Console.ReadKey();
                roundWinner = null;
                int avaScore = ava.play();
                int userScore = user.play();
                if (avaScore == userScore)
                {
                    Console.Clear();
                    roundWinner = startWar(ava, user, winner);
                    Console.WriteLine("{0} played the {1} of {2}.", ava.name, ava.played.cards[ava.played.cards.Count - 1].rank, ava.played.cards[ava.played.cards.Count - 1].suit);
                    Console.WriteLine("{0} played the {1} of {2}.", user.name, user.played.cards[user.played.cards.Count - 1].rank, user.played.cards[user.played.cards.Count - 1].suit);
                    Console.WriteLine("{0} wins the war, capturing {1} of their opponent's cards!", roundWinner.name, ava.played.cards.Count + user.played.cards.Count - roundWinner.played.cards.Count);
                } else if (avaScore == -1)
                {
                    roundWinner = user;
                    winner = user;
                    Console.WriteLine("{0} has run out of cards!", ava.name);
                } else if (userScore == -1)
                {
                    roundWinner = ava;
                    winner = ava;
                    Console.WriteLine("{0} has run out of cards!", user.name);
                } else if (avaScore > userScore)
                {
                    roundWinner = ava;
                } else
                {
                    roundWinner = user;
                }
                if (winner == null)
                {
                    Console.Clear();
                    Console.WriteLine("{0} played the {1} of {2}.", ava.name, ava.played.cards[ava.played.cards.Count - 1].rank, ava.played.cards[ava.played.cards.Count - 1].suit);
                    Console.WriteLine("{0} played the {1} of {2}.", user.name, user.played.cards[user.played.cards.Count - 1].rank, user.played.cards[user.played.cards.Count - 1].suit);
                    Console.WriteLine("{0} wins the round, capturing {1} of their opponent's cards!", roundWinner.name, ava.played.cards.Count + user.played.cards.Count - roundWinner.played.cards.Count);
                    int avaCounter = ava.played.cards.Count;
                    foreach (Card card in ava.played.cards.ToArray())
                    {
                        // Console.WriteLine("Ava has " + ava.played.cards.Count + " played cards remaining; her counter is " + avaCounter);
                        if (card.val == discardTarget && avaCounter == 1)
                        {
                            Console.WriteLine("Discarding all {0}s from the game--discarded the {0} of {1} from the played cards.", ava.played.cards[0].rank, ava.played.cards[0].suit);
                            ava.played.deal();
                            ++discarded;
                        } else
                        {
                            roundWinner.captured.cards.Add(ava.played.deal());
                        }
                        --avaCounter;
                    }
                    int userCounter = user.played.cards.Count;
                    foreach (Card card in user.played.cards.ToArray())
                    {
                        // Console.WriteLine("User has " + user.played.cards.Count + " played cards remaining; your counter is " + userCounter);
                        if (card.val == discardTarget && userCounter == 1)
                        {
                            Console.WriteLine("Discarding all {0}s from the game--discarded the {0} of {1} from the played cards.", user.played.cards[0].rank, user.played.cards[0].suit);
                            user.played.deal();
                            ++discarded;
                        } else
                        {
                            roundWinner.captured.cards.Add(user.played.deal());
                        }
                        --userCounter;
                    }
                    if (discarded == 4 && discardTarget < 14)
                    {
                        Console.WriteLine("We've now discarded all of the {0}s from the game--now we will start discarding all {1}s.", warDeck.ranks[discardTarget - 2], warDeck.ranks[discardTarget - 1]);
                        discarded = 0;
                        ++discardTarget;
                    }
                }
                if (ava.checkLoser())
                {
                    winner = user;
                } else if (user.checkLoser())
                {
                    winner = ava;
                }
                if (winner == null)
                {
                    GameState(ava, user);
                    Instruct();
                }
            }
            if (winner == user)
            {
                Console.WriteLine("Congratulations--you've won! {0} ran out of cards.", ava.name);
                Console.Beep();
            } else
            {
                Console.WriteLine("Oh no--you've run out of cards! {0} wins. Play again sometime!", ava.name);
            }
        }

        static void GameState(Player player1, Player player2)
        {
            Console.WriteLine("{0}'s deck has {1} cards. Your deck has {2} cards.", player1.name, player1.deckSize(), player2.deckSize());
        }
        static void Instruct()
        {
            Console.WriteLine("Hit any key to continue play.");
        }
        static Player startWar(Player player1, Player player2, Player winner)
        {
            Console.Clear();
            Console.WriteLine("{0} played the {1} of {2}.", player1.name, player1.played.cards[player1.played.cards.Count - 1].rank, player1.played.cards[player1.played.cards.Count - 1].suit);
            Console.WriteLine("{0} played the {1} of {2}.", player2.name, player2.played.cards[player2.played.cards.Count - 1].rank, player2.played.cards[player2.played.cards.Count - 1].suit);
            Console.WriteLine("THIS MEANS WAR!!!");
            Console.WriteLine("{0} and {1} each play three cards facedown.", player1.name, player2.name);
            Instruct();
            Console.ReadKey();
            Player roundWinner = null;
            int player1warScore = player1.play(4);
            int player2warScore = player2.play(4);
                if (player1warScore == player2warScore)
                {
                    roundWinner = startWar(player1, player2, winner);
                } else if (player1warScore == -1)
                {
                    roundWinner = player2;
                    winner = player2;
                    Console.WriteLine("{0} has run out of cards!", player1.name);
                    return winner;
                } else if (player2warScore == -1)
                {
                    roundWinner = player1;
                    winner = player1;
                    Console.WriteLine("{0} has run out of cards!", player2.name);
                    return winner;
                } else if (player1warScore > player2warScore)
                {
                    roundWinner = player1;
                } else
                {
                    roundWinner = player2;
                }
            return roundWinner;
        }
    }
}
