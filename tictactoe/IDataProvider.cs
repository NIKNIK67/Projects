using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace tictactoe
{
    public interface IDataProvider
    {
        public void AddPlayer(Player player);
        public List<Player> GetPlayers();
        public Player FindPlayerByName(string name);
        public Player FindPlayerById(int id);
        public Player FindPlayerByScore(int score);
        public void AddScore(Player player, int score);
    }
}
