using System.ComponentModel.DataAnnotations;

namespace LessonTime.WEB.Models.Catalogs
{
    public class FeatureViewModel
    {
        [Display(Name = "Kurs süre")]
        public int Duration { get; set; }
    }
}
