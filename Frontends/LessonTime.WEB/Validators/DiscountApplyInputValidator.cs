using FluentValidation;
using LessonTime.WEB.Models.Discount;

namespace LessonTime.WEB.Validators
{
    public class DiscountApplyInputValidator : AbstractValidator<DiscountApplyInput>
    {
        public DiscountApplyInputValidator()
        {
            RuleFor(x => x.Code).NotEmpty().WithMessage("indirim kupon alanı boş olamaz");

        }
    }
}
