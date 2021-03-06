﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    [Serializable]
    public class Player : INotifyPropertyChanged
    {
        public Player()
        {
            Name = "Player";
            Bank = 20;
            Bet = 0;
            Hand = new List<CardEnum>();
            SplitHand = new List<CardEnum>();
            FinalHandAmount = 0;
            FinalSplitAmount = 0;
            Playing = true;
            HasSplit = false;
            HaveBusted = false;
            splitHasBusted = false;
        }

        private string name;

        public string Name
        {
            get { return name; }
            set {
                name = value;
                FieldChanged();
            }
        }


        private int bank;

        public int Bank
        {
            get { return bank; }
            set {
                bank = value;
                FieldChanged();
            }
        }


        private int bet;

        public int Bet
        {
            get { return bet; }
            set {
                bet = value;
                FieldChanged();
            }
        }


        private List<CardEnum> hand;

        public List<CardEnum> Hand
        {
            get { return hand; }
            set {
                hand = value;
                FieldChanged();
            }
        }

        private List<CardEnum> splitHand;

        public List<CardEnum> SplitHand
        {
            get { return splitHand; }
            set {
                splitHand = value;
                FieldChanged();
            }
        }


        private int finalHandAmount;

        public int FinalHandAmount
        {
            get { return finalHandAmount; }
            set {
                finalHandAmount = value;
                FieldChanged();
            }
        }

        private int finalSplitAmount;

        public int FinalSplitAmount
        {
            get { return finalSplitAmount; }
            set
            {
                finalSplitAmount = value;
                FieldChanged();
            }
        }


        private bool playing;

        public bool Playing
        {
            get { return playing; }
            set {
                playing = value;
                FieldChanged();
            }
        }

        private bool hasSplit;

        public bool HasSplit
        {
            get { return hasSplit; }
            set
            {
                hasSplit = value;
                FieldChanged();
            }
        }


        private bool haveBusted;

        public bool HaveBusted
        {
            get { return haveBusted; }
            set
            {
                haveBusted = value;
                FieldChanged();
            }
        }

        private bool splitHasBusted;

        public bool SplitHasBusted
        {
            get { return splitHasBusted; }
            set
            {
                splitHasBusted = value;
                FieldChanged();
            }
        }

        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;
        protected void FieldChanged([CallerMemberName] string field = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(field));
            }
        }
    }
}
