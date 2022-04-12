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
    public class XMLDataProvider : IDataProvider
    {
        private FileStream stream = new FileStream("player.xml", FileMode.OpenOrCreate);
        public void AddPlayer(Player player)
        {
            XmlSerializer xml = new XmlSerializer(typeof(Player));
            xml.Serialize(stream, player);
        }

        public void AddScore(Player player, int score)
        {
           
        }

        public Player FindPlayerById(int id)
        {
            throw new NotImplementedException();
        }

        public Player FindPlayerByName(string name)
        {
            throw new NotImplementedException();
        }

        public Player FindPlayerByScore(int score)
        {
            throw new NotImplementedException();
        }

        public List<Player> GetPlayers()
        {
            throw new NotImplementedException();
        }
    }
    public class EFDataProvider : IDataProvider
    {
        public void AddPlayer(Player player)
        {
            using (EFContext db = new EFContext())
            {
                db.Players.Add(player);
                db.SaveChanges();
            }
        }
    
        public void AddScore(Player player, int score)
        {
            using (EFContext db = new EFContext())
            {
                db.Players.Find(player.Id).Score += score;
                db.SaveChanges();
            }
        }
    
        public Player FindPlayerById(int id)
        {
            using (EFContext db = new EFContext())
            {
                return db.Players.Find(id);
            }
        }
    
        public Player FindPlayerByName(string name)
        {
            using (EFContext db = new EFContext())
            {
                foreach (Player player in db.Players)
                {
                    if (player.Name == name)
                    {
                        return player;
                    }
                }
            }
            return null;
        }
    
        public Player FindPlayerByScore(int score)
        {
            using (EFContext db = new EFContext())
            {
                foreach (Player player in db.Players)
                {
                    if (player.Score == score)
                    {
                        return player;
                    }
                }
            }
            return null;
        }
    
        public List<Player> GetPlayers()
        {
            using (EFContext db = new EFContext())
            {
                return db.Players.ToList();
            }
        }
    } 
}
