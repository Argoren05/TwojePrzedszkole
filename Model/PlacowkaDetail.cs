using kindergartenAPP.Entities;
using kindergartenAPP.ViewModels;

namespace kindergartenAPP.Model
{
    public class PlacowkaDetail
    {
        public PlacowkaLista PlacowkaLista { get; set; }
        public IList<PlacowkaPlik>? PlacowkaPlik { get; set; }
    }
}
