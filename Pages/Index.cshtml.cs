using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Text;

public class IndexModel : PageModel
{
    [BindProperty]
    public int Length { get; set; } = 12;

    [BindProperty]
    public bool IncludeLetters { get; set; } = true;

    [BindProperty]
    public bool IncludeDigits { get; set; } = true;

    [BindProperty]
    public bool IncludeSpecial { get; set; } = true;

    // Marking the property as nullable to avoid the CS8618 warning.
    public string? GeneratedPassword { get; set; }

    public void OnGet()
    {
        // Default values are set via property initializers.
    }

    public void OnPost()
    {
        string letters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        string digits = "0123456789";
        string special = "!@#$%^&*()_-+=<>?";

        StringBuilder allowedChars = new StringBuilder();

        if (IncludeLetters)
            allowedChars.Append(letters);
        if (IncludeDigits)
            allowedChars.Append(digits);
        if (IncludeSpecial)
            allowedChars.Append(special);

        if (allowedChars.Length == 0)
        {
            GeneratedPassword = "Please select at least one option.";
            return;
        }

        Random random = new Random();
        char[] password = new char[Length];

        for (int i = 0; i < Length; i++)
        {
            password[i] = allowedChars[random.Next(allowedChars.Length)];
        }

        GeneratedPassword = new string(password);
    }
}
