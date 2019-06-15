using System.Linq;
using System.Threading.Tasks;
using Alsein.Extensions;

namespace Cynthia.Card
{
    [CardEffectId("54016")] //玛哈坎捍卫者
    public class MahakamDefender : CardEffect
    {
        //坚韧。
        public MahakamDefender(IGwentServerGame game, GameCard card) : base(game, card)
        {
        }

        public override async Task<int> CardPlayEffect(bool isSpying)
        {
            await Card.Effect.Resilience();
            return 0;
        }
    }
}