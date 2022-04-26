namespace tictactoe
{
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
