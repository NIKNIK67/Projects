using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace tictactoe
{
    [Index(nameof(Id),IsUnique =true)]
    public class Player : IComparable<Player>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Score { get; set; }
        public Player(string name, int score)
        { 
            Name = name;
            Score = score;
        }
        public Player()
        {}
        public int CompareTo(Player? other)
        {
            return other.Score.CompareTo(Score);
        }
    }
}
