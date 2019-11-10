using System;
using System.Linq;
using System.Collections.Generic;

class Card : IComparable<Card>
{
    private int number;
    private char letter;

    public Card(string cardStr)
    {
        int number = int.Parse(cardStr.Substring(0, cardStr.Length - 1));
        char letter = cardStr.Last();

        this.Number = number;
        this.Letter = letter;
    }

    public int Number
    {
        get { return number; }
        set { number = value; }
    }

    public char Letter
    {
        get { return letter; }
        set { letter = value; }
    }

    public int CompareTo(Card other)
    {
        if (this.Number != other.Number)
        {
            return other.Number.CompareTo(this.Number);
        }

        return other.Letter.CompareTo(this.Letter);
    }
}

class NumberWars
{
    static Queue<Card> ReadCards()
    {
        Queue<Card> playerCards = new Queue<Card>();

        Console.ReadLine()
            .ToLower()
            .Split(' ')
            .ToList()
            .ForEach(cardStr => playerCards.Enqueue(new Card(cardStr)));

        return playerCards;
    }

    static void CheckCardsCounts(Queue<Card> firstPlayerCards, Queue<Card> secondPlayerCards, int turnNumber)
    {
        if (firstPlayerCards.Count == 0 && secondPlayerCards.Count == 0)
        {
            Console.WriteLine($"Draw after {turnNumber + 1} turns");
            Environment.Exit(0);
        }
        else if (firstPlayerCards.Count == 0)
        {
            Console.WriteLine($"Second player wins after {turnNumber} turns");
            Environment.Exit(0);
        }
        else if (secondPlayerCards.Count == 0)
        {
            Console.WriteLine($"First player wins after {turnNumber} turns");
            Environment.Exit(0);
        }
    }

    static void BothPlayersDrawThreeCards(Queue<Card> firstPlayerCards, Queue<Card> secondPlayerCards, List<Card> firstPlayerDrawnCards, List<Card> secondPlayerDrawnCards, int turnNumber)
    {
        for (int i = 0; i < 3; i++)
        {
            CheckCardsCounts(firstPlayerCards, secondPlayerCards, turnNumber);

            firstPlayerDrawnCards.Add(firstPlayerCards.Dequeue());
            secondPlayerDrawnCards.Add(secondPlayerCards.Dequeue());
        }
    }

    static int GetLastThreeCardsLettersSum(List<Card> playerDrawnCards)
    {
        return playerDrawnCards
            .Skip(playerDrawnCards.Count - 3)
            .Sum(card => card.Letter);
    }

    static void Main(string[] args)
    {
        Queue<Card> firstPlayerCards = ReadCards();
        Queue<Card> secondPlayerCards = ReadCards();

        const int maxTurnsCount = 1000000;

        for (int turnNumber = 0; turnNumber < maxTurnsCount; turnNumber++)
        {
            CheckCardsCounts(firstPlayerCards, secondPlayerCards, turnNumber);
            
            Card firstPlayerCard = firstPlayerCards.Dequeue();
            Card secondPlayerCard = secondPlayerCards.Dequeue();

            List<Card> cards = new List<Card>
            {
                firstPlayerCard,
                secondPlayerCard
            };

            bool firstWinsCurrentTurn = false;

            if (firstPlayerCard.Number != secondPlayerCard.Number)
            {
                 firstWinsCurrentTurn = firstPlayerCard.Number > secondPlayerCard.Number;
            }
            else
            {
                List<Card> firstPlayerDrawnCards = new List<Card>();
                List<Card> secondPlayerDrawnCards = new List<Card>();

                while (true)
                {
                    BothPlayersDrawThreeCards(firstPlayerCards, secondPlayerCards, firstPlayerDrawnCards, secondPlayerDrawnCards, turnNumber);

                    int firstPlayerScoreOfLetters = GetLastThreeCardsLettersSum(firstPlayerDrawnCards);
                    int secondPlayerScoreOfLetters = GetLastThreeCardsLettersSum(secondPlayerDrawnCards);

                    if (firstPlayerScoreOfLetters != secondPlayerScoreOfLetters)
                    {
                        if (firstPlayerScoreOfLetters > secondPlayerScoreOfLetters)
                        {
                            firstWinsCurrentTurn = true;
                        }

                        firstPlayerDrawnCards.ForEach(cards.Add);
                        secondPlayerDrawnCards.ForEach(cards.Add);

                        break;
                    }
                }
            }

            cards.Sort();

            if (firstWinsCurrentTurn)
            {
                cards.ForEach(firstPlayerCards.Enqueue);
            }
            else
            {
                cards.ForEach(secondPlayerCards.Enqueue);
            }
        }

        if (firstPlayerCards.Count > secondPlayerCards.Count)
        {
            Console.WriteLine($"First player wins after {maxTurnsCount} turns");

        }
        else if (secondPlayerCards.Count > firstPlayerCards.Count)
        {
            Console.WriteLine($"Second player wins after {maxTurnsCount} turns");
        }
        else
        {
            Console.WriteLine($"Draw after {maxTurnsCount} turns");
        }
    }
}