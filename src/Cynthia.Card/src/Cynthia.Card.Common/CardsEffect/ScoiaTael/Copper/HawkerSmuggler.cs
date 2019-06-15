using System.Linq;
using System.Threading.Tasks;
using Alsein.Extensions;

namespace Cynthia.Card
{
    [CardEffectId("54010")] //私枭走私者
    public class HawkerSmuggler : CardEffect
    {
        //每有1个敌军单位被打出，便获得1点增益。
        public HawkerSmuggler(IGwentServerGame game, GameCard card) : base(game, card)
        {
        }


        public override async Task OnUnitPlay(GameCard taget)
        {
            if (taget.PlayerIndex != Card.PlayerIndex && Card.Status.CardRow.IsOnPlace())
            {
                await Card.Effect.Boost(boost);
            }
        }

        private int boost = 1;
    }
}