using Instagram_Clone.Authentication;
using System.ComponentModel.DataAnnotations.Schema;

namespace Instagram_Clone.Models.photo
{
    public class Photo
    {
        public int Id { get; set; }
        public string Path { get; set; }

        public string Name { get; set; }
        public bool IsDeleted { get; set; } = false;

        

    }
}
