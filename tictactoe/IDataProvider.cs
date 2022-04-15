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
        public XMLDataProvider()
        { 
            stream = new FileStream("player.xml", FileMode.OpenOrCreate);
           
        }
        private FileStream stream;
        public void AddPlayer(Player player)
        {
            
            XmlSerializer xml = new XmlSerializer(typeof(List<Player>));
            List<Player> players =  xml.Deserialize(stream) as List<Player>;
            players.Add(player);
            xml.Serialize(stream, players);
            
        }

        public void AddScore(Player player, int score)
        {
            
            XmlSerializer xml = new XmlSerializer(typeof(List<Player>));
            List<Player>? players = xml.Deserialize(stream) as List<Player>;
            players.Where(x => x.Id == player.Id).FirstOrDefault().Score += score;
            xml.Serialize(stream, players);
            
        }

        public Player FindPlayerById(int id)
        {
            XmlSerializer xml = new XmlSerializer(typeof(List<Player>));
            List<Player>? players = xml.Deserialize(stream) as List<Player>;
            return players.Where(x => x.Id == id).FirstOrDefault();
        }

        public Player FindPlayerByName(string name)
        {
            
            XmlSerializer xml = new XmlSerializer(typeof(List<Player>));
            List<Player>? players = xml.Deserialize(stream) as List<Player>;
            
            return players?.Where(x => x.Name == name).FirstOrDefault();
            
        }

        public Player FindPlayerByScore(int score)
        {
            
            XmlSerializer xml = new XmlSerializer(typeof(List<Player>));
            List<Player>? players = xml.Deserialize(stream) as List<Player>;
            
            return players?.Where(x => x.Score == score).FirstOrDefault();
        }

        public List<Player> GetPlayers()
        {
            
            XmlSerializer xml = new XmlSerializer(typeof(List<Player>));
            List<Player>? players;
            try
            {
                players = xml.Deserialize(stream) as List<Player>;
            }
            catch (Exception ex)
            {
                players = new List<Player> { };


            }
            
            return players;
        }
        private void SerializeNew()
        {
            players = new List<Player> { };
            xml.Serialize(stream, players);
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
