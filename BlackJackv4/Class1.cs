using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackv4
{
     class Blackjackv4
    {
        public static int point = 100;
        public static void Main(string[] args)
        {
            main.start();
        }
    }

    class main
    {
        public static void start()
        {
            while (true)
            {
                if (Blackjackv4.point < 0)
                {
                    Console.WriteLine("Game Over");
                    Console.WriteLine("CONTINUE ANY KEY");
                    Console.ReadLine();
                    Console.WriteLine("reset +coin 100");
                    Blackjackv4.point = 100;
                }
                else if (Blackjackv4.point > 0)
                {
                    Console.WriteLine("現在コイン" + Blackjackv4.point + "ベットする金額を決めてください。1-10000");
                    int point = Convert.ToInt32(Console.ReadLine());
                    if (point <= 10000 && Blackjackv4.point >= point)
                    {
                        Blackjackv4.point -= point;

                        judge.admin(point);


                    }
                    else if (point > 10000 && Blackjackv4.point <= point)
                    {
                        Console.WriteLine("その数はベットできません。");
                    }
                }

            }
        }

    }


    enum Result
    {
         win,
         lose,
         draw
    }
    public enum Action
    {
        raze,
        forld,
        bet
    }



    class Card 
    {
        private  String[] number = { "A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13" };
        private  String[] Mark = { "スペード", "ダイヤ", "ハート", "クラブ" };

        private  int[] points = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13 };

        public String numberpointer;

        public String markpointer;

        public int pointspointer;


        public Card()
        {

        }

        public Card(String mark , String num,  int point)
        {
            numberpointer = num;
            markpointer = mark;
            pointspointer = point;

        }



        public  String getnumber(int index)
        {
            return number[index];
        }
        public String getMark (int index)
        {
            return Mark[index];
        }

        public int getpoint (int index)
        {
            return points[index];
        }


    }

    class Deck 
    {
       public Queue<Card> queue = new Queue<Card>();

       public Deck()
        {
        
           Card card = new Card();

            for(int i = 0; i < 12; i++)
            {
                queue.Enqueue(new Card(card.getMark(0),card.getnumber(i),card.getpoint(i)));
            }
            for(int i = 0; i < 12; i++)
            {
                queue.Enqueue(new Card(card.getMark(1),card.getnumber(i),card.getpoint(i)));
            }
            for (int i = 0; i < 12; i++)
            {
                queue.Enqueue(new Card(card.getMark(2), card.getnumber(i), card.getpoint(i)));
            }
            for (int i = 0; i < 12; i++)
            {
                queue.Enqueue(new Card(card.getMark(3), card.getnumber(i), card.getpoint(i)));
            }
        
        }

        public Queue<Card> CardSuffle(Deck deck)
        {
            Card[] cards = deck.queue.ToArray();
            Random rand = new Random();
            int n = cards.GetLength(0);
            while(n > 1)
            {
                n--;
                int k = rand.Next(n + 1);
                Card card = cards[k];
                cards[k] = cards[n];
                cards[n] = card;
            }

            Queue<Card> queue = new Queue<Card>(cards);

            return queue;


        }


    }



    class judge
    {
       public void judgementace(Card card)
        {
           if(card.numberpointer.Equals("A"))
            {
                Console.WriteLine("ACEか11か選べます");
                String text = Console.ReadLine();

                if (text.Equals("A"))
                {
                    card.pointspointer = 1;
                }else if (text.Equals("11"))
                {
                    card.pointspointer = 11;
                }
            }

        }





        public static void admin(int money)
        {
            Result result = Fight();
            Action action = judge.GetAction();
            if(Result.win == result)
            {
                if (Action.raze == action)
                {
                    Console.WriteLine(money + "raze:x2\n" + money * 2);
                    money *= 2;
                }
                if (Action.forld == action)
                {
                    Console.WriteLine(money + "forld:\n" + money);
                }
                if (Action.bet == action)
                {
                    Console.WriteLine(money + "bet\n" + money + money);
                    money += money;
                }
            }


            else if (Result.lose == result)
            {
                if (Action.raze == action)
                {
                    Console.WriteLine(money + "raze:-x2\n" + money * 2);
                    money -= money * 2;
                }
                if (Action.forld == action)
                {
                    Console.WriteLine(money + "forld:\n" + money);
                }
                if (Action.bet == action)
                {
                    Console.WriteLine(money + "bet\n" + money + money);
                    money -= money;
                }

            }
            else if (Result.draw == result)
            {
                if (Action.raze == action)
                {
                    Console.WriteLine(money + "raze:x2\n" + (money * 2) / 2);
                    money -= (money * 2) / 2;
                }
                if (Action.forld == action)
                {
                    Console.WriteLine(money + "forld:\n" + money);
                }
                if (Action.bet == action)
                {
                    Console.WriteLine(money + "bet\n" + money + money / 2);
                    money += money / 2;
                }
            }

            Blackjackv4.point += money;

        }

        bool playersflag = true;
        bool dealersflag = true;





        public int dealerscard;
        public int playerscard;
        public static Action actionpointer;

        static Action GetAction()
        {
            return actionpointer;
        }

        static Result Fight()
        {
            judge judge = new judge();
            Deck deck = new Deck();
            deck.queue = deck.CardSuffle(deck);
            while (judge.playersflag || judge.dealersflag)
            {
                if (judge.playersflag)
                {
                    judge.playersturn(deck.queue.Dequeue());
                }
                if (judge.dealersflag)
                {
                    judge.dealersturn(deck.queue.Dequeue());
                }
            }

           return judge.Account(judge);

        }

        public Result Account(judge judge)
        {
            if (judge.playerscard <= 21 && judge.dealerscard <= 21)
            {
                if (judge.playerscard <= judge.dealerscard)
                {
                    Console.WriteLine("相手の合計");
                    Console.WriteLine(judge.dealerscard);
                    Console.WriteLine("あなたの合計");
                    Console.WriteLine(judge.playerscard);
                    Console.WriteLine("あなたの負けです...");
                    return Result.lose;
                }
                else if (judge.playerscard >= judge.dealerscard)
                {
                    Console.WriteLine("あなたの合計");
                    Console.WriteLine(judge.playerscard);
                    Console.WriteLine("相手の合計");
                    Console.WriteLine(judge.dealerscard);
                    Console.WriteLine("あなたの勝ちです！");
                    return Result.win;
                }

            }
       else if (judge.playerscard > 21 || judge.dealerscard > 21)
            {
                if (judge.playerscard > 21)
                {
                    Console.WriteLine("バースト！");
                    Console.WriteLine("あなたの負けです...");
                    return Result.lose;
                }
                if (judge.dealerscard > 21)
                {
                    Console.WriteLine("バースト！");
                    Console.WriteLine("あなたの勝ちです！");
                    return Result.win;

                }

            }
            else if (judge.playerscard == 21 && judge.dealerscard == 21)
            {Console.WriteLine("引き分け");
              return Result.draw;
               }
                return Result.draw;
                  }
        public bool firstmatch = true;

        void dealersturn(Card card)
        {
            if (16 <= dealerscard)
            {
                Console.WriteLine("引かない");
                dealersflag = false;
                Console.WriteLine("ディーラー合計" + dealerscard);
            }
            else if (16 >= dealerscard)
            {
                Console.WriteLine(card.markpointer + "の" + card.numberpointer + "を" + "引いた");
                dealerscard += card.pointspointer;
                Console.WriteLine("ディーラー合計" + dealerscard);

            }
        }
        void playersturn(Card card)
        {

            if (firstmatch)
            {
                Console.WriteLine("アクション? 賭けに属性を付けれます。\nraze:期待値二倍。しかし負けると負債も二倍\nforld:何もかけない。引かないと同じ？これがどのような効果を出すかは分からない。\nbet:通常。");

                switch (Console.ReadLine())
                {
                    case "raze":
                        {
                            actionpointer = Action.raze;
                        }
                        break;
                    case "forld":
                        {
                            actionpointer = Action.forld;
                        }
                        break;
                    case "bet":
                        {
                            actionpointer = Action.bet;
                        }
                        break;
                }
            }
            firstmatch = false;


            Console.WriteLine("引く 引かない");
            String str = Console.ReadLine();
            if (str.Equals("引く"))
            {
                Console.WriteLine(card.markpointer + "の" + card.numberpointer + "を引いた！");
                judgementace(card);
                playerscard += card.pointspointer;
                Console.WriteLine("プレイヤー合計" + playerscard);
            }
            else if (str.Equals("引かない"))
            {
                Console.WriteLine("スタンド");
                playersflag = false;
                Console.WriteLine("プレイヤー合計" + playerscard);
            }
        }





    }
}


    


