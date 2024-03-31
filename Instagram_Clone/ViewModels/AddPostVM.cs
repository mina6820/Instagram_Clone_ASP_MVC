using Instagram_Clone.Models.photo;
using System.ComponentModel.DataAnnotations.Schema;

namespace Instagram_Clone.ViewModels
{
    public class AddPostVM
    {

        public string? Caption { get; set; }

        public IFormFile Image { get; set; }

    }
}
