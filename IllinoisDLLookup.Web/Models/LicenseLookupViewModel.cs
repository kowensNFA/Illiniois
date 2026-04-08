using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace IllinoisDLLookup.Web.Models
{
    /// <summary>
    /// View model for Illinois driver's license lookup.
    /// Illinois driver's license format: 1 uppercase letter + 11 digits (12 characters total).
    /// The first character is the first letter of the licensee's last name.
    /// </summary>
    public class LicenseLookupViewModel
    {
        [Required(ErrorMessage = "Please enter a driver's license number.")]
        [Display(Name = "Driver's License Number")]
        [RegularExpression(@"^[A-Za-z]\d{11}$",
            ErrorMessage = "Invalid format. Illinois driver's license numbers must be 1 letter followed by 11 digits (e.g. S12345678901).")]
        public string LicenseNumber { get; set; } = string.Empty;

        public LicenseLookupResult? Result { get; set; }
    }

    /// <summary>
    /// Represents the result of an Illinois driver's license lookup / format validation.
    /// </summary>
    public class LicenseLookupResult
    {
        public bool IsValid { get; set; }
        public string? LicenseNumber { get; set; }
        public string? NormalizedNumber { get; set; }
        public string? LastNameInitial { get; set; }
        public string ValidationMessage { get; set; } = string.Empty;

        /// <summary>
        /// Validates and parses an Illinois driver's license number.
        /// Illinois DL format: [A-Z][0-9]{11} — one letter followed by eleven digits.
        /// The leading letter is the first letter of the holder's last name.
        /// </summary>
        public static LicenseLookupResult Validate(string licenseNumber)
        {
            if (string.IsNullOrWhiteSpace(licenseNumber))
            {
                return new LicenseLookupResult
                {
                    IsValid = false,
                    LicenseNumber = licenseNumber,
                    ValidationMessage = "License number cannot be empty."
                };
            }

            string normalized = licenseNumber.Trim().ToUpperInvariant();

            var ilPattern = new Regex(@"^[A-Z]\d{11}$");

            if (!ilPattern.IsMatch(normalized))
            {
                return new LicenseLookupResult
                {
                    IsValid = false,
                    LicenseNumber = licenseNumber,
                    NormalizedNumber = normalized,
                    ValidationMessage = "The number entered does not match the Illinois driver's license format. "
                        + "An Illinois driver's license number consists of one letter followed by eleven digits (12 characters total)."
                };
            }

            return new LicenseLookupResult
            {
                IsValid = true,
                LicenseNumber = licenseNumber,
                NormalizedNumber = normalized,
                LastNameInitial = normalized.Substring(0, 1),
                ValidationMessage = "The license number matches the Illinois driver's license format."
            };
        }
    }
}
