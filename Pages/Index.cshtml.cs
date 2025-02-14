using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Text;

public class IndexModel : PageModel
{
    [BindProperty]
    public int Length { get; set; } = 16;

    [BindProperty]
    public int Count { get; set; } = 1;

    [BindProperty]
    public bool IncludeLetters { get; set; } = true;

    [BindProperty]
    public bool IncludeDigits { get; set; } = true;

    [BindProperty]
    public bool IncludeSpecial { get; set; } = true;

    // Changed to a list to support multiple generated passwords.
    public List<string>? GeneratedPasswords { get; set; }

    public void OnGet()
    {
    }

    public void OnPost()
    {
        if (Count < 1)
        {
            Count = 1;
        }

        string letters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        string digits = "0123456789";
        // Updated special characters list: excludes !, @, %, * and ^
        string special = "#$&()_-+=<>?";

        StringBuilder allowedChars = new StringBuilder();

        if (IncludeLetters)
            allowedChars.Append(letters);
        if (IncludeDigits)
            allowedChars.Append(digits);
        if (IncludeSpecial)
            allowedChars.Append(special);

        if (allowedChars.Length == 0)
        {
            GeneratedPasswords = new List<string> { "Please select at least one option." };
            return;
        }

        Random random = new Random();
        List<string> passwords = new List<string>();

        for (int j = 0; j < Count; j++)
        {
            char[] password = new char[Length];
            for (int i = 0; i < Length; i++)
            {
                password[i] = allowedChars[random.Next(allowedChars.Length)];
            }
            passwords.Add(new string(password));
        }

        GeneratedPasswords = passwords;
    }
}
