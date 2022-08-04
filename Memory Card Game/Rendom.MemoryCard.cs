using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memory_Card_Game
{
    internal class RendomMemoryCard
    {
        public static Card<T>[] ShuffleCard(Card<T>[] i_CardPack)
        {
            findAllHiddenCards(i_CardPack);
            return i_CardPack;
         }

        public static List<int> findAllHiddenCards<T>(T i_CardPack)
         {
            List<int> indexOfAllHiddenCards = new List<int>();
            foreach (Card<T> card in i_CardPack)
            {
                if (card.Flipped)
                {
                    indexOfAllHiddenCards.Add(0);
                }
            }

            return indexOfAllHiddenCards;
        }
    }
}
