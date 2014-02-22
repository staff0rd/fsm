using CsQuery;
using Fsm.DataScraper.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Net;
using System.Text.RegularExpressions;
using Fsm.DataScraper.Services;

namespace Fsm.DataScraper
{
    class Program
    {
        static void Main(string[] args)
        {
            var looseCanon = new LooseCanon
            {
                Url= @"\\linuxb0x\store\www.loose-canon.info",
                Scraped = DateTime.Now
            };

//            var test = CQ.CreateFragment(@"<p style='text-align: center;'><strong></strong><em>a history of a venerated ancient prophet</em></p>
//<p><em>&nbsp;<br/></em></p>
//<p>&#8220;as transcribed&#8221; by Warlord of Elephants</p>
//<p><em>Chapter I</em></p>
//<p>1For canned Pasta was an Abomination before F.S.M. 2The land became barren, the waters as slime, the earth was rent and much suffering ensued. 3&#8243;We have lost our way”, cried the, ummm let&#8217;s see, oh yeah cried &#8216;The Lost Ones&#8217;. 4We must return to the true path or at least the real trail, maybe the actual sidewalk; 5any way this stuff ain&#8217;t workin&#8217;&#8221;. 6So with empty bellies (for none could abide the Abomination) they did gather together salt, noodles, and water.</p>
//<p>7It came to pass that the noodles boiled and a great huzzah went up. 8&#8243;Huzzah&#8221;! they cried (almost nobody talked then; they always cried stuff). 9&#8243;We must test it to see if&#8217;n it&#8217;s ready. 10Poke it with a fork&#8221;! cried some. 11&#8243;Fling it against the wall&#8221;! cried others. 12While all the crying was going on little Penelope Pasta did Taste it. 13&#8243;Hey the kid&#8217;s eatin all our pasta&#8221;! cried everybody.</p>
//<p>14Little Penelope cried (yeah her too) &#8220;I have tasted the Pasta and it needs Garlic Butter and Meat Sauce&#8221;! 15&#8243;Huzzah&#8221;! cried the people and finished their salads with the nice ranch dressing and the little bread sticks everybody liked so much. 16So heresy was avoided, 17carbo loading was accomplished 18and the legend of the ancient prophet Penelope begun.</p>
//<p><em>Chapter II</em></p>
//<p>1Now as the Pastafarians were saved and hunger pains at bay there came a great lethargy upon the People. 2&#8243;We must sleep&#8221;! they cried, &#8220;for our bellies are full and T.V. hasn&#8217;t been invented yet&#8221;. 3So they all did fall down into a deep slumber all except Penelope. 4She&#8217;d had too many after-dinner espressos with her tiramisu.</p>
//<p>5As she idly walked along she heard a voice: &#8220;Gird up you loins and follow&#8221;. 6&#8243;Grid up my loins&#8221;? she thought, &#8220;sounds vaguely naughty&#8221;. 7But as T.V. hadn&#8217;t been invented yet Penelope put the Holy Colander on her head and grabbed a handy pair of salad tongs 8(not the crappy plastic ones but the good solid metal ones). 9Penelope strode (yep you guessed nobody walked anywhere then, they all strode) through the wilderness. 10The voice led her through hill and dale 11(Hill, Dale &amp; Rill attorneys at law in the ancient world).</p>");
//            var result = test["p"].Select(p => p.InnerText);


            looseCanon.Books = new DataScraperService().GetPages(looseCanon.Url);

            XmlSerializerService.Serialize<LooseCanon>(looseCanon, @"D:\git\fsm\trunk\canon.xml");

            Console.WriteLine("Finished");
            Console.ReadKey();
        }


    }
}
