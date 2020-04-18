using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YetAnotherAutoChess.Enums
{
    public enum GameStates
    {
        Menu,
        Play,
        Defeat,
        Victory
    }
    public enum Levels
    {
        Menu,
        BattleZone
    }

    public enum GameTypes
    {
        OneVsOne,
        EightVsEight,
        PVE,
        None
    }

    public enum MatchState
    {
        Preparation,
        Disposal,
        Blessing,
        Duplication,
        Stretching,
        Battle
    }

    public enum SynergyType
    {
        Mythology,
        Deity
    }

    public enum Synergy
    {
        Aztec,
        Celtic,
        Chinese,
        Egyptian,
        Greek,
        Hindu,
        Japanese,
        Maya,
        Norse,
        Sumerian,
        Voodoo,

        Beauty,
        Chief,
        Dragon,
        Earth,
        Harvest,
        Hunt,
        Thunder,
        Trickster,
        Underworld,
        War,
        Water,
        Wisdom,

        none
    }

    public enum SynergyBuffTarget
    {
        AllAllies,
        SynergyHolders
    }

    public enum Piece
    {
        none,
        Pawn,
        Bishop,
        Knight,
        Rook,
        Queen
    }

    public enum Rarity
    {
        none,
        Common,
        Uncommon,
        Rare,
        Epic,
        Legendary
    }

    public enum ActiveUnits
    {
        Thor
    }

    public enum DamageType
    {
        none,
        Physical,
        Magical,
        True
    }

    public enum TargetingSystem
    {
        Target,
        ClosestEnemy,
        HighestEnemyDps,
        LowestAllyHp,
        Self
    }
}
