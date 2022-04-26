using System.Xml;

namespace tictactoe
{
    public class XmlDataProvider : IDataProvider
    {
        XmlDocument xDoc;
        public XmlDataProvider()
        {
            xDoc = new XmlDocument();
            if (!File.Exists("players.xml"))
            {
                
                FileStream  stream = File.Create("players.xml");         
                stream.Close();
                
            }
            try
            {
                xDoc.Load("players.xml");
            }
            catch{}
            XmlElement xRoot = xDoc.DocumentElement;
            if (xRoot == null)
            {
                xRoot = xDoc.CreateElement("players");
                xDoc.AppendChild(xRoot);
                xDoc.Save("players.xml");
            }
           
            
           
        }
        public void AddPlayer(Player player)
        {
            xDoc.Load("players.xml");
            XmlElement xRoot = xDoc.DocumentElement;
            XmlElement xPlayer = xDoc.CreateElement("player");
            XmlAttribute idAttribute = xDoc.CreateAttribute("id");
            XmlElement xmlName = xDoc.CreateElement("name");
            XmlElement xmlScore = xDoc.CreateElement("score");
            XmlText idText = xDoc.CreateTextNode($"{xRoot.ChildNodes.Count}");
            XmlText nameText = xDoc.CreateTextNode($"{player.Name}");
            XmlText scoreText = xDoc.CreateTextNode($"{player.Score}");
            idAttribute.AppendChild(idText);
            xmlName.AppendChild(nameText);
            xmlScore.AppendChild(scoreText);
            xPlayer.Attributes.Append(idAttribute);
            xPlayer.AppendChild(xmlName);
            xPlayer.AppendChild(xmlScore);
            xRoot.AppendChild(xPlayer);
            xDoc.Save("players.xml");

        }

        public void AddScore(Player player, int score)
        {
            xDoc.Load("players.xml");
            List<Player> list = GetPlayers();
            XmlElement xRoot = xDoc.DocumentElement;
            XmlNodeList nodes = xRoot.SelectNodes("*");
            if (nodes.Count != 0 && nodes is not null)
            {
                foreach (XmlNode node in nodes)
                {
                    if (Convert.ToInt32(node.Attributes.GetNamedItem("id").Value) == player.Id)
                    {
                        foreach (XmlNode ChildNode in node.ChildNodes)
                        {
                            if (ChildNode.Name == "score")
                            {
                                ChildNode.InnerText = Convert.ToString(Convert.ToInt32(ChildNode.InnerText) + score);
                                xDoc.Save("players.xml");
                                break;
                            }
                        }
                        
                        
                    }
                }

            }
            
        }
       
        public Player FindPlayerById(int id)
        {

            xDoc.Load("players.xml");
            List<Player> players = GetPlayers();
            foreach (Player player in players)
            {
                if (player.Id == id)
                {
                    return player;
                }
            }
            return null;
        }

        public Player FindPlayerByName(string name)
        {
            xDoc.Load("players.xml");
            List<Player> players = GetPlayers();
            foreach (Player player in players)
            {
                if (player.Name == name)
                { 
                    return player;
                }
            }
            return null;
        }

        public Player FindPlayerByScore(int score)
        {
            xDoc.Load("players.xml");
            List<Player> players = GetPlayers();
            foreach (Player player in players)
            {
                if (player.Score == score)
                {
                    return player;
                }
            }
            return null;
        }

        public List<Player> GetPlayers()
        {
            xDoc.Load("players.xml");
            XmlElement xRoot = xDoc.DocumentElement;
            XmlNodeList nodes = xRoot.SelectNodes("*");
            if (nodes.Count != 0 && nodes is not null)
            {   List<Player> result = new List<Player>();
                foreach (XmlNode xmlNode in nodes)
                {
                    Player player = new Player();
                    player.Id = Convert.ToInt32(xmlNode.Attributes.GetNamedItem("id").Value);
                    foreach (XmlNode childNode in xmlNode.ChildNodes)
                    {
                        if (childNode.Name == "name")
                        {
                            player.Name = childNode.InnerText;
                        }
                        if (childNode.Name == "score")
                        {
                            player.Score = Convert.ToInt32(childNode.InnerText);

                        }
                    }
                    result.Add (player);
                   
                        
                }
                return result;
            }
            else
            {
                return new List<Player> { };
            }
            return new List<Player>() { };
        }



    }
}
