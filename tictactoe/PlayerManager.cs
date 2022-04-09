using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tictactoe
{
    internal static class PlayerManager
    {
        internal static Player FindPlayerByName(ApplicationContext db,string name)
        {
            foreach (Player player in db.Players)
            {
                if (player.Name == name)
                {
                    return player;
                }
            }
            return null;
        }
        internal static Player FindPlayerById (ApplicationContext db, int id)
        {
            foreach (Player player in db.Players)
            {
                if (player.Id == id)
                {
                    return player;
                }
            }
            return null;
        }
        internal static Player FindPlayerByScore(ApplicationContext db, int score)
        {
            foreach (Player player in db.Players)
            {
                if (player.Score == score)
                {
                    return player;
                }
            }
            return null;
        }
        internal static void AddScore(Player player, int count)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Players.Find(player.Id).Score += count;
                db.SaveChanges();
                
            }
        }
       
    }
}
