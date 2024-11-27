namespace HotelManagementService.Application.Validators;

/// <summary>
/// EN: Validator for HotelDto.
/// TR: HotelDto için doğrulayıcı.
/// </summary>
public class HotelValidator : AbstractValidator<HotelDto>
{
    public HotelValidator()
    {
        RuleFor(h => h.Name).NotEmpty().WithMessage("Hotel name is required.");
        RuleFor(h => h.Street).NotEmpty().WithMessage("Street is required.");
        RuleFor(h => h.City).NotEmpty().WithMessage("City is required.");
        RuleFor(h => h.Country).NotEmpty().WithMessage("Country is required.");
    }
}