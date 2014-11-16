namespace VoiceWall.Web.ViewModels.Search
{
    using System.ComponentModel.DataAnnotations;

    public class SearchViewModel
    {
        [Required]
        [MinLength(2, ErrorMessage="Search text must be at least 2 characters")]
        [DataType(DataType.Text)]
        public string SearchText { get; set; }
    }
}