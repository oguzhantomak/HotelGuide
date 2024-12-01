using HotelManagementService.Application.Constants;

namespace HotelManagementService.Application.Validators;

/// <summary>
/// EN: Validator for HotelDto.
/// TR: HotelDto için doğrulayıcı.
/// </summary>
public class HotelValidator : AbstractValidator<HotelDto>
{
    public HotelValidator()
    {
        RuleFor(h => h.Name).NotEmpty().WithMessage(ValidationMessages.HotelNameRequired);
        RuleFor(h => h.Street).NotEmpty().WithMessage(ValidationMessages.StreetRequired);
        RuleFor(h => h.City).NotEmpty().WithMessage(ValidationMessages.CityRequired);
        RuleFor(h => h.Country).NotEmpty().WithMessage(ValidationMessages.CountryRequired);
    }
}