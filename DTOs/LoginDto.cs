using System;
using System.ComponentModel.DataAnnotations;

namespace MiniItHelpdesk.DTOs;

public class LoginDto
{
    [Required, EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;
}