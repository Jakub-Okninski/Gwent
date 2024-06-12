using Gwent_Library.Karty;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Gwent_Library.TypyKart
{
    public delegate void CardEffectDelegate (CardWarrior karta, PlayerBoard plansza);
    public abstract class CardWarrior : Card, ICloneable
    {
        public int Force { get; set; }
        public int ForceTemporary { get; set; }
        public int ForceDefault { get; }
        public bool IsHero { get; set; }
        public CardEffectDelegate Effect { get; set; }
        public CardWarrior(string name, int force, bool isHero) : base(name)
        {
            ForceTemporary = force;
            ForceDefault = force;
            Force = force;
            IsHero = isHero;
        }
        public virtual void setDefaultForce()
        {
            Force = ForceTemporary; 
        }
        public override string ToString()
        {
            return base.ToString() + $" Force: {Force}, Is Hero: {IsHero} , Force Default: {ForceDefault} , Force Temporary: {ForceTemporary}, ";
        }
    }
}